using System;
using System.Collections.Generic;
using UnityEngine;

public class FrameworkSystemManager : Singleton<FrameworkSystemManager>, IKernelSystem
{
    /// <summary>
    /// �Ѿ������ϵͳ
    /// </summary>
    public List<IFrameworkSystem> systems { get; private set; }
    public void InitializedKernelSystem()
    {
        systems = new List<IFrameworkSystem>();
        //ע�����ϵͳ
        RegisterListenEvent();
    }
    /// <summary>
    /// ע������¼�
    /// </summary>
    public void RegisterListenEvent()
    {
        //ע���������ڽ����ʼ���¼�
        EventCenterSystem.Instance.AddEventListener<E_FrameworkEvent, LifeCycleState>(E_FrameworkEvent.ChangeLifeCycle, (state) =>
        {
            if(state is Initialization_LifeCycleState)
                InitializedAllFrameworkSystem();
        });

    }
    //��ʼ�����п��ϵͳ
    private void InitializedAllFrameworkSystem()
    {

    }
    /// <summary>
    /// �������п��ϵͳ���ڽ�������ʱ�����
    /// </summary>
    public void KillAllFrameworkSystem()
    {
    }
}