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
    private LifeCycleState currentState = null;

    public void InitializedKernelSystem()
    {
        currentState = new None_LifeCycleState();
    }
    /// <summary>
    /// �����ʼ���׶Σ����ں˳�ʼ����ɺ����
    /// </summary>
    public void EnterInitializationState()
    {
        Debug.Log("�ںˣ�����ɽ����ʼ���׶�");
        TransitionToState(new Initialization_LifeCycleState());
    }

    /// <summary>
    /// �л���������״̬
    /// </summary>
    private void TransitionToState(LifeCycleState newState)
    {
        if (currentState == newState) return;
        //�����л����������¼�
        EventCenterSystem.Instance.TriggerEvent(E_FrameworkEvent.TransitionE_LifeCycleState, newState);
        currentState = newState;
    

    }
}