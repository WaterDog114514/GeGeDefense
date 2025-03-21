using System;
using UnityEngine;
/// <summary>
/// 肉鸽系统内嵌式模块
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRogueEmbedModuel 
{
    void Initialized();
    void LoadConfiguration();
}
