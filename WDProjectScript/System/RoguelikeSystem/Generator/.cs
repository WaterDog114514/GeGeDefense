/// <summary>
/// 当前种子计算器
/// </summary>
public class SeedCalculater
{
    /// <summary>
    /// 本次流程的全局种子
    /// </summary>
    private int GlobalSeed;
    private int Offset;
    /// <summary>
    /// 当前关卡节点索引
    /// </summary>
    private int LevelNodeIndex;
    public SeedCalculater(int globalSeed, int offset)
    {
        GlobalSeed = globalSeed;
        this.Offset = offset;
    }

    /// <summary>
    /// 计算局部种子。
    /// </summary>
    public int CalculateLocalSeed()
    {
        //计算公式
        int localSeed = GlobalSeed + LevelNodeIndex * 500 + Offset * 5;
        LevelNodeIndex++;
        return localSeed;
    }
    /// <summary>
    /// 上一层楼
    /// </summary>
    public void UpdateNextLayer()
    {
        currentLayer++;
        currentLayerIndex = 0;
    }
}