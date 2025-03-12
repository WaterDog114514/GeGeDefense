using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
namespace WDEditor
{

    public class win_FrameworkSetting : BaseWindow<winDraw_FrameworkSetting,
        winData_FrameworkSetting>
    {

        public override void OnOpenWindow()
        {
            //加载配置文件
            data.settingData = SettingDataLoader.Instance.LoadData<FrameworkSettingData>();
            //加载失败，则重新创建一个
            if (data.settingData == null)
            {
                data.settingData = new FrameworkSettingData();
            }

        }
        [MenuItem("水汪汪框架/小框架主设定")]
        protected static void OpenWindow()
        {
            EditorWindow.GetWindow<win_FrameworkSetting>();
        }
        //退出时候自动保存
        public override void OnCloseWindow()
        {
            SaveData();
        }
        public void SaveData()
        {
            SettingDataLoader.Instance.SaveData(data.settingData);
            AssetDatabase.Refresh();
        }
    }


}
