using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace WDEditor
{
    /// <summary>
    ///编辑器开发的设置管理类，用来查看并设定编辑器信息
    /// </summary>
    public class win_ExtensionEditor : EditorWindow
    {

        [MenuItem("水汪汪框架/生成编辑器拓展")]
        protected static void OpenWindow()
        {
            EditorWindow.GetWindow<win_ExtensionEditor>();
        }
        public string FileName;
        public void OnGUI()
        {
            FileName = EditorGUILayout.TextField("编辑器名：", FileName);
            if (GUILayout.Button("生成三板斧..."))
            {
                string path = EditorUtility.SaveFolderPanel("选择生成路径", Application.dataPath, "");
                if (string.IsNullOrEmpty(path)) return;

                CreateWindow(FileName, path);
                CreateDraw(FileName, path);
                CreateData(FileName, path);

            }
        }
        public void CreateData(string Name, string path)
        {
            ScriptInfo info = new ScriptInfo();
            info.ClassName = $"winData_{Name}";
            info.InheritClassName = "BaseWindowData";
            info.MethodLines = new List<string>()
            {
                "public override void IntiFirstCreate()\r\n{\r\n}",
            };
            ScriptCreateHelper.CreateScript(info, Path.Combine(path, $"winData_{Name}.cs"));
        }
        public void CreateWindow(string Name, string path)
        {
            ScriptInfo info = new ScriptInfo();
            info.ClassName = $"win_{Name}";
            info.InheritClassName = $"BaseWindow<winDraw_{Name}, winData_{Name}>";
            info.UsingInfo = new string[] { "using UnityEditor;" };
            info.MethodLines = new List<string>()
            {
                "[MenuItem(\"水汪汪框架/生成编辑器拓展\")]\r\n    protected static void OpenWindow()\r\n{}",
            };
            ScriptCreateHelper.CreateScript(info, Path.Combine(path, $"win_{Name}.cs"));
        }
        public void CreateDraw(string Name, string path)
        {
            ScriptInfo info = new ScriptInfo();
            info.ClassName = $"winDraw_{Name}";
            info.InheritClassName = $"BaseWindowDraw<winData_{Name}>";
            info.UsingInfo = new string[] { "using UnityEditor;" };
            info.MethodLines = new List<string>()
            {
                "public override void Draw(){}",
                "public override void OnCreated(){\r\n  }"
            };
            ScriptCreateHelper.CreateScript(info, Path.Combine(path, $"winDraw_{Name}.cs"));
        }
    }
}
