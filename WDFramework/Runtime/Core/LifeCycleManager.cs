using System;
using UnityEngine;

/// <summary>
/// 生命周期管理器，负责管理游戏的生命周期
/// </summary>
public class LifeCycleManager : Singleton<LifeCycleManager>, IKernelSystem
{
 
    /// <summary>
    /// 当前游戏阶段
    /// </summary>
    private LifeCycleState currentState = null;

    public void InitializedKernelSystem()
    {
        currentState = new None_LifeCycleState();
    }
    /// <summary>
    /// 进入初始化阶段，由内核初始化完成后调用
    /// </summary>
    public void EnterInitializationState()
    {
        Debug.Log("内核：已完成进入初始化阶段");
        TransitionToState(new Initialization_LifeCycleState());
    }

    /// <summary>
    /// 切换生命周期状态
    /// </summary>
    private void TransitionToState(LifeCycleState newState)
    {
        if (currentState == newState) return;
        //触发切换生命周期事件
        EventCenterSystem.Instance.TriggerEvent(E_FrameworkEvent.TransitionE_LifeCycleState, newState);
        currentState = newState;
    

    }
}