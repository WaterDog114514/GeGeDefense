using System;
using UnityEngine;

/// <summary>
/// ��Ŀ�趨��ʼ����
/// ������Ŀ���趨�ĳ�ʼ��
/// </summary>
public class GameSystemSetting : InitializedProjectSetting
{
    public override void RegisterProjectSetting()
    {
        Debug.Log("��Ϸϵͳע���Ѿ�ʵ�ֳ�ʼ��");
        GameSystemManager.Instance.RegisterAutoLauncherSystem(typeof(Game_LifeCycle), typeof(RogueLikeGeneratorSystem));


    }
}