using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 主编辑器窗口设置管理类，用来存储并设定编辑器风格信息，也可以用来生成编辑器类
/// </summary>
namespace WDEditor
{
    [Serializable]
    public class winData_FrameworkSetting : BaseWindowData
    {
        //垃圾颜色字体
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
