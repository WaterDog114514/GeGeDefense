using System;
using UnityEngine;

/// <summary>
/// ��������ö��
/// </summary>

public abstract class LifeCycleState
{
    
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