/// <summary>
/// �������Ļ��࣬���о���������Ӧ�̳д��ࡣ
/// </summary>
/// <typeparam name="TGeneratorUnit">���ɵĻ�����λ����</typeparam>
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
    /// ���������ļ�
    /// </summary>
    public abstract void LoadGenerateConfiguration();
    /// <summary>
    /// ��ʼ����������
    /// </summary>
    public abstract void Initialize();

    public abstract TConfig Generate();
    public abstract TUnit GenerateUnit();
    /// <summary>
    /// ��������õ�ȫ���趨
    /// </summary>
    /// <param name="setting"></param>
    public virtual void LoadGlobalSetting(GlobalRandomSetting setting)
    {
        globalSetting = setting;
        seedCalculater = new SeedCalculater(setting.GlobalSeed);
    }

}