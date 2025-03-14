using UnityEngine;
//关卡系统的模块基类
public abstract class LevelGameModuel : internalGameModuel, I_InternalSystemEventModue<E_LevelSystemEvent>
{
    //绑定此模块的通信器
    public EventManager<E_LevelSystemEvent> eventManager => LevelSystem.Instance.eventManager;
}
