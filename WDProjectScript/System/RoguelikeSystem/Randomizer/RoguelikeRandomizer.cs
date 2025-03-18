using System.Collections.Generic;

/// <summary>
/// 统一随机化器，支持均等随机和加权随机。
/// 主要对Unit的字段进行随机
/// </summary>
/// <typeparam name="T">随机化类型（如 int、enum 等）</typeparam>
public class RoguelikeRandomizer
{
    private bool isUseWeightedRandom = false;
    // 加权随机选择器
    private WeightedRandomSelector<int> weightedSelector;

    /// <summary>
    /// 构造函数。
    /// </summary>
    /// <param name="seed">随机种子</param>
    /// <param name="useWeightedRandom">是否使用加权随机</param>
    public RoguelikeRandomizer(bool useWeightedRandom = true)
    {
        isUseWeightedRandom = useWeightedRandom;
    }
    /// <summary>
    /// 获取下一个随机值。
    /// </summary>
    public int GetNext()
    {
        return weightedSelector.GetRandomItem();
    }
    /// <summary>
    /// 大规模重置
    /// </summary>
    /// <param name="seed"></param>
    /// <param name="items"></param>
    /// <param name="useWeightedRandom"></param>
    public void Reset(int seed, Dictionary<int, float> items, bool useWeightedRandom = false)
    {
        isUseWeightedRandom = useWeightedRandom;
        ResetSeed(seed);
        ResetItems(items);
    }

    /// <summary>
    /// 重置随机化器的种子
    /// </summary>
    /// <param name="seed">新的随机种子</param>
    public void ResetSeed(int seed)
    {
        weightedSelector.ResetSeed(seed);
    }
    public void ResetItems(Dictionary<int, float> items)
    {
        //若为均等随机，理应所有权重一致
        if (!isUseWeightedRandom)
        {
            foreach (var key in items.Keys)
            {
                items[key] = 1;
            }
        }
        //先置空，后慢慢添加
        weightedSelector.ClearItems();
        foreach (var key in items.Keys)
        {
            weightedSelector.AddItem(key, items[key]);
        }

    }
}