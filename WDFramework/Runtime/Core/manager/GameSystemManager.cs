using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class GameSystemManager : Singleton<GameSystemManager>, IKernelSystem
{

    /// <summary>
    /// �Ѿ������ϵͳ
    /// </summary>
    /// Key:��Ϸϵͳ������
    public Dictionary<Type, IGameSystem> activeSystems { get; private set; }

    /// <summary>
    /// ��ͬ���������Զ�����ϵͳ
    /// </summary>
    /// Key �������ڵ����� Value:������ϵͳ��List
    private Dictionary<Type, List<Type>> lifeCycleAutoLauncherSystems;

    public void InitializedKernelSystem()
    {  //��ʼ��
        activeSystems = new Dictionary<Type, IGameSystem>();
        lifeCycleAutoLauncherSystems = new Dictionary<Type, List<Type>>();
        //�õ���������Type�������һ����
        var types = ReflectionHelper.GetSubclasses(typeof(LifeCycleState));
        foreach (var itemType in types)
        {
            lifeCycleAutoLauncherSystems.Add(itemType, new List<Type>());
        }
        // ���Ľ׶��л��¼�
        EventCenterSystem.Instance.AddEventListener<E_FrameworkEvent, LifeCycleState>(E_FrameworkEvent.ChangeLifeCycle, OnLifeCycleChanged, 1);
    }
    /// <summary>
    /// ע������ĳ����monoϵͳ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void RegisterSystem<T>() where T : class, IGameSystem, new()
    {
        //����¼���ϵͳ
        T newSystem = new T();
        activeSystems.Add(typeof(T), newSystem);
        //Ϊ�¼���ϵͳע������¼�(����еĻ�)
        RegisterGameSystemUpdate(newSystem);
    }
    /// <summary>
    /// ע������ĳ��monoϵͳ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void RegisterMonoSystem<T>() where T : MonoBehaviour, IGameSystem
    {
        if (typeof(T).IsSubclassOf(typeof(ManagedMonoSingleton<T>)))
        {
            //����¼���ϵͳ
            T newSystem = ManagedMonoSingleton<T>.ConstructSystem();
            activeSystems.Add(typeof(T), newSystem);
            //Ϊ�¼���ϵͳע������¼�(����еĻ�)
            RegisterGameSystemUpdate(newSystem);
        }
    }
    /// <summary>
    /// ͨ�� Type ע��� Mono ϵͳ
    /// </summary>
    /// <param name="systemType">ϵͳ����</param>
    private void RegisterSystemByType(Type systemType)
    {
        // ��������Ƿ�����Լ��
        if (systemType.IsClass && !systemType.IsAbstract && typeof(IGameSystem).IsAssignableFrom(systemType))
        {
            // ʹ�÷��䴴��ʵ��
            object systemInstance = Activator.CreateInstance(systemType);
            if (systemInstance is IGameSystem system)
            {
                activeSystems.Add(systemType, system);
                //Ϊ�¼���ϵͳע������¼�(����еĻ�)
                RegisterGameSystemUpdate(systemInstance as IGameSystem);
            }
        }
    }
    /// <summary>
    /// ͨ�� Type ע�� Mono ϵͳ
    /// </summary>
    /// <param name="systemType">ϵͳ����</param>
    private void RegisterMonoSystemByType(Type systemType)
    {
        // ��������Ƿ�����Լ��
        if (systemType.IsClass && !systemType.IsAbstract && typeof(MonoBehaviour).IsAssignableFrom(systemType) && typeof(IGameSystem).IsAssignableFrom(systemType))
        {
            // ���� GameObject ���������
            GameObject systemObject = new GameObject(systemType.Name);
            IGameSystem systemInstance = systemObject.AddComponent(systemType) as IGameSystem;
            if (systemInstance != null)
            {
                activeSystems.Add(systemType, systemInstance);
                //Ϊ�¼���ϵͳע������¼�(����еĻ�)
                RegisterGameSystemUpdate(systemInstance);
            }
        }
    }
    /// <summary>
    /// ע���Զ�������ϵͳ
    /// </summary>
    public void RegisterAutoLauncherSystem(Type lifeCycle, Type System)
    {
        var systemsList = lifeCycleAutoLauncherSystems[lifeCycle];
        if (!typeof(IGameSystem).IsAssignableFrom(System))
        {
            Debug.Log("��ϵͳ������Ϸϵͳ���޷�ע��");
            return;
        }
        if (systemsList.Contains(System))
        {
            Debug.Log("��ϵͳ�Ѿ���ӹ��ˡ��޷��ظ����");
            return;
        }
        systemsList.Add(System);
    }
    /// <summary>
    /// �׶��л��¼�����
    /// </summary>
    /// <param name="phase">Ŀ��׶�</param>
    private void OnLifeCycleChanged(LifeCycleState SwtichState)
    {
        Debug.Log("�Ѿ�����׶Σ�" + SwtichState.GetType().Name);
        //ͨ���½׶ε����ͣ��õ�Ҫ�л��׶ε�����ϵͳType��List
        List<Type> AutoLauncherSystems = lifeCycleAutoLauncherSystems[SwtichState.GetType()];
        //��ȡ�ж�
        if (AutoLauncherSystems == null)
        {
            throw new Exception("�޷��õ�State��Typeö��");
        }

        //��������������������Ϸϵͳ
        foreach (Type systemType in AutoLauncherSystems)
        {
            //�Ѿ���������ϵͳ�Ͳ�Ҫ�ظ�������
            if (activeSystems.ContainsKey(systemType))
            {
                Debug.Log("�Ѿ���������" + systemType.Name);
                continue;
            }
            // �ж��Ƿ�Ϊ MonoBehaviour ϵͳ
            if (typeof(MonoBehaviour).IsAssignableFrom(systemType))
            {
                // ���� RegisterMonoSystem
                RegisterMonoSystemByType(systemType);
            }
            else
            {
                // ���� RegisterSystem
                RegisterSystemByType(systemType);
            }
        }
        //����������׶ε�ϵͳ
        CleanupCurrentPhase();

    }
    /// <summary>
    /// ����ǰ�׶β���Ҫ��ϵͳ
    /// </summary>
    private void CleanupCurrentPhase()
    {
        //��õ�ǰ�������ڵ�����
        Type CurrentLifeCycle = LifeCycleManager.Instance.currentState.GetType();
        //�õ���ǰ��������Ӧ���е�List
        List<Type> AutoLauncherSystems = lifeCycleAutoLauncherSystems[CurrentLifeCycle];
        //Ҫ�����ϵͳ
        List<Type> deleteSystems = new List<Type>();
        // �������м����ϵͳ
        foreach (var systemType in activeSystems.Keys)
        {
            //  Ҫö�ٻ���������˼��
            // ���ϵͳ�������½׶Σ�������
            if (!AutoLauncherSystems.Contains(systemType))
            {
                //����ɾ���б�
                deleteSystems.Add(systemType);
            }
        }
        //��������ɾ������
        for (int i = deleteSystems.Count - 1; i >= 0; i--)
        {
            var systemType = deleteSystems[i];
            var system = activeSystems[systemType];
            //�ж��Ƿ���IUpdate��������Ҳ���Ƴ��˰�
            if (typeof(IUpdate).IsAssignableFrom(system.GetType()))
            {
                UpdateSystem.Instance.RemoveUpdateListener(E_UpdateLayer.GameSystem, (system as IUpdate));
            }
            //����ϵͳ��
            system.DestorySystem();
            //�Ӽ������Ƴ�
            activeSystems.Remove(systemType);
        }
        Debug.Log("������ϡ���������" + deleteSystems.Count);
    }
    /// <summary>
    /// �Զ�����/ע�����Ϸϵͳ���еĸ����߼�
    /// </summary>
    /// <param name="phase"></param>
    private void RegisterGameSystemUpdate(IGameSystem system)
    {
        //��������Ϸϵͳ�Ƿ�ӵ�и�����
        //���ݼ̳в�ͬ��mono���£���������ͬupdate
        if (typeof(IUpdate).IsAssignableFrom(system.GetType()))
        {
            UpdateSystem.Instance.AddUpdateListener(E_UpdateLayer.GameSystem, (system as IUpdate));
        }
    }
}

