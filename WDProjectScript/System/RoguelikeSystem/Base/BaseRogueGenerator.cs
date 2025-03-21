/// <summary>
/// 生成器的基类，所有具体生成器应继承此类。
/// </summary>
/// <typeparam name="TGeneratorUnit">生成的基本单位类型</typeparam>
public abstract class BaseRogueGenerator<TUnit, TConfig> : IEventManager<E_GeneratorEvent>, IRogueGeneratorModuel<TUnit, TConfig> where TUnit : BaseGeneratorUnit where TConfig : BaseRogueConfig
{
    public GlobalRandomSetting globalSetting { get; private set; }
    public SeedCalculater seedCalculater { get; private set; }

    public EventManager<E_GeneratorEvent> eventManager => RogueLikeGeneratorSystem.Instance.eventManager;

    public BaseRogueGenerator()
    {
        Initialize();
        LoadGenerateConfiguration();
    }
    /// <summary>
    /// 加载配置文件
    /// </summary>
    public abstract void LoadGenerateConfiguration();
    /// <summary>
    /// 初始化生成器。
    /// </summary>
    public abstract void Initialize();

    public abstract TConfig Generate();
    public abstract TUnit GenerateUnit();
    /// <summary>
    /// 加载随机好的全局设定
    /// </summary>
    /// <param name="setting"></param>
    public virtual void LoadGlobalSetting(GlobalRandomSetting setting)
    {
        globalSetting = setting;
        seedCalculater = new SeedCalculater(setting.GlobalSeed);
    }

}