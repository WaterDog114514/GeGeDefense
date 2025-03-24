/// <summary>
/// �������Ļ��࣬���о���������Ӧ�̳д��ࡣ
/// </summary>
/// <typeparam name="TGeneratorUnit">���ɵĻ�����λ����</typeparam>
public abstract class BaseRogueGenerator<TConfig> : IEventManager<E_GeneratorEvent>, IRogueGeneratorModuel<TConfig> where TConfig : BaseRogueConfig
{
    public GlobalRandomSetting globalSetting { get; private set; }
    public SeedCalculater seedCalculater { get; protected set; }

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
    /// <summary>
    /// ��������õ�ȫ���趨
    /// </summary>
    /// <param name="setting"></param>
    public abstract void InitializeRandomizer(GlobalRandomSetting setting);
}