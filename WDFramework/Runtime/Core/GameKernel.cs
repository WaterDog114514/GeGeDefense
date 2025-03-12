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
        //整个游戏启动流程
        // 1. 初始化内核系统
        RegisterKernelSystems();
        // 2. 正式开启框架生命周期
        // 启动游戏后进入初始化阶段
        LifeCycleManager.Instance.EnterInitializationState();

    }
    /// <summary>
    /// 初始化三大内核系统
    /// </summary>
    private static void RegisterKernelSystems()
    {
        Debug.Log("内核：已完成初始化系统");
        //初始化框架系统管理器
        FrameworkSystemManager.Instance.InitializedKernelSystem(); 
        //初始化游戏系统管理器
        GameSystemManager.Instance.InitializedKernelSystem();
        //初始化生命周期系统管理器
        LifeCycleManager.Instance.InitializedKernelSystem();
        //注册更新系统
        UpdateSystem.Instance.InitializedKernelSystem();
       
    }

    //游戏结束，关闭进程调用
    public static void ExitGame()
    {

    }


}