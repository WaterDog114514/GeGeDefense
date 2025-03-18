using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生命周期管理器，负责管理游戏的生命周期
/// </summary>
public class ProjectSettingManager : Singleton<ProjectSettingManager>, IKernelSystem
{
    /// <summary>
    /// 已经完成注册的系统
    /// </summary>
    private List<InitializedProjectSetting> registeredProjectSetting;
    public void InitializedKernelSystem()
    {
        registeredProjectSetting = new List<InitializedProjectSetting>();

    }
    public void RegisterAllProjectSetting()
    {
        //通过反射得到全部的设定类
        var projectSettingTypes = ReflectionHelper.GetSubclasses(typeof(InitializedProjectSetting));
        foreach (var itemType in projectSettingTypes)
        {
            var newSetting = Activator.CreateInstance(itemType) as InitializedProjectSetting;
            //注册这些设定
            newSetting.RegisterProjectSetting();
            registeredProjectSetting.Add(newSetting);
        }


    }
}