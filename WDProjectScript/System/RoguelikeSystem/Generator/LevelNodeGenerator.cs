using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 肉鸽系统 
/// 一个负责随机生成的系统
/// </summary>
public class LevelNodeGenerator : BaseRogueGenerator< LevelNodeGeneratorConfig>
{
    /// <summary>
    /// 关卡节点配置文件
    /// </summary>
    private Dictionary<int, LevelNodeConfiguration> _LevelNodeConfig;
    /// <summary>
    /// 节点id随机器，不同类型地图节点对应不同id
    /// </summary>
    private Randomizer<int> NodeIDRandomizer;
    public override LevelNodeGeneratorConfig Generate()
    {
        var generateConfig = InitializeGeneratorConfig();
        //流程的群落biome生成，一共可能生成3次
        for (int biomeIndex = 1; biomeIndex <= _LevelNodeConfig.Count; biomeIndex++)
        {
            //当前群落的数据信息
            var biomeData = new SingleLevelBiome();
            //当前群落流程的配置文件
            var biomeConfig = LoadBiomeConfiguration(biomeIndex);
            //计算生成完毕需要次数
            int totalGeneratorCount = biomeConfig.TotalLayerCount / biomeConfig.SubdivisionLayerCount;
            //计算单次生成的节点数
            int singleGeneratorNodeCount = biomeConfig.SubdivisionLayerCount * biomeConfig.SingleLayerNodeCount;
            //按层 分步骤生成
            for (int generateIndex = 0; generateIndex < totalGeneratorCount; generateIndex++)
            {
                //生成单次节点
                var NodesList = NodeIDRandomizer.GetProportionRandom(singleGeneratorNodeCount);
                //用来计数索引
                int NodeIDIndex = 0;
                //逐层增加
                for (int layerIndex = 0; layerIndex < biomeConfig.SubdivisionLayerCount; layerIndex++)
                {
                    //当前层的信息
                    var layerData = new SingleLevelLayer();
                    //单层节点生成
                    for (int NodeIndex = 0; NodeIndex < biomeConfig.SingleLayerNodeCount; NodeIndex++)
                    {
                        //当前节点
                        var nodeData = new SingleLevelNode();
                        nodeData.NodeID = NodesList[NodeIDIndex];
                        NodeIDIndex++;
                        //当前层加入此节点
                        layerData.layerNodes.Add(nodeData);
                    }
                    //加入当前层
                    biomeData.layers.Add(layerData);
                }
            }
            //所有群系流程加入当前群系
            generateConfig.biomes.Add(biomeData);

        }
        return generateConfig;
    }
    private LevelNodeGeneratorConfig InitializeGeneratorConfig()
    {
        var generateConfig = new LevelNodeGeneratorConfig();
        generateConfig.biomes = new List<SingleLevelBiome>();
        return generateConfig;
    }

    public override void Initialize()
    {
        //注册生成关卡事件
        eventManager.AddRequestListener(E_GeneratorEvent.GenerateAllLevelNode,Generate);
    }
    public override void InitializeRandomizer(GlobalRandomSetting setting)
    {
       // seedCalculater = new SeedCalculater(setting.GlobalSeed, 1145);
        NodeIDRandomizer = new Randomizer<int>(setting.GlobalSeed + 1145);
    }
    public override void LoadGenerateConfiguration()
    {
        _LevelNodeConfig = ExcelBinarayLoader.Instance.GetDataContainer<LevelNodeConfiguration>();
    }
    /// <summary>
    /// 加载一次biome的流程的配置
    /// </summary>
    /// <param name="PageIndex"></param>
    /// <returns></returns>
    private LevelNodeConfiguration LoadBiomeConfiguration(int PageIndex)
    {
        var config = _LevelNodeConfig[PageIndex];
        //进行随机节点比例调整
        NodeIDRandomizer.ClearItems();
        NodeIDRandomizer.AddItem((int)E_LevelNodeType.Battle, config.BattleNodeRatio);
        NodeIDRandomizer.AddItem((int)E_LevelNodeType.Event, config.RandomEventRatio);
        NodeIDRandomizer.AddItem((int)E_LevelNodeType.Shop, config.ShopRatio);
        //进行修正器调整
        return config;
    }
}
