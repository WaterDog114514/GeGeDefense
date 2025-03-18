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
    public LifeCycleState currentState { get; private set;}

    public void InitializedKernelSystem()
    {
        currentState = new None_LifeCycleState();
    }

    /// <summary>
    /// 切换生命周期状态
    /// </summary>
    public void SwtichState(LifeCycleState newState)
    {
        //如果是当前周期，就不换
        if (currentState == newState) return;
        //赋予新生命周期状态
        currentState = newState;
        //触发切换生命周期事件
        EventCenterSystem.Instance.TriggerEvent(E_FrameworkEvent.ChangeLifeCycle, newState);
    

    }
}