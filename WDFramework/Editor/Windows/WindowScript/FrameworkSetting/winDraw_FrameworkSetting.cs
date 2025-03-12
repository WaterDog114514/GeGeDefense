using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using WDFramework;


namespace WDEditor
{
    //������
    public class winDraw_FrameworkSetting : BaseWindowDraw<winData_FrameworkSetting>
    {

        private FrameworkSettingData settingData => data.settingData;
        private GUIStyle TitleStyle => data.TitleStyle;
        private bool IsFoldPrefab;
        private bool IsFoldExcel = false;
        private bool IsFoldAB = false;
        //����
        public winDraw_FrameworkSetting(EditorWindow window, winData_FrameworkSetting data) : base(window, data)
        {
        }

        private void DrawExcelSetting()
        {
            IsFoldExcel = EditorGUILayout.Foldout(IsFoldExcel, "Excel�����ļ���ȡ���ã�");
            if (!IsFoldExcel)
            {
                //����·�����
                GUILayout.Label("����·������", TitleStyle);
                EditorGUILayout.BeginHorizontal();
                settingData.loadContainerSetting.DataPath = EditorGUILayout.TextField("Excel�������ļ���·����", settingData.loadContainerSetting.DataPath);
                settingData.loadContainerSetting.IsDebugStreamingAssetLoad = EditorGUILayout.Toggle("����Stream���Լ���", settingData.abLoadSetting.IsStreamingABLoad);
                EditorGUILayout.EndHorizontal();
            }
        }
        private void DrawABSetting()
        {
            IsFoldAB = EditorGUILayout.Foldout(IsFoldAB, "AB��������ã�");
            if (!IsFoldAB)
            {
                //�������
                GUILayout.Label("����������", TitleStyle);
                settingData.abLoadSetting.ABMainName = EditorGUILayout.TextField("������", settingData.abLoadSetting.ABMainName);
                //����·�����
                GUILayout.Label("����·������", TitleStyle);

                EditorGUILayout.BeginHorizontal();
                settingData.abLoadSetting.IsStreamingABLoad = EditorGUILayout.Toggle("��StreamimgAsset�м���", settingData.abLoadSetting.IsStreamingABLoad);
                //�������
                if (!settingData.abLoadSetting.IsStreamingABLoad)
                    settingData.abLoadSetting.ABRuntimeLoadPath = EditorGUILayout.TextField("����·��(�����ϷĿ¼)��", settingData.abLoadSetting.ABRuntimeLoadPath);
                else
                {
                    GUI.enabled = false;
                    settingData.abLoadSetting.ABRuntimeLoadPath = EditorGUILayout.TextField("����·����", Path.Combine(Application.streamingAssetsPath, "ABPackage"));
                    GUI.enabled = true;
                }
                EditorGUILayout.EndHorizontal();
                GUILayout.Label($"��ǰ����·��Ϊ��{settingData.abLoadSetting.ABRuntimeLoadPath}");

                //���ñ༭������
                EditorGUILayout.BeginHorizontal();
                settingData.abLoadSetting.IsDebugABLoad = EditorGUILayout.Toggle("�����༭�����Լ���", settingData.abLoadSetting.IsDebugABLoad);
                GUILayout.Label("(�����󣬽�ͨ��Editorͬ������AB��)");
                EditorGUILayout.EndHorizontal();
                if (settingData.abLoadSetting.IsDebugABLoad)
                {
                    settingData.abLoadSetting.ABEditorLoadPath = EditorGUILayout.TextField("�༭��AB������·����", settingData.abLoadSetting.ABEditorLoadPath);
                }
            }
        }

        //���÷���
        private string[] SettingOptions;
        /// <summary>
        /// �����Ĭ������
        /// </summary>
        private void DrawPoolDefaultSetting()
        {
            if (SettingOptions == null)
            {
                SettingOptions = Enum.GetNames(typeof(Pool.E_PoolType));
            }

            IsFoldPrefab = EditorGUILayout.Foldout(IsFoldPrefab, "Excel�����ļ���ȡ���ã�");
            if (!IsFoldPrefab)
            {
               
                //����·�����
                GUILayout.Label("�����Ĭ������", TitleStyle);
                settingData.defaultPoolSetting.PoolType = (Pool.E_PoolType)EditorGUILayout.Popup(
                    "�����Ĭ�����ͣ�",
                   (int)settingData.defaultPoolSetting.PoolType,
                     SettingOptions);

                settingData.defaultPoolSetting.MaxCount = EditorGUILayout.IntField("�����Ĭ�����ޣ�", settingData.defaultPoolSetting.MaxCount);
            }
        }
        public void DrawButton()
        {
            EditorGUILayout.Space(20);
            if (GUILayout.Button("���������޸�"))
            {
                (window as win_FrameworkSetting).SaveData();
            }
        }
        public override void Draw()
        {
            //Excel�����ļ���������
            DrawExcelSetting();
            //AB��������ػ���
            DrawABSetting();
            //Ԥ����������
            DrawPoolDefaultSetting();
            //���Ƶײ���ť
            DrawButton();
        }


        public override void OnCreated()
        {
        }


    }

}