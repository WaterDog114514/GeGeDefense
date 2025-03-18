using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class GameSystemManager : Singleton<GameSystemManager>, IKernelSystem
{

    /// <summary>
    /// 已经激活的系统
    /// </summary>
    /// Key:游戏系统的类型
    public Dictionary<Type, IGameSystem> activeSystems { get; private set; }

    /// <summary>
    /// 不同生命周期自动启动系统
    /// </summary>
    /// Key 生命周期的类型 Value:启动的系统的List
    private Dictionary<Type, List<Type>> lifeCycleAutoLauncherSystems;

    public void InitializedKernelSystem()
    {  //初始化
        activeSystems = new Dictionary<Type, IGameSystem>();
        lifeCycleAutoLauncherSystems = new Dictionary<Type, List<Type>>();
        //得到所有周期Type，先添加一波先
        var types = ReflectionHelper.GetSubclasses(typeof(LifeCycleState));
        foreach (var itemType in types)
        {
            lifeCycleAutoLauncherSystems.Add(itemType, new List<Type>());
        }
        // 订阅阶段切换事件
        EventCenterSystem.Instance.AddEventListener<E_FrameworkEvent, LifeCycleState>(E_FrameworkEvent.ChangeLifeCycle, OnLifeCycleChanged, 1);
    }
    /// <summary>
    /// 注册启用某个非mono系统
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void RegisterSystem<T>() where T : class, IGameSystem, new()
    {
        //添加新激活系统
        T newSystem = new T();
        activeSystems.Add(typeof(T), newSystem);
        //为新激活系统注册更新事件(如果有的话)
        RegisterGameSystemUpdate(newSystem);
    }
    /// <summary>
    /// 注册启用某个mono系统
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void RegisterMonoSystem<T>() where T : MonoBehaviour, IGameSystem
    {
        if (typeof(T).IsSubclassOf(typeof(ManagedMonoSingleton<T>)))
        {
            //添加新激活系统
            T newSystem = ManagedMonoSingleton<T>.ConstructSystem();
            activeSystems.Add(typeof(T), newSystem);
            //为新激活系统注册更新事件(如果有的话)
            RegisterGameSystemUpdate(newSystem);
        }
    }
    /// <summary>
    /// 通过 Type 注册非 Mono 系统
    /// </summary>
    /// <param name="systemType">系统类型</param>
    private void RegisterSystemByType(Type systemType)
    {
        // 检查类型是否满足约束
        if (systemType.IsClass && !systemType.IsAbstract && typeof(IGameSystem).IsAssignableFrom(systemType))
        {
            // 使用反射创建实例
            object systemInstance = Activator.CreateInstance(systemType);
            if (systemInstance is IGameSystem system)
            {
                activeSystems.Add(systemType, system);
                //为新激活系统注册更新事件(如果有的话)
                RegisterGameSystemUpdate(systemInstance as IGameSystem);
            }
        }
    }
    /// <summary>
    /// 通过 Type 注册 Mono 系统
    /// </summary>
    /// <param name="systemType">系统类型</param>
    private void RegisterMonoSystemByType(Type systemType)
    {
        // 检查类型是否满足约束
        if (systemType.IsClass && !systemType.IsAbstract && typeof(MonoBehaviour).IsAssignableFrom(systemType) && typeof(IGameSystem).IsAssignableFrom(systemType))
        {
            // 创建 GameObject 并挂载组件
            GameObject systemObject = new GameObject(systemType.Name);
            IGameSystem systemInstance = systemObject.AddComponent(systemType) as IGameSystem;
            if (systemInstance != null)
            {
                activeSystems.Add(systemType, systemInstance);
                //为新激活系统注册更新事件(如果有的话)
                RegisterGameSystemUpdate(systemInstance);
            }
        }
    }
    /// <summary>
    /// 注册自动启动的系统
    /// </summary>
    public void RegisterAutoLauncherSystem(Type lifeCycle, Type System)
    {
        var systemsList = lifeCycleAutoLauncherSystems[lifeCycle];
        if (!typeof(IGameSystem).IsAssignableFrom(System))
        {
            Debug.Log("该系统不是游戏系统，无法注册");
            return;
        }
        if (systemsList.Contains(System))
        {
            Debug.Log("该系统已经添加过了。无法重复添加");
            return;
        }
        systemsList.Add(System);
    }
    /// <summary>
    /// 阶段切换事件处理
    /// </summary>
    /// <param name="phase">目标阶段</param>
    private void OnLifeCycleChanged(LifeCycleState SwtichState)
    {
        Debug.Log("已经进入阶段：" + SwtichState.GetType().Name);
        //通过新阶段的类型，得到要切换阶段的所有系统Type的List
        List<Type> AutoLauncherSystems = lifeCycleAutoLauncherSystems[SwtichState.GetType()];
        //获取判断
        if (AutoLauncherSystems == null)
        {
            throw new Exception("无法得到State的Type枚举");
        }

        //根据新类型启动各个游戏系统
        foreach (Type systemType in AutoLauncherSystems)
        {
            //已经启动过的系统就不要重复启动了
            if (activeSystems.ContainsKey(systemType))
            {
                Debug.Log("已经启动过了" + systemType.Name);
                continue;
            }
            // 判断是否为 MonoBehaviour 系统
            if (typeof(MonoBehaviour).IsAssignableFrom(systemType))
            {
                // 调用 RegisterMonoSystem
                RegisterMonoSystemByType(systemType);
            }
            else
            {
                // 调用 RegisterSystem
                RegisterSystemByType(systemType);
            }
        }
        //清理不是这个阶段的系统
        CleanupCurrentPhase();

    }
    /// <summary>
    /// 清理当前阶段不需要的系统
    /// </summary>
    private void CleanupCurrentPhase()
    {
        //获得当前生命周期的类型
        Type CurrentLifeCycle = LifeCycleManager.Instance.currentState.GetType();
        //得到当前生命周期应该有的List
        List<Type> AutoLauncherSystems = lifeCycleAutoLauncherSystems[CurrentLifeCycle];
        //要清理的系统
        List<Type> deleteSystems = new List<Type>();
        // 遍历所有激活的系统
        foreach (var systemType in activeSystems.Keys)
        {
            //  要枚举还是类做？思考
            // 如果系统不属于新阶段，则销毁
            if (!AutoLauncherSystems.Contains(systemType))
            {
                //加入删除列表
                deleteSystems.Add(systemType);
            }
        }
        //进行批量删除操作
        for (int i = deleteSystems.Count - 1; i >= 0; i--)
        {
            var systemType = deleteSystems[i];
            var system = activeSystems[systemType];
            //判断是否有IUpdate，连更新也给移除了吧
            if (typeof(IUpdate).IsAssignableFrom(system.GetType()))
            {
                UpdateSystem.Instance.RemoveUpdateListener(E_UpdateLayer.GameSystem, (system as IUpdate));
            }
            //销毁系统类
            system.DestorySystem();
            //从激活中移除
            activeSystems.Remove(systemType);
        }
        Debug.Log("清理完毕。供清理了" + deleteSystems.Count);
    }
    /// <summary>
    /// 自动启用/注册该游戏系统所有的更新逻辑
    /// </summary>
    /// <param name="phase"></param>
    private void RegisterGameSystemUpdate(IGameSystem system)
    {
        //检索此游戏系统是否拥有更新类
        //根据继承不同的mono更新，来启动不同update
        if (typeof(IUpdate).IsAssignableFrom(system.GetType()))
        {
            UpdateSystem.Instance.AddUpdateListener(E_UpdateLayer.GameSystem, (system as IUpdate));
        }
    }
}

