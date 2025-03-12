using UnityEngine;

public class LevelSystem : ManagedSingleton<LevelSystem>
{
    /// <summary>
    /// 用于内部系统通信
    /// </summary>
    public EventManager<E_LevelSystemEvent> eventManager;

    public override void Initialized()
    {
        eventManager = new EventManager<E_LevelSystemEvent>();
    }
}
