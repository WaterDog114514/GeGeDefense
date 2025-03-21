/// <summary>
/// �������Ļ��࣬���о���������Ӧ�̳д��ࡣ
/// </summary>
/// <typeparam name="TGeneratorUnit">���ɵĻ�����λ����</typeparam>
public abstract class BaseEmbeddedModuel : IEventManager<E_GeneratorEvent>
{
    public EventManager<E_GeneratorEvent> eventManager => RogueLikeGeneratorSystem.Instance.eventManager;
    public BaseEmbeddedModuel()
    {
        Initialized();
        LoadConfiguration();
    }
    protected abstract void Initialized();
    protected abstract void LoadConfiguration();
}