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
    /// 获取某类型的所有子类
    /// </summary>
    /// <param name="baseType"></param>
    /// <returns></returns>
    public static List<Type> GetSubclasses(Type baseType)
    {
        return Assembly.GetAssembly(baseType)
                       .GetTypes()
                       .Where(t => t.IsSubclassOf(baseType))
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
    /// 获取所有程序集中，目标类型的所有子类
    /// </summary>
    /// <param name="targetType"></param>
    /// <returns></returns>
    public static List<Type> GetGenericSubClasses(Type targetType)
    {
        // 获取所有程序集中的所有类型
        var allTypes = AppDomain.CurrentDomain.GetAssemblies() .SelectMany(assembly => assembly.GetTypes());
        // 找到所有继承自指定泛型基类的子类
        var subclassesTypes = allTypes
            .Where(t => IsSubclassOfGeneric(t, targetType) && !t.IsAbstract) // 过滤掉抽象类
            .ToList();

        return subclassesTypes;
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
