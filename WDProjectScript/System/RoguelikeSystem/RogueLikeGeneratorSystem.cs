using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 肉鸽系统 
/// 一个负责随机生成的系统
/// </summary>
public class RogueLikeGeneratorSystem : ManagedSingleton<RogueLikeGeneratorSystem>, IGameSystem, IEventManager<E_GeneratorEvent>
{
    private GlobalRandomSetting globalSetting;
    //全局random
    /// <summary>
    /// 各个肉鸽模块
    /// </summary>
    private List<IRogueGenerator> generators;
    private GlobalSettingGenerator globalSettingGenerator;
    public EventManager<E_GeneratorEvent> eventManager { get; private set; }
    /// <summary>
    /// 初始化整个肉鸽系统
    /// </summary>
    public override void Initialized()
    {
        InitializedAllEmbeddedModules();
        InitializedAllGenerator();
        Debug.Log("肉鸽模块——初始化成功");
    }
    private void InitializedAllEmbeddedModules()
    {
        //创建模块列表和通讯器
        generators = new List<IRogueGenerator>();
        eventManager = new EventManager<E_GeneratorEvent>();
        //全局设定生成器
        globalSettingGenerator = new GlobalSettingGenerator();
    }
    private void InitializedAllGenerator()
    {
        generators.Add(new LevelNodeGenerator());
    }
    /// <summary>
    /// 开始生成全部关卡流程
    /// </summary>
    public RogueGenerateProcessConfig StartGenerate(int globalSeed = -1)
    {
        //先生成全局设定
        globalSetting = globalSettingGenerator.GenerateGlobalSetting(globalSeed);
        //给所有模块加载Config
        foreach (var generator in generators)
        {
           (generator).InitializeRandomizer(globalSetting);
        }
        //生成节点配置
        var levelNodeConfig = eventManager.TriggerRequest<LevelNodeGeneratorConfig>(E_GeneratorEvent.GenerateAllLevelNode);
        var config = new RogueGenerateProcessConfig();
        config.levelNodeGeneratorConfig = levelNodeConfig;
        return config;
    }

    /// <summary>
    /// 更新随机进度。
    /// </summary>
    public void UpdateNextProgress(float progress)
    {
    }

}