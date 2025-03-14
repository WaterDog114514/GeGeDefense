// 项目层/RogueSystem/Interfaces/IRogueGenerator.cs
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单个大地块配置
/// 关卡场景配置的单位
/// </summary>
[System.Serializable]
public struct SceneTileData
{
    // 对应场景ID
    public int SceneID;       
    public Vector2 position;    // 相对坐标
    public int connectionCount; // 连接分支数
}
public enum E_SceneTileDirection
{

}