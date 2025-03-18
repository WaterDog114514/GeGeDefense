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
        Debug.Log("��ʽ������Ϸ");
        //������Ϸ��������
        // 1. ��ʼ���ں�ϵͳ
        RegisterKernelSystems();
        //2.��ʼ�����е���Ŀ���ã���Ŀ���ĳ�ʼ��
        ProjectSettingInitialized();
        // 3. ��ʽ���������������
        // �����ʼ���׶�
        EnterInitializationState();
    }
    private static void EnterInitializationState()
    {
        Debug.Log("�ںˣ�����ɽ����ʼ���׶�");
        LifeCycleManager.Instance.SwtichState(new Initialization_LifeCycleState());
    }
    /// <summary>
    /// ��ʼ��4���ں�ϵͳ
    /// </summary>
    private static void RegisterKernelSystems()
    {
        //��ʼ�����ϵͳ������
        FrameworkSystemManager.Instance.InitializedKernelSystem();
        //��ʼ����Ϸϵͳ������
        GameSystemManager.Instance.InitializedKernelSystem();
        //��ʼ����������ϵͳ������
        LifeCycleManager.Instance.InitializedKernelSystem();
        //��ʼ����Ŀ���ʼ��������
        ProjectSettingManager.Instance.InitializedKernelSystem();
        //��ʼ������ϵͳ
        UpdateSystem.Instance.InitializedKernelSystem();
        Debug.Log("�ںˣ�����ɳ�ʼ��ϵͳ");
    }
    /// <summary>
    /// ��ʼ��������Ŀ���趨����ע�ᰴ����/ע��XX�¼���
    /// ֻҪ�̳�ProjectSetting�ļһ�ͻᱻ��ʼ��
    /// </summary>
    private static void ProjectSettingInitialized()
    {
       ProjectSettingManager.Instance.RegisterAllProjectSetting();
    }
    //��Ϸ�������رս��̵���
    public static void ExitGame()
    {

    }


}