using System.Runtime.CompilerServices;
using UnityEngine;

public class babaSystem : ManagedSingleton<babaSystem>, IGameSystem,IMonoLastUpdate
{
    public override void Initialized()
    {
        Debug.Log("�ְ�ϵͳ��ʼ����");
    }
    public override void DestorySystem()
    {
        base.DestorySystem();
        Debug.Log("�ְ�ϵͳɾ���˰�");
    }

    public void MonoLastUpdate()
    {
        Debug.Log("�ְ�������°�");
    }
}
public class sonSystem : ManagedSingleton<sonSystem>, IGameSystem,IMonoUpdate,IMonoLastUpdate
{
    public bool isStartUpdate { get;set; }

    public override void Initialized()
    {
        Debug.Log("����ϵͳ��ʼ����");
    }
    public override void DestorySystem()
    {
        base.DestorySystem();
        Debug.Log("����ϵͳɾ���˰�");
    }

    public void MonoUpdate()
    {
        Debug.Log("���¶���ϵͳ");
    }

    public void MonoLastUpdate()
    {
        Debug.Log("�ӳٸ��¶���ϵͳ");
    }
}
public class monoSisterSystem : ManagedMonoSingleton<monoSisterSystem>, IGameSystem,IMonoFixedUpdate
{
    public bool isStartUpdate { get;set; }

    public override void Initialized()
    {
        Debug.Log("����monoϵͳ��ʼ����");
    }
    public override void DestorySystem()
    {
        base.DestorySystem();
        Debug.Log("����monoϵͳɾ���˰�");
    }

    public void MonoFixedUpdate()
    {
        Debug.Log("����������°���");
    }
}
