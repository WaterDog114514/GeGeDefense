using UnityEngine;

/// <summary>
/// ������ؿ�����
/// �ؿ��������õĵ�λ
/// </summary>
[System.Serializable]
public class SceneGeneratorUnit : BaseGeneratorUnit
{
    /// <summary>
    /// ��Ӧ����ID
    /// </summary>
    public int SceneID;
    public E_TileDirection Direction;
    public E_SceneBiome Enviroment;

    /// <summary>
    /// �ĸ��ؿ�Ķ�λ������ͨ����ת����
    /// </summary>
    public enum E_TileDirection
    {
        Left_Right,
        LeftUp_RightDown,
        LeftDown_RightUp,
        Up_Down,
    }
}
