// ��Ŀ��/RogueSystem/Interfaces/IRogueGenerator.cs
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������ؿ�����
/// �ؿ��������õĵ�λ
/// </summary>
[System.Serializable]
public struct SceneTileData
{
    // ��Ӧ����ID
    public int SceneID;       
    public Vector2 position;    // �������
    public int connectionCount; // ���ӷ�֧��
}
public enum E_SceneTileDirection
{

}