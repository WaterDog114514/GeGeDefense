using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 肉鸽系统 
/// 一个负责随机生成的系统
/// </summary>
public class SceneGenerator : BaseRogueGenerator<SceneGeneratorUnit>
{
    /// <summary>
    /// 当前游戏的地形环境
    /// 由节点随机而定
    /// </summary>
    private E_SceneBiome currentBiome;
    //配置文件
    private Dictionary<int, SceneRandomConfig> SceneGeneratorConfig;
    private RoguelikeRandomizer SceneIDRandomizer;
    public override BaseRogueConfig Generate(int seed)
    {
    }
    public override SceneGeneratorUnit GenerateUnit(int seed)
    {
        throw new System.NotImplementedException();
    }

    public override void Initialize()
    {
        SceneIDRandomizer = new RoguelikeRandomizer();

    }

    public override void LoadConfiguration()
    {
        SceneGeneratorConfig = ExcelBinarayLoader.Instance.GetDataContainer<SceneRandomConfig>();
    }
}
