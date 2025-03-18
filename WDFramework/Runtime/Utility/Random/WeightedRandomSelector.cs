using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 加权随机选择器（Weighted Random Selector）
/// 适用于在多个元素中，根据各自的权重值进行随机抽取。
/// </summary>
/// <typeparam name="T">泛型 T，表示可以是任何类型（如字符串、整数、类对象等）。</typeparam>
public class WeightedRandomSelector<T> : RandomSelector
{
    // 存储物品及其权重
    private Dictionary<T, float> itemWeights = new Dictionary<T, float>();
    // 记录所有权重的总和
    private float totalWeight = 0;
    /// <summary>
    /// 构造函数，初始化随机数生成器并设置种子
    /// </summary>
    /// <param name="seed">随机种子</param>
    public WeightedRandomSelector(int seed) : base(seed)
    {
    }
    /// <summary>
    /// 添加一个物品，并指定其权重。
    /// </summary>
    /// <param name="item">物品对象</param>
    /// <param name="weight">该物品的权重，必须大于0</param>
    public void AddItem(T item, float weight)
    {
        if (weight <= 0)
        {
            Debug.LogWarning("权重必须大于0！");
            return;
        }

        if (itemWeights.ContainsKey(item))
        {
            // 如果物品已存在，则更新权重
            totalWeight -= itemWeights[item]; // 先移除旧权重
            itemWeights[item] = weight;
        }
        else
        {
            // 否则，添加新物品
            itemWeights.Add(item, weight);
        }

        totalWeight += weight; // 更新总权重
    }

    /// <summary>
    /// 根据权重随机选择一个物品
    /// </summary>
    /// <returns>随机选中的物品</returns>
    public T GetRandomItem()
    {
        if (itemWeights.Count == 0)
        {
            throw new InvalidOperationException("没有可选项！");
        }

        // 使用 System.Random 生成随机值
        float randomValue = (float)random.NextDouble() * totalWeight;
        float cumulativeSum = 0;

        // 遍历字典中的物品，并根据累积权重判断选中哪一个
        foreach (var item in itemWeights)
        {
            cumulativeSum += item.Value; // 累加当前物品的权重
            if (randomValue < cumulativeSum)
            {
                return item.Key; // 选中该物品
            }
        }

        return default; // 理论上不会到这里，除非计算错误
    }
    public void ClearItems()
    {
        itemWeights.Clear();
    }
}