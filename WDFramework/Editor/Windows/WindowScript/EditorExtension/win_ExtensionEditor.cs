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
    ///�༭�����������ù����࣬�����鿴���趨�༭����Ϣ
    /// </summary>
    public class win_ExtensionEditor : EditorWindow
    {

        [MenuItem("ˮ�������/���ɱ༭����չ")]
        protected static void OpenWindow()
        {
            EditorWindow.GetWindow<win_ExtensionEditor>();
        }
        public string FileName;
        public void OnGUI()
        {
            FileName = EditorGUILayout.TextField("�༭������", FileName);
            if (GUILayout.Button("�������師..."))
            {
                string path = EditorUtility.SaveFolderPanel("ѡ������·��", Application.dataPath, "");
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
                "[MenuItem(\"ˮ�������/���ɱ༭����չ\")]\r\n    protected static void OpenWindow()\r\n{}",
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
