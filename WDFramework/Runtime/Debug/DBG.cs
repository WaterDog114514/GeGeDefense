using UnityEngine;
/// <summary>
/// ���Debug���������
/// </summary>
public static class DBG
{
    public static void Log(object message)
    {
#if UNITY_EDITOR
        Debug.Log(message);
#endif
        //�༭�������в�ͬ����
    }
}
