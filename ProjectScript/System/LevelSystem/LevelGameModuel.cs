using UnityEngine;
//�ؿ�ϵͳ��ģ�����
public abstract class LevelGameModuel : internalGameModuel, I_InternalSystemEventModue<E_LevelSystemEvent>
{
    //�󶨴�ģ���ͨ����
    public EventManager<E_LevelSystemEvent> eventManager => LevelSystem.Instance.eventManager;
}
