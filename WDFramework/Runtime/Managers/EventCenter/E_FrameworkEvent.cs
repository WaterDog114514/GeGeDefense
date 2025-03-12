using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//框架层

/// <summary>
/// 事件监听的框架层全局事件
/// </summary>
public enum E_FrameworkEvent
{
    //所有要监听的事件定义一个规范名
    /// <summary>
    /// 改变游戏阶段，一般用在监听系统注册，加载逻辑
    /// </summary>
    ChangePhase,
    ChangeScene,
    /// <summary>
    /// 切换游戏生命周期
    /// </summary>
    TransitionE_LifeCycleState
     
}
