using System;
using UnityEngine;

/// <summary>
/// 生命周期枚举
/// </summary>

public abstract class LifeCycleState
{
    public static bool operator ==(LifeCycleState left, LifeCycleState right)
    {
        if (ReferenceEquals(left, right)) return true;
        if (left is null || right is null) return false;
        return left.GetType() == right.GetType();
    }

    public static bool operator !=(LifeCycleState left, LifeCycleState right)
    {
        return !(left == right);
    }

    public override bool Equals(object obj)
    {
        return obj is LifeCycleState other && this == other;
    }

    public override int GetHashCode()
    {
        return GetType().GetHashCode();
    }
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
//public static class LifeCycleStateExtension
//{
//    public static string getTypeName(this LifeCycleState state)
//    {
//        return state.GetType().Name;
//    }
//}