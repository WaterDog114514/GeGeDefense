using System;
/// <summary>
/// 用来标记Update是否已经启用的
/// </summary>
interface IUpdateStart
{
    bool isStartUpdate { get; set; }
}

/// <summary>
/// mono固定帧触发
/// </summary>
interface IMonoFixedUpdate : IUpdateStart
{
    void MonoFixedUpdate();
}
/// <summary>
/// mono帧更新触发
/// </summary>
interface IMonoUpdate : IUpdateStart
{
    void MonoUpdate();
}
/// <summary>
/// mono帧更新触发
/// </summary>
interface ILastUpdate : IUpdateStart
{
    void MonoLastUpdate();
}
