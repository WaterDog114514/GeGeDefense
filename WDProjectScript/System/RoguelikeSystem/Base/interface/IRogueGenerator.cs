public interface IRogueGenerator
{
     
     GlobalRandomSetting globalSetting { get; }
    /// <summary>
    /// ��������ʼ��
    /// </summary>
    void Initialize();
    void LoadGlobalSetting(GlobalRandomSetting setting);

}
public interface IRogueGeneratorModuel<TUnit, TConfig>:IRogueGenerator where TUnit : BaseGeneratorUnit where TConfig : BaseRogueConfig
{
    /// <summary>
    /// ���������ļ���
    /// </summary>
    public abstract TConfig Generate();
    /// <summary>
    /// ����һ��������λ��
    /// </summary>
    public abstract TUnit GenerateUnit();
}