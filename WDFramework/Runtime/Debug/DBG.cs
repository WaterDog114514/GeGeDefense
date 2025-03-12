using UnityEngine;
/// <summary>
/// 快捷Debug的另类表现
/// </summary>
public static class DBG
{
    public static void Log(object message)
    {
#if UNITY_EDITOR
        Debug.Log(message);
#endif
        //编辑器和运行不同表现
    }
}
