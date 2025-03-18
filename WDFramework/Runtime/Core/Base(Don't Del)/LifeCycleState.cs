using System;
using UnityEngine;

/// <summary>
/// ��������ö��
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
// ��ʼ���׶�
public class Initialization_LifeCycleState : LifeCycleState
{

}
// ���ٺ�ж�ؽ׶�
public class Shutdown_LifeCycleState : LifeCycleState
{

}
//ʲô�������Ľ׶Σ�һ���Ǵ�����ʼ�׶�
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