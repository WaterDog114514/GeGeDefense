using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 全局设定生成器
/// 负责生成全局种子 关卡数量 关卡层数等等
/// </summary>
public class GlobalSettingGenerator
{
    private System.Random GlobalRandom;

    public void Initialize()
    {
        GlobalRandom = new System.Random();
    }
    /// <summary>
    /// 生成好全局设定 如关卡数量 全局种子
    /// </summary>
    public GlobalRandomSetting GenerateGlobalSetting(int globalSeed = -1)
    {
        GlobalRandomSetting setting = new GlobalRandomSetting();
        //-1代表需要随机生成种子
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
/// 全局随机化设定
/// </summary>
[System.Serializable]
public class GlobalRandomSetting
{
    /// <summary>
    /// 总层数数量
    /// </summary>
    public int LayerCount;
    // 全局种子
    public int GlobalSeed;
    /// <summary>
    /// 所有群系流程
    /// </summary>
    public List<BiomeProcessData> biomeProcessDatas;
}
/// <summary>
/// 单个群系流程数据
/// </summary>
public class BiomeProcessData
{
    public E_SceneBiome biome;
    /// <summary>
    /// 层数
    /// </summary>
    public int LayerCount;
}