using System;
using System.Data;
using System.IO;
using System.Text;

/// <summary>
/// C# ����������ʵ��
/// ���ܣ�
/// 1. ������Excel����Ӧ��������
/// 2. ���ɰ����ֵ�ṹ��������
/// </summary>
public class CSharpCodeGenerator 
{

    public void GenerateDataClass(DataTable table, string outputPath, ExcelReadRule readRule)
    {
        // ����У��
        if (table == null || table.Rows.Count == 0) throw new ArgumentException("�������Ϊ��");
        if (readRule.PropertyNameRowIndex < 0 || readRule.PropertyTypeRowIndex < 0) throw new ArgumentOutOfRangeException("��������Ч");

        // ��ȡ�ֶζ�����
        DataRow rowName = table.Rows[readRule.PropertyNameRowIndex];
        DataRow rowType = table.Rows[readRule.PropertyTypeRowIndex];

        // �������Ŀ¼
        Directory.CreateDirectory(outputPath);

        // ���������
        StringBuilder classCode = new StringBuilder();
        classCode.AppendLine("[System.Serializable]"); // ������л����
        classCode.AppendLine($"public class {readRule.ConfigTypeName} : ExcelConfiguration"); // �̳л���
        classCode.AppendLine("{");

        // �ӵ�3�п�ʼ�����ֶΣ�����ǰ���У�ע���к�ID�У�
        for (int i = 2; i < table.Columns.Count; i++)
        {
            classCode.AppendLine($"public {rowType[i]} {rowName[i]};");
        }
        classCode.AppendLine("}");

        // д���ļ�
        string filePath = Path.Combine(outputPath, $"{readRule.ConfigTypeName}.cs");
        File.WriteAllText(filePath, classCode.ToString());
    }

    public void GenerateContainerClass(DataTable table, string outputPath,ExcelReadRule readRule)
    {
        // ����У��
        if (table == null) throw new ArgumentException("�������Ϊ��");

        // �������������
        StringBuilder containerCode = new StringBuilder();
        containerCode.AppendLine("using System.Collections.Generic;"); // ���������ռ�
        containerCode.AppendLine("[System.Serializable]"); // ���л����
        containerCode.AppendLine($"public class {readRule.ConfigTypeName}Container : ExcelConfigurationContainer<{table.TableName}>"); // ������������
        containerCode.AppendLine("{");
        containerCode.AppendLine("}");

        // д���ļ�
        string filePath = Path.Combine(outputPath, $"{readRule.ConfigTypeName}Container.cs");
        File.WriteAllText(filePath, containerCode.ToString());
    }
}