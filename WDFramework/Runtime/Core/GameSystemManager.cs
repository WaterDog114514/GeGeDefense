using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class GameSystemManager : Singleton<GameSystemManager>, IKernelSystem
{
    /// <summary>
    /// �Ѿ������ϵͳ
    /// </summary>
    public Dictionary<Type, IGameSystem> activeSystems { get; private set; }

    /// <summary>
    /// �׶��Զ�����ϵͳ
    /// </summary>
    private Dictionary<E_PhaseState, List<Type>> phaseAutoLauncherSystems;

    public void InitializedKernelSystem()
    {  //��ʼ��
        activeSystems = new Dictionary<Type, IGameSystem>();
        phaseAutoLauncherSystems = new Dictionary<E_PhaseState, List<Type>>();
        // ���Ľ׶��л��¼�
        EventCenterSystem.Instance.AddEventListener<E_FrameworkEvent, E_PhaseState>(E_FrameworkEvent.ChangePhase, OnPhaseChanged, 1);
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
    /// �׶��л��¼�����
    /// </summary>
    /// <param name="phase">Ŀ��׶�</param>
    private void OnPhaseChanged(E_PhaseState phase)
    {
        // ����ǰ�׶β���Ҫ��ϵͳ
        CleanupCurrentPhase();
        // �����½׶ε�ϵͳ
        if (phaseAutoLauncherSystems.TryGetValue(phase, out List<Type> systemTypes))
        {
            foreach (Type systemType in systemTypes)
            {
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
        }
    }




    /// <summary>
    /// ����ǰ�׶β���Ҫ��ϵͳ
    /// </summary>
    private void CleanupCurrentPhase()
    {
        // �������м����ϵͳ
        foreach (var systemEntry in activeSystems)
        {
            Type systemType = systemEntry.Key;
            IGameSystem system = systemEntry.Value;

            // ���ϵͳ�������½׶Σ�������
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
    /// �ж�ϵͳ�Ƿ����ڵ�ǰ�׶�
    /// </summary>
    /// <param name="systemType">ϵͳ����</param>
    /// <returns>�Ƿ����ڵ�ǰ�׶�</returns>
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
    /// �Զ�����/ע�����Ϸϵͳ���еĸ����߼�
    /// </summary>
    /// <param name="phase"></param>
    private void RegisterGameSystemUpdate(IGameSystem system)
    {
        Type systemType = system.GetType();
        //��������Ϸϵͳ�Ƿ�ӵ�и�����
        if (!systemType.IsSubclassOf(typeof(IUpdateStart)) || (system as IUpdateStart).isStartUpdate) return;
        //���ݼ̳в�ͬ��mono���£���������ͬupdate
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

