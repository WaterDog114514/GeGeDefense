using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 脚本生成类  为了每个模块都能使用，生成脚本必须使用此类
/// </summary>
public static class ScriptCreateHelper
{
    /// <summary>
    /// 脚本信息
    /// </summary>
    public static ScriptInfo info;

    /// <summary>
    /// 生成脚本
    /// </summary>
    /// <param name="path">保存路径</param>
    /// <param name="ClassName">类名</param>
    /// <param name="InheritName">继承名</param>
    /// <param name="usingInfo">引用信息</param>
    /// <param name="Members">成员变量们</param>
    /// <param name="Methods">方法们</param>
    public static void CreateScript(string path, string ClassName, string InheritName = null, string[] usingInfo = null, List<string> Members = null, List<string> Methods = null)
    {
        //赋值
        info = new ScriptInfo();
        info.UsingInfo = usingInfo;
        info.ClassName = ClassName;
        info.InheritClassName = InheritName;
        info.MemberLines = Members;
        info.MethodLines = Methods;
        CreateScript(info, path);
    }
    /// <summary>
    ///  生成脚本的真正方法
    /// </summary>
    public static void CreateScript(ScriptInfo info, string path)
    {
        StringBuilder content = new StringBuilder();

        //空三行
        if (info.ClassName == null)
        {
            Debug.LogError("生成错误，类名为空，请检查！已停止生成");
            return;
        }

        //生成引用信息
        if (info.UsingInfo != null && info.UsingInfo.Length > 0)
        {
            for (int i = 0; i < info.UsingInfo.Length; i++)
            {
                content.AppendLine(info.UsingInfo[i]);
            }
        }
        content.AppendLine(DefaultUsingInfo);
        content.Append("\n\n\n");
        //写类名
        if (info.InheritClassName == null)
            content.AppendLine("public class " + info.ClassName);
        else
            content.AppendLine("public class " + info.ClassName + " : " + info.InheritClassName);
        //第一个大括号
        content.AppendLine("{");

        #region 写每行空成员和空方法

        //写每行成员字段
        if (info.MemberLines!= null&&info.MemberLines.Count>0)
        {
            //没写就跳过
            for (int i = 0; i < info.MemberLines.Count; i++)
            {
                content.AppendLine(info.MemberLines[i]);
            }
        }
        content.Append("\n");
        //写每行的方法
        if (info.MethodLines != null&& info.MethodLines.Count>0)
        {
            for (int i = 0; i < info.MethodLines.Count; i++)
            {
                content.AppendLine(info.MethodLines[i]);
            }
        }
        #endregion
        //写附加内容
        if (info.ExtraContent != null)
        {
            content.AppendLine("\n");
            content.Append(info.ExtraContent);
        }

        //最后大括号
        content.AppendLine("\n\n}");

        //开始写入脚本
        if (path == null) path = AssetsPath;
        File.WriteAllText(path, content.ToString());
        //刷新一下 才能看到
        AssetDatabase.Refresh();
    }
    /// <summary>
    /// 必加引用
    /// </summary>
    private const string DefaultUsingInfo = "using UnityEngine;";
    /// <summary>
    /// 生成路径
    /// </summary>
    private static string Path;
    //固定路径
    public static string AssetsPath = Application.dataPath;
}



#region 生成代码的信息类们

/// <summary>
/// 生成脚本方法信息
/// </summary>
[System.Serializable]

public class ScriptInfo
{/// <summary>
/// 脚本引用信息
/// </summary>
    public string[] UsingInfo;
    public string ClassName;
    public string InheritClassName;
    /// <summary>
    /// 成员信息
    /// </summary>
    public List<string> MemberLines;
    /// <summary>
    /// 方法信息
    /// </summary>
    public List<string> MethodLines;
    /// <summary>
    /// 附加内容
    /// </summary>
    public string ExtraContent;
}
#endregion
