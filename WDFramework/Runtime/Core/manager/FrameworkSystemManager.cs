using System;
using System.Collections.Generic;
using UnityEngine;

public class FrameworkSystemManager : Singleton<FrameworkSystemManager>, IKernelSystem
{
    /// <summary>
    /// 已经激活的系统
    /// </summary>
    public List<IFrameworkSystem> systems { get; private set; }
    public void InitializedKernelSystem()
    {
        systems = new List<IFrameworkSystem>();
        //注册监听系统
        RegisterListenEvent();
    }
    /// <summary>
    /// 注册监听事件
    /// </summary>
    public void RegisterListenEvent()
    {
        //注册生命周期进入初始化事件
        EventCenterSystem.Instance.AddEventListener<E_FrameworkEvent, LifeCycleState>(E_FrameworkEvent.ChangeLifeCycle, (state) =>
        {
            if(state is Initialization_LifeCycleState)
                InitializedAllFrameworkSystem();
        });

    }
    //初始化所有框架系统
    private void InitializedAllFrameworkSystem()
    {

    }
    /// <summary>
    /// 销毁所有框架系统，在结束进程时候调用
    /// </summary>
    public void KillAllFrameworkSystem()
    {
    }
}