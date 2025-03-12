using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditorInternal;
using UnityEngine;
using static LifeCycleManager;
/// <summary>
/// ��Ϸ�ںˣ��������Դ
/// һ��������ʼҲ������������һ�еĺ���
/// </summary>
public static class GameKernel
{

    // ��Ϸ������ڣ�ÿ��������Ϸ������
    [RuntimeInitializeOnLoadMethod]
    public static void StartGame()
    {
        //������Ϸ��������
        // 1. ��ʼ���ں�ϵͳ
        RegisterKernelSystems();
        // 2. ��ʽ���������������
        // ������Ϸ������ʼ���׶�
        LifeCycleManager.Instance.EnterInitializationState();

    }
    /// <summary>
    /// ��ʼ�������ں�ϵͳ
    /// </summary>
    private static void RegisterKernelSystems()
    {
        Debug.Log("�ںˣ�����ɳ�ʼ��ϵͳ");
        //��ʼ�����ϵͳ������
        FrameworkSystemManager.Instance.InitializedKernelSystem(); 
        //��ʼ����Ϸϵͳ������
        GameSystemManager.Instance.InitializedKernelSystem();
        //��ʼ����������ϵͳ������
        LifeCycleManager.Instance.InitializedKernelSystem();
        //ע�����ϵͳ
        UpdateSystem.Instance.InitializedKernelSystem();
       
    }

    //��Ϸ�������رս��̵���
    public static void ExitGame()
    {

    }


}