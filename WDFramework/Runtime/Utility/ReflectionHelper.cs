using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


/// <summary>
/// 反射工具类 可以获取某类的所有子类
/// 后续可以考虑放一个在C-Sharp程序集里，不然跨程序集反射会获取不到
/// </summary>
public static class ReflectionHelper
{
    /// <summary>
    /// 获取某类型的所有子类（包括所有已加载的程序集）
    /// </summary>
    /// <param name="baseType">基类类型</param>
    /// <returns>所有子类的类型列表</returns>
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
    /// 直接返回某类型的父类类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Type GetBaseType(Type type)
    {
        return type.BaseType;
    }

    /// <summary>
    /// 获取指定类型的第N个泛型参数类型
    /// </summary>
    /// <param name="type">要获取泛型参数的类型</param>
    /// <param name="n">参数索引（从0开始）</param>
    /// <returns>返回指定的泛型参数类型，若不存在则返回 null</returns>
    public static Type GetGenericArgumentType(Type type, int n)
    {

        var genericArgs = type.GetGenericArguments();
        if (genericArgs.Length > n)
        {
            return genericArgs[n]; // 返回第N个泛型参数类型
        }
        return null; // 如果没有找到，返回 null
    }
    /// <summary>
    /// 判断某个类型是否继承自指定的泛型基类
    /// </summary>
    private static bool IsSubclassOfGeneric(Type type, Type genericBaseType)
    {
        if (type.BaseType == null)
            return false;

        // 递归检查所有父类
        while (type != null && type != typeof(object))
        {
            // 检查是否是泛型类型
            if (type.IsGenericType && type.GetGenericTypeDefinition() == genericBaseType)
            {
                return true; // 找到了匹配的泛型基类
            }

            type = type.BaseType; // 移动到父类
        }

        return false; // 没有找到匹配的泛型基类
    }
    //根据string遍历所有程序集，找到对应Type
    public static Type FindTypeInAssemblies(string typeName)
    {
        // 遍历所有已加载的程序集
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            // 查找该类型
            Type type = assembly.GetTypes().FirstOrDefault(t => t.Name == typeName);
            if (type != null)
            {
                return type; // 找到并返回类型
            }
        }
        return null; // 未找到类型
    }
}
