using System;
using UnityEngine;


[Serializable]
public class winData_TerrainCreator : BaseWindowData
{
    //新地形文件名
    public string newFileName;
    //上次保存文件夹地址
    public string LastSaveDirectoryPath;
    [NonSerialized]
    public GameObject prefab;
    /// <summary>
    /// 生成几X几的
    /// </summary>
    public int CreateNumber;
    public float Size;
    [NonSerialized]
    public string newCopyDirectoryPath;
    public string[] newDatasPath;
    [NonSerialized]
    public TerrainData[] terrainDatas;
    public override void IntiFirstCreate()
    {

    }


}
