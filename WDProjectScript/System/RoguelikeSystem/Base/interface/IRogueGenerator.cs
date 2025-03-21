public interface IRogueGenerator
{
     
     GlobalRandomSetting globalSetting { get; }
    /// <summary>
    /// 生成器初始化
    /// </summary>
    void Initialize();
    void LoadGlobalSetting(GlobalRandomSetting setting);

}
public interface IRogueGeneratorModuel<TUnit, TConfig>:IRogueGenerator where TUnit : BaseGeneratorUnit where TConfig : BaseRogueConfig
{
    /// <summary>
    /// 生成配置文件。
    /// </summary>
    public abstract TConfig Generate();
    /// <summary>
    /// 生成一个基本单位。
    /// </summary>
    public abstract TUnit GenerateUnit();
}