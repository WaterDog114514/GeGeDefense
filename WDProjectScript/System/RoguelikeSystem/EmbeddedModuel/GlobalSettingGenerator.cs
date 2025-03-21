using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ȫ���趨������
/// ��������ȫ������ �ؿ����� �ؿ������ȵ�
/// </summary>
public class GlobalSettingGenerator
{
    private System.Random GlobalRandom;

    public void Initialize()
    {
        GlobalRandom = new System.Random();
    }
    /// <summary>
    /// ���ɺ�ȫ���趨 ��ؿ����� ȫ������
    /// </summary>
    public GlobalRandomSetting GenerateGlobalSetting(int globalSeed = -1)
    {
        GlobalRandomSetting setting = new GlobalRandomSetting();
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
    /// <summary>
    /// ����Ⱥϵ����
    /// </summary>
    public List<BiomeProcessData> biomeProcessDatas;
}
/// <summary>
/// ����Ⱥϵ��������
/// </summary>
public class BiomeProcessData
{
    public E_SceneBiome biome;
    /// <summary>
    /// ����
    /// </summary>
    public int LayerCount;
}