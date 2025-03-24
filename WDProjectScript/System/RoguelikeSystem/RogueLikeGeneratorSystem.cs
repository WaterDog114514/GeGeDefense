using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ϵͳ 
/// һ������������ɵ�ϵͳ
/// </summary>
public class RogueLikeGeneratorSystem : ManagedSingleton<RogueLikeGeneratorSystem>, IGameSystem, IEventManager<E_GeneratorEvent>
{
    private GlobalRandomSetting globalSetting;
    //ȫ��random
    /// <summary>
    /// �������ģ��
    /// </summary>
    private List<IRogueGenerator> generators;
    private GlobalSettingGenerator globalSettingGenerator;
    public EventManager<E_GeneratorEvent> eventManager { get; private set; }
    /// <summary>
    /// ��ʼ���������ϵͳ
    /// </summary>
    public override void Initialized()
    {
        InitializedAllEmbeddedModules();
        InitializedAllGenerator();
        Debug.Log("���ģ�顪����ʼ���ɹ�");
    }
    private void InitializedAllEmbeddedModules()
    {
        //����ģ���б��ͨѶ��
        generators = new List<IRogueGenerator>();
        eventManager = new EventManager<E_GeneratorEvent>();
        //ȫ���趨������
        globalSettingGenerator = new GlobalSettingGenerator();
    }
    private void InitializedAllGenerator()
    {
        generators.Add(new LevelNodeGenerator());
    }
    /// <summary>
    /// ��ʼ����ȫ���ؿ�����
    /// </summary>
    public RogueGenerateProcessConfig StartGenerate(int globalSeed = -1)
    {
        //������ȫ���趨
        globalSetting = globalSettingGenerator.GenerateGlobalSetting(globalSeed);
        //������ģ�����Config
        foreach (var generator in generators)
        {
           (generator).InitializeRandomizer(globalSetting);
        }
        //���ɽڵ�����
        var levelNodeConfig = eventManager.TriggerRequest<LevelNodeGeneratorConfig>(E_GeneratorEvent.GenerateAllLevelNode);
        var config = new RogueGenerateProcessConfig();
        config.levelNodeGeneratorConfig = levelNodeConfig;
        return config;
    }

    /// <summary>
    /// ����������ȡ�
    /// </summary>
    public void UpdateNextProgress(float progress)
    {
    }

}