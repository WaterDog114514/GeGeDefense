// ��Ŀ��/RogueSystem/Interfaces/IRogueGenerator.cs
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �ؿ���������
/// </summary>
public class SceneGeneratorConfig : BaseRogueConfig
{
    public E_SceneBiome Biome;
    public int LeftTerrainID;
    public int RightTerrainID;
    public int UpTerrainID;
    public int DownTerrainID;
    public int LeftUpTerrainID;
    public int RightUpTerrainID;
    public int LeftDownTerrainID;
    public int RightDownTerrainID;
}
