using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 全局设定生成器
/// 负责生成全局种子 关卡数量 关卡层数等等
/// </summary>
public class GlobalSettingGenerator
{
    private System.Random GlobalRandom;
    private Dictionary<int,GlobalRandom> _config;
    public   GlobalSettingGenerator()
    {
        GlobalRandom = new System.Random();
        //加载配置文件
        _config = ExcelBinarayLoader.Instance.GetDataContainer<GlobalRandom>();
    }
    /// <summary>
    /// 生成好全局设定 如关卡数量 全局种子
    /// </summary>
    public GlobalRandomSetting GenerateGlobalSetting(int globalSeed = -1)
    {
        GlobalRandomSetting setting = new GlobalRandomSetting();
        //加载必须配置
        setting.MaxDiffcultyLevel = _config[1].MaxDiffcultyLevel;
        setting.TotalBiomeCount = _config[1].TotalBiomeCount;
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
    public int  MaxDiffcultyLevel ;
    public int  TotalBiomeCount;

}
