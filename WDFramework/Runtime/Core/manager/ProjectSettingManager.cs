using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������ڹ����������������Ϸ����������
/// </summary>
public class ProjectSettingManager : Singleton<ProjectSettingManager>, IKernelSystem
{
    /// <summary>
    /// �Ѿ����ע���ϵͳ
    /// </summary>
    private List<InitializedProjectSetting> registeredProjectSetting;
    public void InitializedKernelSystem()
    {
        registeredProjectSetting = new List<InitializedProjectSetting>();

    }
    public void RegisterAllProjectSetting()
    {
        //ͨ������õ�ȫ�����趨��
        var projectSettingTypes = ReflectionHelper.GetSubclasses(typeof(InitializedProjectSetting));
        foreach (var itemType in projectSettingTypes)
        {
            var newSetting = Activator.CreateInstance(itemType) as InitializedProjectSetting;
            //ע����Щ�趨
            newSetting.RegisterProjectSetting();
            registeredProjectSetting.Add(newSetting);
        }


    }
}