using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


/// <summary>
/// ���乤���� ���Ի�ȡĳ�����������
/// �������Կ��Ƿ�һ����C-Sharp�������Ȼ����򼯷�����ȡ����
/// </summary>
public static class ReflectionHelper
{
    /// <summary>
    /// ��ȡĳ���͵��������ࣨ���������Ѽ��صĳ��򼯣�
    /// </summary>
    /// <param name="baseType">��������</param>
    /// <returns>��������������б�</returns>
    public static List<Type> GetSubclasses(Type baseType)
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly =>
            {
                try
                {
                    return assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    return ex.Types.Where(t => t != null);
                }
            })
            .Where(t => t != null && t.IsClass && !t.IsAbstract && t.IsSubclassOf(baseType))
            .ToList();
    }


    /// <summary>
    /// ֱ�ӷ���ĳ���͵ĸ�������
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Type GetBaseType(Type type)
    {
        return type.BaseType;
    }

    /// <summary>
    /// ��ȡָ�����͵ĵ�N�����Ͳ�������
    /// </summary>
    /// <param name="type">Ҫ��ȡ���Ͳ���������</param>
    /// <param name="n">������������0��ʼ��</param>
    /// <returns>����ָ���ķ��Ͳ������ͣ����������򷵻� null</returns>
    public static Type GetGenericArgumentType(Type type, int n)
    {

        var genericArgs = type.GetGenericArguments();
        if (genericArgs.Length > n)
        {
            return genericArgs[n]; // ���ص�N�����Ͳ�������
        }
        return null; // ���û���ҵ������� null
    }
    /// <summary>
    /// �ж�ĳ�������Ƿ�̳���ָ���ķ��ͻ���
    /// </summary>
    private static bool IsSubclassOfGeneric(Type type, Type genericBaseType)
    {
        if (type.BaseType == null)
            return false;

        // �ݹ������и���
        while (type != null && type != typeof(object))
        {
            // ����Ƿ��Ƿ�������
            if (type.IsGenericType && type.GetGenericTypeDefinition() == genericBaseType)
            {
                return true; // �ҵ���ƥ��ķ��ͻ���
            }

            type = type.BaseType; // �ƶ�������
        }

        return false; // û���ҵ�ƥ��ķ��ͻ���
    }
    //����string�������г��򼯣��ҵ���ӦType
    public static Type FindTypeInAssemblies(string typeName)
    {
        // ���������Ѽ��صĳ���
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            // ���Ҹ�����
            Type type = assembly.GetTypes().FirstOrDefault(t => t.Name == typeName);
            if (type != null)
            {
                return type; // �ҵ�����������
            }
        }
        return null; // δ�ҵ�����
    }
}
