using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 随化比例调节
/// </summary>
/// <typeparam name="T"></typeparam>
public class RandomRegulator<T> where T : IComparable<T>
{
    // 使用 System.Random 替代 UnityEngine.Random
    protected System.Random random;
    // 存储物品及其权重
    public List<I_ItemRegulator<T>> items { private set; get; }
    // 修正权重
    private Dictionary<T,int> TargetFixedWeight;
    /// <summary>
    /// 权重划分
    /// </summary>
    private Dictionary<T,int> ItemsWeight;

    /// <summary>
    /// 构造函数，初始化随机数生成器并设置种子
    /// </summary>
    /// <param name="seed">随机种子</param>
    public RandomRegulator(int seed)
    {
        random = new System.Random(seed);
        items = new List<I_ItemRegulator<T>>();
    }

    public void AdjustToTargetWeight()
    {
        // 先划分比例
        CalculateCurrentWeight();

        // 计算偏移差
        var deviations = new Dictionary<T, float>();
        int totalItems = items.Count;

        foreach (var target in TargetFixedWeight)
        {
            float currentRatio = ItemsWeight.ContainsKey(target.Key)
                ? (float)ItemsWeight[target.Key] / totalItems
                : 0f;
            float targetRatio = (float)target.Value / totalItems;
            deviations[target.Key] = targetRatio - currentRatio;
        }

        // 调整 item
        AdjustCurrentItems(deviations);
    }
    private void CalculateCurrentWeight()
    {
        ItemsWeight.Clear();

        foreach (var item in items)
        {
            if (ItemsWeight.ContainsKey(item.FixedValue))
            {
                ItemsWeight[item.FixedValue]++;
            }
            else
            {
                ItemsWeight.Add(item.FixedValue, 1);
            }
        }
    }
    /// <summary>
    /// 调整当前item
    /// </summary>
    private void AdjustCurrentItems(Dictionary<T, float> deviations)
    {
        foreach (var deviation in deviations)
        {
            if (deviation.Value > 0)
            {
                // 需要增加该类型的数量
                int itemsToAdjust = (int)(deviation.Value * items.Count);

                for (int i = 0; i < itemsToAdjust; i++)
                {
                    // 随机选择一个非目标类型的 item
                    var candidates = items
                        .Where(x => !x.FixedValue.Equals(deviation.Key))
                        .ToList();

                    if (candidates.Count == 0) break; // 无可调整项

                    int index = random.Next(candidates.Count);
                    var itemToAdjust = candidates[index];

                    // 设置值为目标值
                    itemToAdjust.FixedValue = deviation.Key;

                    // 更新统计
                    ItemsWeight[itemToAdjust.FixedValue]--;
                    ItemsWeight[deviation.Key]++;
                }
            }
        }
    }
    /// <summary>
    /// 添加一个 item。
    /// </summary>
    public void AddAdjustItem(I_ItemRegulator<T> item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);
        }
    }
    /// <summary>
    /// 添加目标权重。
    /// </summary>
    public void AddTargetWeight(T item, int weight)
    {
        if (TargetFixedWeight.ContainsKey(item))
        {
            TargetFixedWeight[item] = weight;
        }
        else
        {
            TargetFixedWeight.Add(item, weight);
        }
    }
    /// <summary>
    /// 移除随机物品
    /// </summary>
    /// <param name="item"></param>
    public void RemoveItem(T item)
    {
        
    }
    public void ResetSeed(int seed)
    {
        random = new System.Random(seed);
    }
    /// <summary>
    /// 清空所有物品
    /// </summary>
    public void ClearItems()
    {
        items.Clear();
     
    }
}