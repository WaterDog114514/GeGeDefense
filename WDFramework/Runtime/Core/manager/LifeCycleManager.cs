using System;
using UnityEngine;

/// <summary>
/// �������ڹ����������������Ϸ����������
/// </summary>
public class LifeCycleManager : Singleton<LifeCycleManager>, IKernelSystem
{
 
    /// <summary>
    /// ��ǰ��Ϸ�׶�
    /// </summary>
    public LifeCycleState currentState { get; private set;}

    public void InitializedKernelSystem()
    {
        currentState = new None_LifeCycleState();
    }

    /// <summary>
    /// �л���������״̬
    /// </summary>
    public void SwtichState(LifeCycleState newState)
    {
        //����ǵ�ǰ���ڣ��Ͳ���
        if (currentState == newState) return;
        //��������������״̬
        currentState = newState;
        //�����л����������¼�
        EventCenterSystem.Instance.TriggerEvent(E_FrameworkEvent.ChangeLifeCycle, newState);
    

    }
}