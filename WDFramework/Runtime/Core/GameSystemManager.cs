using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class GameSystemManager : Singleton<GameSystemManager>, IKernelSystem
{
    /// <summary>
    /// 已经激活的系统
    /// </summary>
    public Dictionary<Type, IGameSystem> activeSystems { get; private set; }

    /// <summary>
    /// 阶段自动启动系统
    /// </summary>
    private Dictionary<E_PhaseState, List<Type>> phaseAutoLauncherSystems;

    public void InitializedKernelSystem()
    {  //初始化
        activeSystems = new Dictionary<Type, IGameSystem>();
        phaseAutoLauncherSystems = new Dictionary<E_PhaseState, List<Type>>();
        // 订阅阶段切换事件
        EventCenterSystem.Instance.AddEventListener<E_FrameworkEvent, E_PhaseState>(E_FrameworkEvent.ChangePhase, OnPhaseChanged, 1);
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
    /// 阶段切换事件处理
    /// </summary>
    /// <param name="phase">目标阶段</param>
    private void OnPhaseChanged(E_PhaseState phase)
    {
        // 清理当前阶段不需要的系统
        CleanupCurrentPhase();
        // 加载新阶段的系统
        if (phaseAutoLauncherSystems.TryGetValue(phase, out List<Type> systemTypes))
        {
            foreach (Type systemType in systemTypes)
            {
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
        }
    }




    /// <summary>
    /// 清理当前阶段不需要的系统
    /// </summary>
    private void CleanupCurrentPhase()
    {
        // 遍历所有激活的系统
        foreach (var systemEntry in activeSystems)
        {
            Type systemType = systemEntry.Key;
            IGameSystem system = systemEntry.Value;

            // 如果系统不属于新阶段，则销毁
            if (!IsSystemInCurrentPhase(systemType))
            {
                if (system is MonoBehaviour monoSystem)
                {
                    UnityEngine.Object.Destroy(monoSystem.gameObject);
                }
                else if (system is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                activeSystems.Remove(systemType);
            }
        }
    }
    /// <summary>
    /// 判断系统是否属于当前阶段
    /// </summary>
    /// <param name="systemType">系统类型</param>
    /// <returns>是否属于当前阶段</returns>
    private bool IsSystemInCurrentPhase(Type systemType)
    {
        foreach (var phaseSystems in phaseAutoLauncherSystems.Values)
        {
            if (phaseSystems.Contains(systemType))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 自动启用/注册该游戏系统所有的更新逻辑
    /// </summary>
    /// <param name="phase"></param>
    private void RegisterGameSystemUpdate(IGameSystem system)
    {
        Type systemType = system.GetType();
        //检索此游戏系统是否拥有更新类
        if (!systemType.IsSubclassOf(typeof(IUpdateStart)) || (system as IUpdateStart).isStartUpdate) return;
        //根据继承不同的mono更新，来启动不同update
        if (systemType.IsSubclassOf(typeof(IMonoUpdate)))
        {
            UpdateSystem.Instance.AddUpdateListener(E_UpdateLayer.GameSystem, (system as IMonoUpdate).MonoUpdate);
        }
        if (systemType.IsSubclassOf(typeof(IMonoFixedUpdate)))
        {
            UpdateSystem.Instance.AddFixedUpdateListener(E_UpdateLayer.GameSystem, (system as IMonoFixedUpdate).MonoFixedUpdate);

        }
        if (systemType.IsSubclassOf(typeof(ILastUpdate)))
        {
            UpdateSystem.Instance.AddLateUpdateListener(E_UpdateLayer.GameSystem, (system as ILastUpdate).MonoLastUpdate);
        }


    }
}

