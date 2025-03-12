using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using WDFramework;


namespace WDEditor
{
    //绘制类
    public class winDraw_FrameworkSetting : BaseWindowDraw<winData_FrameworkSetting>
    {

        private FrameworkSettingData settingData => data.settingData;
        private GUIStyle TitleStyle => data.TitleStyle;
        private bool IsFoldPrefab;
        private bool IsFoldExcel = false;
        private bool IsFoldAB = false;
        //构造
        public winDraw_FrameworkSetting(EditorWindow window, winData_FrameworkSetting data) : base(window, data)
        {
        }

        private void DrawExcelSetting()
        {
            IsFoldExcel = EditorGUILayout.Foldout(IsFoldExcel, "Excel配置文件读取设置：");
            if (!IsFoldExcel)
            {
                //加载路径相关
                GUILayout.Label("加载路径设置", TitleStyle);
                EditorGUILayout.BeginHorizontal();
                settingData.loadContainerSetting.DataPath = EditorGUILayout.TextField("Excel二进制文件总路径：", settingData.loadContainerSetting.DataPath);
                settingData.loadContainerSetting.IsDebugStreamingAssetLoad = EditorGUILayout.Toggle("开启Stream测试加载", settingData.abLoadSetting.IsStreamingABLoad);
                EditorGUILayout.EndHorizontal();
            }
        }
        private void DrawABSetting()
        {
            IsFoldAB = EditorGUILayout.Foldout(IsFoldAB, "AB包打包设置：");
            if (!IsFoldAB)
            {
                //包名相关
                GUILayout.Label("主包名设置", TitleStyle);
                settingData.abLoadSetting.ABMainName = EditorGUILayout.TextField("主包：", settingData.abLoadSetting.ABMainName);
                //加载路径相关
                GUILayout.Label("加载路径设置", TitleStyle);

                EditorGUILayout.BeginHorizontal();
                settingData.abLoadSetting.IsStreamingABLoad = EditorGUILayout.Toggle("从StreamimgAsset中加载", settingData.abLoadSetting.IsStreamingABLoad);
                //如果开启
                if (!settingData.abLoadSetting.IsStreamingABLoad)
                    settingData.abLoadSetting.ABRuntimeLoadPath = EditorGUILayout.TextField("加载路径(相对游戏目录)：", settingData.abLoadSetting.ABRuntimeLoadPath);
                else
                {
                    GUI.enabled = false;
                    settingData.abLoadSetting.ABRuntimeLoadPath = EditorGUILayout.TextField("加载路径：", Path.Combine(Application.streamingAssetsPath, "ABPackage"));
                    GUI.enabled = true;
                }
                EditorGUILayout.EndHorizontal();
                GUILayout.Label($"当前加载路径为：{settingData.abLoadSetting.ABRuntimeLoadPath}");

                //设置编辑器加载
                EditorGUILayout.BeginHorizontal();
                settingData.abLoadSetting.IsDebugABLoad = EditorGUILayout.Toggle("开启编辑器测试加载", settingData.abLoadSetting.IsDebugABLoad);
                GUILayout.Label("(开启后，将通过Editor同步加载AB包)");
                EditorGUILayout.EndHorizontal();
                if (settingData.abLoadSetting.IsDebugABLoad)
                {
                    settingData.abLoadSetting.ABEditorLoadPath = EditorGUILayout.TextField("编辑器AB包加载路径：", settingData.abLoadSetting.ABEditorLoadPath);
                }
            }
        }

        //设置分组
        private string[] SettingOptions;
        /// <summary>
        /// 对象池默认设置
        /// </summary>
        private void DrawPoolDefaultSetting()
        {
            if (SettingOptions == null)
            {
                SettingOptions = Enum.GetNames(typeof(Pool.E_PoolType));
            }

            IsFoldPrefab = EditorGUILayout.Foldout(IsFoldPrefab, "Excel配置文件读取设置：");
            if (!IsFoldPrefab)
            {
               
                //加载路径相关
                GUILayout.Label("对象池默认设置", TitleStyle);
                settingData.defaultPoolSetting.PoolType = (Pool.E_PoolType)EditorGUILayout.Popup(
                    "对象池默认类型：",
                   (int)settingData.defaultPoolSetting.PoolType,
                     SettingOptions);

                settingData.defaultPoolSetting.MaxCount = EditorGUILayout.IntField("对象池默认上限：", settingData.defaultPoolSetting.MaxCount);
            }
        }
        public void DrawButton()
        {
            EditorGUILayout.Space(20);
            if (GUILayout.Button("保存所有修改"))
            {
                (window as win_FrameworkSetting).SaveData();
            }
        }
        public override void Draw()
        {
            //Excel配置文件加载设置
            DrawExcelSetting();
            //AB包加载相关绘制
            DrawABSetting();
            //预设体总设置
            DrawPoolDefaultSetting();
            //绘制底部按钮
            DrawButton();
        }


        public override void OnCreated()
        {
        }


    }

}