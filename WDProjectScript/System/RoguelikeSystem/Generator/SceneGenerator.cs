using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ϵͳ 
/// һ������������ɵ�ϵͳ
/// </summary>
public class SceneGenerator : BaseRogueGenerator<SceneGeneratorUnit>
{
    /// <summary>
    /// ��ǰ��Ϸ�ĵ��λ���
    /// �ɽڵ��������
    /// </summary>
    private E_SceneBiome currentBiome;
    //�����ļ�
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
