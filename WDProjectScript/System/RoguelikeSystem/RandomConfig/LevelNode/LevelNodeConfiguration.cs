[System.Serializable]
public class LevelNodeConfiguration : ExcelConfiguration
{
	/// <summary>
	///一个流程有多个群系，必须从1开始
	/// </summary>
	public int BiomeIndex;
	public int TotalLayerCount;
	public int SingleLayerNodeCount;
	/// <summary>
	///每隔几层上升一难度
	/// </summary>
	public int DiffcultyLayerInterval;
	/// <summary>
	///每层的细分比例确保间隔层内的比例严格相等
	/// </summary>
	public int SubdivisionLayerCount;
	public int BattleNodeRatio;
	public int RandomEventRatio;
	public int ShopRatio;
}
