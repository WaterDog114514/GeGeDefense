using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 均等随机器
/// </summary>
/// <typeparam name="T"></typeparam>
public class EqualRandomSelector<T>
{
    private List<T> items = new List<T>(); // 存储物品列表
    private System.Random random; // 使用 System.Random 替代 UnityEngine.Random

    /// <summary>
    /// 构造函数，初始化随机数生成器并设置种子
    /// </summary>
    /// <param name="seed">随机种子</param>
    public EqualRandomSelector(int seed)
    {
        random = new System.Random(seed); // 使用种子初始化随机数生成器
    }

    /// <summary>
    /// 添加一个物品
    /// </summary>
    /// <param name="item">物品对象</param>
    public void AddItem(T item)
    {
        items.Add(item);
    }

    /// <summary>
    /// 随机选择一个物品
    /// </summary>
    /// <returns>随机选中的物品</returns>
    public T GetRandomItem()
    {
        if (items.Count == 0)
        {
            throw new InvalidOperationException("没有可选项！");
        }

        // 使用 System.Random 生成随机索引
        int randomIndex = random.Next(0, items.Count);
        return items[randomIndex];
    }
}