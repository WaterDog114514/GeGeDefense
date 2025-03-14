using UnityEngine;

public class LevelSystem : ManagedSingleton<LevelSystem>
{
    /// <summary>
    /// �����ڲ�ϵͳͨ��
    /// </summary>
    public EventManager<E_LevelSystemEvent> eventManager;

    public override void Initialized()
    {
        eventManager = new EventManager<E_LevelSystemEvent>();
    }
}
