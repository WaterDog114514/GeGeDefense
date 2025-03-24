using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ȫ���趨������
/// ��������ȫ������ �ؿ����� �ؿ������ȵ�
/// </summary>
public class GlobalSettingGenerator
{
    private System.Random GlobalRandom;
    private Dictionary<int,GlobalRandom> _config;
    public   GlobalSettingGenerator()
    {
        GlobalRandom = new System.Random();
        //���������ļ�
        _config = ExcelBinarayLoader.Instance.GetDataContainer<GlobalRandom>();
    }
    /// <summary>
    /// ���ɺ�ȫ���趨 ��ؿ����� ȫ������
    /// </summary>
    public GlobalRandomSetting GenerateGlobalSetting(int globalSeed = -1)
    {
        GlobalRandomSetting setting = new GlobalRandomSetting();
        //���ر�������
        setting.MaxDiffcultyLevel = _config[1].MaxDiffcultyLevel;
        setting.TotalBiomeCount = _config[1].TotalBiomeCount;
        //-1������Ҫ�����������
        if (globalSeed == -1)
        {
            GlobalRandom = new System.Random();
        }
        else
        {
            GlobalRandom = new System.Random(globalSeed);
        }
        setting.GlobalSeed = GlobalRandom.Next(1, int.MaxValue / 2);
        return setting;
    }

}
/// <summary>
/// ȫ��������趨
/// </summary>
[System.Serializable]
public class GlobalRandomSetting
{
    /// <summary>
    /// �ܲ�������
    /// </summary>
    public int LayerCount;
    // ȫ������
    public int GlobalSeed;
    public int  MaxDiffcultyLevel ;
    public int  TotalBiomeCount;

}
