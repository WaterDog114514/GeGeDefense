public interface IRogueGenerator
{
     GlobalRandomSetting globalSetting { get; }
    /// <summary>
    /// 生成器初始化
    /// </summary>
    void Initialize();
    void InitializeRandomizer(GlobalRandomSetting setting);

}
public interface IRogueGeneratorModuel<TConfig>:IRogueGenerator where TConfig : BaseRogueConfig
{
    /// <summary>
    /// 生成配置文件。
    /// </summary>
    public abstract TConfig Generate();
}