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
    private List<IRogueSystemModuel> moduels;
    private GlobalSettingGenerator globalSettingGenerator;
    public EventManager<E_GeneratorEvent> eventManager { get; private set; }
    /// <summary>
    /// 初始化整个肉鸽系统
    /// </summary>
    public override void Initialized()
    {
        //创建模块列表和通讯器
        moduels = new List<IRogueSystemModuel>();
        eventManager = new EventManager<E_GeneratorEvent>();
        //全局设定生成器
        globalSettingGenerator = new GlobalSettingGenerator();
        globalSettingGenerator.Initialize();
        Debug.Log("肉鸽模块——初始化成功");
    }
    public void LoadConfig()
    {

    }
    /// <summary>
    /// 开始生成全部关卡流程
    /// </summary>
    public void StartGenerate(int globalSeed = -1)
    {
        //先生成全局设定
        globalSetting = globalSettingGenerator.GenerateGlobalSetting(globalSeed);
    }

    /// <summary>
    /// 生成所有关卡
    /// </summary>
    /// <param name="globalSeed"></param>
    private void GenerateAllLevel()
    {
        //按群系流程生成
        for (int i = 0; i < globalSetting.biomeProcessDatas.Count; i++)
        {

        }
    }

    /// <summary>
    /// 更新随机进度。
    /// </summary>
    public void UpdateNextProgress(float progress)
    {
        _randomProgress = Mathf.Clamp(progress, 0, 100);
    }

    /// <summary>
    /// 计算局部种子。
    /// </summary>
    private int CalculateLocalSeed(int offset)
    {
        return _globalSeed + offset + (int)(_randomProgress * 100);
    }

    /// <summary>
    /// 初始化各个肉鸽系统各个模块
    /// </summary>
    public void InitializeGeneratorModules()
    {
        //全局种子管理器
        moduels.Add(new SeedManager());
        //场景生成器
        moduels.Add(new SceneGenerator());
        //每个模块初始化
        foreach (var moduel in moduels)
        {
            moduel.Initialize();
        }
    }
}
