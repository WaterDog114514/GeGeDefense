using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Excel��Ϸ���������ļ����ع��ߣ����Excel���ع���ʹ��
/// </summary>
public class ExcelBinarayLoader : Singleton<ExcelBinarayLoader>
{
    private Dictionary<Type, IDictionary> dic_LoadedContainer = new Dictionary<Type, IDictionary>();
    public ExcelBinarayLoader()
    {
    }
    /// <summary>
    /// ����Ҫ����ͨ��������������ļ��洢λ��
    /// </summary>
    private string ConfigurationDiretoryPath;
    private const string FileSuffix = ".waterdogdata";
    //KeyType���ֵ�Key��ConfigType��Configuration�洢����
    public IDictionary GetDataContainer(Type ConfigType)
    {
        if (dic_LoadedContainer.ContainsKey(ConfigType)) return dic_LoadedContainer[ConfigType];
        //return GetDataContainer<TKey, TConfigType>($"{typeof(TKey).Name}.waterdogdata");
        //ͨ������ȫ�����в���Type
     
        //�ҵ��������ͣ��Ϳ�ʼ���м��أ���StreamingAssets�����
        else
        {
            return LoadExcelContainer(ConfigType, ConfigType.Name + "Container");
        }
    }
    /// <summary>
    /// ȡ��Excel���ñ�ͨ��FileNameȥ����StreamingAsset���˷����Լ��ض����ͬ���͵�Excel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    //TKey���ֵ�Key��TConfigType��Configuration�洢����
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
    /// ��ͨ��StreamingAssets�м����ļ�  
    /// </summary>
    /// <param name="FileName">�ļ���(������׺��)</param>
    private IDictionary LoadExcelContainer( Type ConfigType, string FileName)
    {
        var path = Application.streamingAssetsPath + "/" + FileName+FileSuffix;
        if (!File.Exists(path))
        {
            Debug.Log($"����ʧ�ܣ������ڴ�{FileName}�ļ�");
            return null;
        }
        //�ȿ���û�С����˾Ͳ�Ҫ�ظ����
        var genericTypeDefinition = typeof(ExcelConfigurationContainer<>);
        var typeArguments = new Type[] { ConfigType };
        var container = BinaryManager.LoadOpenGeneric(genericTypeDefinition, typeArguments, path);
        if (container == null)
        {
            Debug.LogError("��������ʧ��");
            return null;
        }
        // ��ȡ container ����
        var containerProperty = container.GetType().BaseType.GetField("container");
        if (containerProperty == null)
        {
            Debug.LogError("δ�ҵ� container ����");
            return null;
        }
        // ��ȡ container ���Ե�ֵ
        var containerValue = containerProperty.GetValue(container) as IDictionary;
        if (containerValue == null)
        {
            Debug.LogError("container ����ֵΪ��");
            return null;
        }

        // ���»���ӵ��ֵ�
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
    /// ��ñ���ĳһ������������ֵ��Ϣ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name=""></param>
    /// <returns></returns>
    //public string[] GetDataPropertyInfo<TKey,TConfigType>(string PropertyName, string FileName) where  TConfigType : ExcelConfiguration
    //{
    //    var container = GetDataContainer<TKey,TConfigType>()  as Dictionary<TKey,TConfigType>;
    //    // �����ȡdataDic�ֶ�
    //    var dataDicField = typeof(T).GetField("dataDic");
    //    if (dataDicField == null)
    //    {
    //        Debug.LogError($"��ȡ�����벻Ҫ��{typeof(T).Name}�и�dataDic������");
    //        return null;
    //    }
    //    // ��ȡdataDic��ֵ
    //    var dataDicValue = dataDicField.GetValue(container) as IDictionary;

    //    // ��ȡ�ֵ��ֵ���ͣ�����2��
    //    var valueType = dataDicField.FieldType.GetGenericArguments()[1];

    //    // ͨ������2�õ���Ϊname���ֶ�
    //    var nameField = valueType.GetField(PropertyName);
    //    if (nameField == null)
    //    {
    //        Debug.LogWarning($"Excel�������ʾ��{valueType.Name}������{PropertyName}���ֶΣ��ѷ���null");
    //        return null;
    //    }
    //    int index = 0;
    //    string[] propertyNames = new string[dataDicValue.Count];
    //    // �����ֵ䣬��ȡ��Ϊname���ֶε�ֵ
    //    foreach (DictionaryEntry pair in dataDicValue)
    //    {
    //        object valueObject = pair.Value;
    //        propertyNames[index] = nameField.GetValue(valueObject).ToString();
    //        index++;
    //    }
    //    return propertyNames;
    //}
}
