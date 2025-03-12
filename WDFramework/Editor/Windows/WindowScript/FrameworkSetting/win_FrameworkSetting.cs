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
            //���������ļ�
            data.settingData = SettingDataLoader.Instance.LoadData<FrameworkSettingData>();
            //����ʧ�ܣ������´���һ��
            if (data.settingData == null)
            {
                data.settingData = new FrameworkSettingData();
            }

        }
        [MenuItem("ˮ�������/С������趨")]
        protected static void OpenWindow()
        {
            EditorWindow.GetWindow<win_FrameworkSetting>();
        }
        //�˳�ʱ���Զ�����
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
