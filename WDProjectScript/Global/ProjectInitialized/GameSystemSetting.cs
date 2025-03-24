using System;
using UnityEngine;

/// <summary>
/// 项目设定初始化类
/// 负责项目层设定的初始化
/// </summary>
public class GameSystemSetting : InitializedProjectSetting
{
    public override void RegisterProjectSetting()
    {
        Debug.Log("游戏系统注册已经实现初始化");
        GameSystemManager.Instance.RegisterAutoLauncherSystem(typeof(Game_LifeCycle), typeof(RogueLikeGeneratorSystem));


    }
}