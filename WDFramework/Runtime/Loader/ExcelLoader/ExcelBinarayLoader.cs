using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Excel游戏数据配置文件加载工具，配合Excel加载工具使用
/// </summary>
public class ExcelBinarayLoader : Singleton<ExcelBinarayLoader>
{
    private Dictionary<Type, IDictionary> dic_LoadedContainer = new Dictionary<Type, IDictionary>();
    public ExcelBinarayLoader()
    {
    }
    /// <summary>
    /// 后面要做，通过框架设置配置文件存储位置
    /// </summary>
    private string ConfigurationDiretoryPath;
    private const string FileSuffix = ".waterdogdata";
    //KeyType：字典Key，ConfigType：Configuration存储类型
    public IDictionary GetDataContainer(Type ConfigType)
    {
        if (dic_LoadedContainer.ContainsKey(ConfigType)) return dic_LoadedContainer[ConfigType];
        //return GetDataContainer<TKey, TConfigType>($"{typeof(TKey).Name}.waterdogdata");
        //通过反射全集进行查找Type
     
        //找到附属类型，就开始进行加载，从StreamingAssets里加载
        else
        {
            return LoadExcelContainer(ConfigType, ConfigType.Name + "Container");
        }
    }
    /// <summary>
    /// 取得Excel配置表，通过FileName去加载StreamingAsset，此法可以加载多个相同类型的Excel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    //TKey：字典Key，TConfigType：Configuration存储类型
    public Dictionary<int,TConfigType> GetDataContainer<TConfigType>() where TConfigType : ExcelConfiguration
    {
        if (!dic_LoadedContainer.ContainsKey(typeof(TConfigType)))
        {
            var dictionary =  LoadExcelContainer(typeof(TConfigType), typeof(TConfigType).Name + "Container");
            return dictionary as Dictionary<int, TConfigType>;
        }
        return dic_LoadedContainer[typeof(TConfigType)] as Dictionary<int, TConfigType>;
    }

    /// <summary>
    /// 将通过StreamingAssets中加载文件  
    /// </summary>
    /// <param name="FileName">文件名(包括后缀名)</param>
    private IDictionary LoadExcelContainer( Type ConfigType, string FileName)
    {
        var path = Application.streamingAssetsPath + "/" + FileName+FileSuffix;
        if (!File.Exists(path))
        {
            Debug.Log($"加载失败，不存在此{FileName}文件");
            return null;
        }
        //先看有没有。有了就不要重复添加
        var genericTypeDefinition = typeof(ExcelConfigurationContainer<>);
        var typeArguments = new Type[] { ConfigType };
        var container = BinaryManager.LoadOpenGeneric(genericTypeDefinition, typeArguments, path);
        if (container == null)
        {
            Debug.LogError("加载容器失败");
            return null;
        }
        // 获取 container 属性
        var containerProperty = container.GetType().BaseType.GetField("container");
        if (containerProperty == null)
        {
            Debug.LogError("未找到 container 属性");
            return null;
        }
        // 获取 container 属性的值
        var containerValue = containerProperty.GetValue(container) as IDictionary;
        if (containerValue == null)
        {
            Debug.LogError("container 属性值为空");
            return null;
        }

        // 更新或添加到字典
        if (dic_LoadedContainer.ContainsKey(ConfigType))
        {
            dic_LoadedContainer[ConfigType] = containerValue;
        }
        else
        {
            dic_LoadedContainer.Add(ConfigType, containerValue);
        }

        return containerValue;
    }
    /// <summary>
    /// 获得表中某一列属性所有数值信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name=""></param>
    /// <returns></returns>
    //public string[] GetDataPropertyInfo<TKey,TConfigType>(string PropertyName, string FileName) where  TConfigType : ExcelConfiguration
    //{
    //    var container = GetDataContainer<TKey,TConfigType>()  as Dictionary<TKey,TConfigType>;
    //    // 反射获取dataDic字段
    //    var dataDicField = typeof(T).GetField("dataDic");
    //    if (dataDicField == null)
    //    {
    //        Debug.LogError($"读取错误，请不要在{typeof(T).Name}中改dataDic属性名");
    //        return null;
    //    }
    //    // 获取dataDic的值
    //    var dataDicValue = dataDicField.GetValue(container) as IDictionary;

    //    // 获取字典的值类型（类型2）
    //    var valueType = dataDicField.FieldType.GetGenericArguments()[1];

    //    // 通过类型2得到名为name的字段
    //    var nameField = valueType.GetField(PropertyName);
    //    if (nameField == null)
    //    {
    //        Debug.LogWarning($"Excel表加载提示：{valueType.Name}不存在{PropertyName}的字段，已返回null");
    //        return null;
    //    }
    //    int index = 0;
    //    string[] propertyNames = new string[dataDicValue.Count];
    //    // 遍历字典，获取名为name的字段的值
    //    foreach (DictionaryEntry pair in dataDicValue)
    //    {
    //        object valueObject = pair.Value;
    //        propertyNames[index] = nameField.GetValue(valueObject).ToString();
    //        index++;
    //    }
    //    return propertyNames;
    //}
}
