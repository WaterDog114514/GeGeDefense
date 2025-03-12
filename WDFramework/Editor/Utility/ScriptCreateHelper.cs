using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// �ű�������  Ϊ��ÿ��ģ�鶼��ʹ�ã����ɽű�����ʹ�ô���
/// </summary>
public static class ScriptCreateHelper
{
    /// <summary>
    /// �ű���Ϣ
    /// </summary>
    public static ScriptInfo info;

    /// <summary>
    /// ���ɽű�
    /// </summary>
    /// <param name="path">����·��</param>
    /// <param name="ClassName">����</param>
    /// <param name="InheritName">�̳���</param>
    /// <param name="usingInfo">������Ϣ</param>
    /// <param name="Members">��Ա������</param>
    /// <param name="Methods">������</param>
    public static void CreateScript(string path, string ClassName, string InheritName = null, string[] usingInfo = null, List<string> Members = null, List<string> Methods = null)
    {
        //��ֵ
        info = new ScriptInfo();
        info.UsingInfo = usingInfo;
        info.ClassName = ClassName;
        info.InheritClassName = InheritName;
        info.MemberLines = Members;
        info.MethodLines = Methods;
        CreateScript(info, path);
    }
    /// <summary>
    ///  ���ɽű�����������
    /// </summary>
    public static void CreateScript(ScriptInfo info, string path)
    {
        StringBuilder content = new StringBuilder();

        //������
        if (info.ClassName == null)
        {
            Debug.LogError("���ɴ�������Ϊ�գ����飡��ֹͣ����");
            return;
        }

        //����������Ϣ
        if (info.UsingInfo != null && info.UsingInfo.Length > 0)
        {
            for (int i = 0; i < info.UsingInfo.Length; i++)
            {
                content.AppendLine(info.UsingInfo[i]);
            }
        }
        content.AppendLine(DefaultUsingInfo);
        content.Append("\n\n\n");
        //д����
        if (info.InheritClassName == null)
            content.AppendLine("public class " + info.ClassName);
        else
            content.AppendLine("public class " + info.ClassName + " : " + info.InheritClassName);
        //��һ��������
        content.AppendLine("{");

        #region дÿ�пճ�Ա�Ϳշ���

        //дÿ�г�Ա�ֶ�
        if (info.MemberLines!= null&&info.MemberLines.Count>0)
        {
            //ûд������
            for (int i = 0; i < info.MemberLines.Count; i++)
            {
                content.AppendLine(info.MemberLines[i]);
            }
        }
        content.Append("\n");
        //дÿ�еķ���
        if (info.MethodLines != null&& info.MethodLines.Count>0)
        {
            for (int i = 0; i < info.MethodLines.Count; i++)
            {
                content.AppendLine(info.MethodLines[i]);
            }
        }
        #endregion
        //д��������
        if (info.ExtraContent != null)
        {
            content.AppendLine("\n");
            content.Append(info.ExtraContent);
        }

        //��������
        content.AppendLine("\n\n}");

        //��ʼд��ű�
        if (path == null) path = AssetsPath;
        File.WriteAllText(path, content.ToString());
        //ˢ��һ�� ���ܿ���
        AssetDatabase.Refresh();
    }
    /// <summary>
    /// �ؼ�����
    /// </summary>
    private const string DefaultUsingInfo = "using UnityEngine;";
    /// <summary>
    /// ����·��
    /// </summary>
    private static string Path;
    //�̶�·��
    public static string AssetsPath = Application.dataPath;
}



#region ���ɴ������Ϣ����

/// <summary>
/// ���ɽű�������Ϣ
/// </summary>
[System.Serializable]

public class ScriptInfo
{/// <summary>
/// �ű�������Ϣ
/// </summary>
    public string[] UsingInfo;
    public string ClassName;
    public string InheritClassName;
    /// <summary>
    /// ��Ա��Ϣ
    /// </summary>
    public List<string> MemberLines;
    /// <summary>
    /// ������Ϣ
    /// </summary>
    public List<string> MethodLines;
    /// <summary>
    /// ��������
    /// </summary>
    public string ExtraContent;
}
#endregion
