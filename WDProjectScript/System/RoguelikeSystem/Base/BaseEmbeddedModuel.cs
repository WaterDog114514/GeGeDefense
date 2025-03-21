/// <summary>
/// 生成器的基类，所有具体生成器应继承此类。
/// </summary>
/// <typeparam name="TGeneratorUnit">生成的基本单位类型</typeparam>
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