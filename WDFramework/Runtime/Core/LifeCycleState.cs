using System;
using UnityEngine;

/// <summary>
/// 生命周期枚举
/// </summary>

public abstract class LifeCycleState
{
    
}
// 初始化阶段
public class Initialization_LifeCycleState : LifeCycleState
{

}
// 销毁和卸载阶段
public class Shutdown_LifeCycleState : LifeCycleState
{

}
//什么都不做的阶段，一无是处的起始阶段
public class None_LifeCycleState : LifeCycleState
{

}