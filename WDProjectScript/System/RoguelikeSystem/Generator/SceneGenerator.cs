//using System.Collections.Generic;
//using UnityEngine;
///// <summary>
///// ���ϵͳ 
///// һ������������ɵ�ϵͳ
///// </summary>
//public class SceneGenerator : BaseRogueGenerator<SceneGeneratorUnit, SceneGeneratorConfig>
//{
//    /// <summary>
//    /// ��ǰ��Ϸ�ĵ��λ���
//    /// �ɽڵ��������
//    /// </summary>
//    private E_SceneBiome currentBiome;
//    //�����ļ�
//    private Dictionary<int, SceneRandomConfig> SceneGeneratorConfig;
//    private RoguelikeRandomizer SceneIDRandomizer;

//    public override SceneGeneratorConfig Generate()
//    {
//        throw new System.NotImplementedException();
//    }

//    public override SceneGeneratorUnit GenerateUnit()
//    {
//        throw new System.NotImplementedException();
//    }

//    public override void Initialize()
//    {
//        SceneIDRandomizer = new RoguelikeRandomizer();

//    }

//    public override void LoadGenerateConfiguration()
//    {
//        SceneGeneratorConfig = ExcelBinarayLoader.Instance.GetDataContainer<SceneRandomConfig>();
//    }
//}
