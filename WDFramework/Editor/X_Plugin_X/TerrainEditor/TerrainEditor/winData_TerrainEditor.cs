using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class winData_TerrainEditor : BaseWindowData
{

    [SerializeField]
    public Dictionary<string, SerializableColor> dic_CellColorSetting = new Dictionary<string, SerializableColor>();
    public int QuadTreeSize;
    public int MaxDepth;

    //重要窗口参数
    public SerializableRect LeftPanelRect;
    public SerializableRect RightPanelRect;

}
