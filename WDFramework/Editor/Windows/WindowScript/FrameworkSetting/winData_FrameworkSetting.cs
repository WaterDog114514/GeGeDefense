using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
/// <summary>
/// ���༭���������ù����࣬�����洢���趨�༭�������Ϣ��Ҳ�����������ɱ༭����
/// </summary>
namespace WDEditor
{
    [Serializable]
    public class winData_FrameworkSetting : BaseWindowData
    {
        //������ɫ����
        [NonSerialized]
        public GUIStyle TitleStyle = new GUIStyle();
        [NonSerialized]
        public FrameworkSettingData settingData;
        public override void IntiLoad()
        {
            TitleStyle = new GUIStyle();
        }

    }
}
