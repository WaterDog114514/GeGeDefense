/// <summary>
/// 当前种子计算器
/// </summary>
public class SeedCalculater
{
    /// <summary>
    /// 本次流程的全局种子
    /// </summary>
    private int GlobalSeed;
    /// <summary>
    /// 当前生成层
    /// </summary>
    private int currentLayer;
    /// <summary>
    /// 当前所在层的节点索引
    /// </summary>
    private int currentLayerIndex;

    public SeedCalculater(int globalSeed)
    {
        GlobalSeed = globalSeed;
        currentLayer = 0;
        currentLayerIndex = 0;
    }

    /// <summary>
    /// 计算局部种子。
    /// </summary>
    public int CalculateLocalSeed()
    {
        //计算当前种子
        int localSeed = GlobalSeed + currentLayer * 1000 + currentLayerIndex * 5;
        currentLayerIndex++;
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