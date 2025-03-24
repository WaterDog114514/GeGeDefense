// 项目层/RogueSystem/Interfaces/IRogueGenerator.cs
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 关卡场景配置
/// </summary>
[System.Serializable]
public class LevelNodeGeneratorConfig : BaseRogueConfig
{
    public List<SingleLevelBiome> biomes;
    public LevelNodeGeneratorConfig() { 
        biomes = new List<SingleLevelBiome>();
    }
}
/// <summary>
/// 单此群落流程
/// </summary>
[System.Serializable]
public class SingleLevelBiome
{
    public List<SingleLevelLayer> layers;
    public SingleLevelBiome()
    {
        layers = new List<SingleLevelLayer>();  
    }
}
[System.Serializable]
public class SingleLevelLayer
{
    public List<SingleLevelNode> layerNodes;
    public SingleLevelLayer()
    {
        layerNodes = new List<SingleLevelNode>();
    }
}
[System.Serializable]
public class SingleLevelNode : I_ItemRegulator<int>
{
    public int NodeID;
    public int FixedValue { get => NodeID; set => NodeID = value; }
}