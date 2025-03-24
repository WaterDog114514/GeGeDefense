using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ϵͳ 
/// һ������������ɵ�ϵͳ
/// </summary>
public class LevelNodeGenerator : BaseRogueGenerator< LevelNodeGeneratorConfig>
{
    /// <summary>
    /// �ؿ��ڵ������ļ�
    /// </summary>
    private Dictionary<int, LevelNodeConfiguration> _LevelNodeConfig;
    /// <summary>
    /// �ڵ�id���������ͬ���͵�ͼ�ڵ��Ӧ��ͬid
    /// </summary>
    private Randomizer<int> NodeIDRandomizer;
    public override LevelNodeGeneratorConfig Generate()
    {
        var generateConfig = InitializeGeneratorConfig();
        //���̵�Ⱥ��biome���ɣ�һ����������3��
        for (int biomeIndex = 1; biomeIndex <= _LevelNodeConfig.Count; biomeIndex++)
        {
            //��ǰȺ���������Ϣ
            var biomeData = new SingleLevelBiome();
            //��ǰȺ�����̵������ļ�
            var biomeConfig = LoadBiomeConfiguration(biomeIndex);
            //�������������Ҫ����
            int totalGeneratorCount = biomeConfig.TotalLayerCount / biomeConfig.SubdivisionLayerCount;
            //���㵥�����ɵĽڵ���
            int singleGeneratorNodeCount = biomeConfig.SubdivisionLayerCount * biomeConfig.SingleLayerNodeCount;
            //���� �ֲ�������
            for (int generateIndex = 0; generateIndex < totalGeneratorCount; generateIndex++)
            {
                //���ɵ��νڵ�
                var NodesList = NodeIDRandomizer.GetProportionRandom(singleGeneratorNodeCount);
                //������������
                int NodeIDIndex = 0;
                //�������
                for (int layerIndex = 0; layerIndex < biomeConfig.SubdivisionLayerCount; layerIndex++)
                {
                    //��ǰ�����Ϣ
                    var layerData = new SingleLevelLayer();
                    //����ڵ�����
                    for (int NodeIndex = 0; NodeIndex < biomeConfig.SingleLayerNodeCount; NodeIndex++)
                    {
                        //��ǰ�ڵ�
                        var nodeData = new SingleLevelNode();
                        nodeData.NodeID = NodesList[NodeIDIndex];
                        NodeIDIndex++;
                        //��ǰ�����˽ڵ�
                        layerData.layerNodes.Add(nodeData);
                    }
                    //���뵱ǰ��
                    biomeData.layers.Add(layerData);
                }
            }
            //����Ⱥϵ���̼��뵱ǰȺϵ
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
        //ע�����ɹؿ��¼�
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
    /// ����һ��biome�����̵�����
    /// </summary>
    /// <param name="PageIndex"></param>
    /// <returns></returns>
    private LevelNodeConfiguration LoadBiomeConfiguration(int PageIndex)
    {
        var config = _LevelNodeConfig[PageIndex];
        //��������ڵ��������
        NodeIDRandomizer.ClearItems();
        NodeIDRandomizer.AddItem((int)E_LevelNodeType.Battle, config.BattleNodeRatio);
        NodeIDRandomizer.AddItem((int)E_LevelNodeType.Event, config.RandomEventRatio);
        NodeIDRandomizer.AddItem((int)E_LevelNodeType.Shop, config.ShopRatio);
        //��������������
        return config;
    }
}
