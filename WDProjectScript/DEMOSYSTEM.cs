using System.Runtime.CompilerServices;
using UnityEngine;

public class babaSystem : ManagedSingleton<babaSystem>, IGameSystem,IMonoLastUpdate
{
    public override void Initialized()
    {
        Debug.Log("爸爸系统初始化了");
    }
    public override void DestorySystem()
    {
        base.DestorySystem();
        Debug.Log("爸爸系统删除了啊");
    }

    public void MonoLastUpdate()
    {
        Debug.Log("爸爸老年更新啊");
    }
}
public class sonSystem : ManagedSingleton<sonSystem>, IGameSystem,IMonoUpdate,IMonoLastUpdate
{
    public bool isStartUpdate { get;set; }

    public override void Initialized()
    {
        Debug.Log("儿子系统初始化了");
    }
    public override void DestorySystem()
    {
        base.DestorySystem();
        Debug.Log("儿子系统删除了啊");
    }

    public void MonoUpdate()
    {
        Debug.Log("更新蛾子系统");
    }

    public void MonoLastUpdate()
    {
        Debug.Log("延迟更新蛾子系统");
    }
}
public class monoSisterSystem : ManagedMonoSingleton<monoSisterSystem>, IGameSystem,IMonoFixedUpdate
{
    public bool isStartUpdate { get;set; }

    public override void Initialized()
    {
        Debug.Log("姐妹mono系统初始化了");
    }
    public override void DestorySystem()
    {
        base.DestorySystem();
        Debug.Log("姐妹mono系统删除了啊");
    }

    public void MonoFixedUpdate()
    {
        Debug.Log("姐妹物理更新啊啊");
    }
}
