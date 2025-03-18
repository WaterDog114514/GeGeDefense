using UnityEngine;

/// <summary>
/// 单个大地块配置
/// 关卡场景配置的单位
/// </summary>
[System.Serializable]
public class SceneGeneratorUnit : BaseGeneratorUnit
{
    /// <summary>
    /// 对应场景ID
    /// </summary>
    public int SceneID;
    public E_TileDirection Direction;
    public E_SceneBiome Enviroment;

    /// <summary>
    /// 四个地块的对位，后期通过反转即可
    /// </summary>
    public enum E_TileDirection
    {
        Left_Right,
        LeftUp_RightDown,
        LeftDown_RightUp,
        Up_Down,
    }
}
