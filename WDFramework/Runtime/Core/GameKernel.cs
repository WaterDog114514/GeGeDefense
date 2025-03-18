using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditorInternal;
using UnityEngine;
using static LifeCycleManager;
/// <summary>
/// 游戏内核，万物的起源
/// 一切由他开始也由它结束，是一切的核心
/// </summary>
public static class GameKernel
{

    // 游戏启动入口，每当启动游戏后会调用
    [RuntimeInitializeOnLoadMethod]
    public static void StartGame()
    {
        Debug.Log("正式启动游戏");
        //整个游戏启动流程
        // 1. 初始化内核系统
        RegisterKernelSystems();
        //2.初始化所有的项目设置，项目核心初始化
        ProjectSettingInitialized();
        // 3. 正式开启框架生命周期
        // 进入初始化阶段
        EnterInitializationState();
    }
    private static void EnterInitializationState()
    {
        Debug.Log("内核：已完成进入初始化阶段");
        LifeCycleManager.Instance.SwtichState(new Initialization_LifeCycleState());
    }
    /// <summary>
    /// 初始化4大内核系统
    /// </summary>
    private static void RegisterKernelSystems()
    {
        //初始化框架系统管理器
        FrameworkSystemManager.Instance.InitializedKernelSystem();
        //初始化游戏系统管理器
        GameSystemManager.Instance.InitializedKernelSystem();
        //初始化生命周期系统管理器
        LifeCycleManager.Instance.InitializedKernelSystem();
        //初始化项目层初始化管理器
        ProjectSettingManager.Instance.InitializedKernelSystem();
        //初始化更新系统
        UpdateSystem.Instance.InitializedKernelSystem();
        Debug.Log("内核：已完成初始化系统");
    }
    /// <summary>
    /// 初始化所有项目层设定，如注册按键绑定/注册XX事件等
    /// 只要继承ProjectSetting的家伙就会被初始化
    /// </summary>
    private static void ProjectSettingInitialized()
    {
       ProjectSettingManager.Instance.RegisterAllProjectSetting();
    }
    //游戏结束，关闭进程调用
    public static void ExitGame()
    {

    }


}