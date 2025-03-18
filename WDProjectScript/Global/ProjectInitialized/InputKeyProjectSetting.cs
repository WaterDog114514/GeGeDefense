using System;
using UnityEngine;

/// <summary>
/// 项目设定初始化类
/// 负责项目层设定的初始化
/// </summary>
public class InputKeyProjectSetting : InitializedProjectSetting
{
    public override void RegisterProjectSetting()
    {
        Debug.Log("按键设定已经实现初始化");
    }
}