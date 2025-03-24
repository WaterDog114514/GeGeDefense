public interface IRogueGenerator
{
     GlobalRandomSetting globalSetting { get; }
    /// <summary>
    /// ��������ʼ��
    /// </summary>
    void Initialize();
    void InitializeRandomizer(GlobalRandomSetting setting);

}
public interface IRogueGeneratorModuel<TConfig>:IRogueGenerator where TConfig : BaseRogueConfig
{
    /// <summary>
    /// ���������ļ���
    /// </summary>
    public abstract TConfig Generate();
}