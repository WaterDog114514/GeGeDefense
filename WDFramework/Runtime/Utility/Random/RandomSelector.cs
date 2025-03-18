using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 均等随机器
/// </summary>
/// <typeparam name="T"></typeparam>
public class RandomSelector
{ 
    // 使用 System.Random 替代 UnityEngine.Random
    protected System.Random random;
    /// <summary>
    /// 构造函数，初始化随机数生成器并设置种子
    /// </summary>
    /// <param name="seed">随机种子</param>
    public RandomSelector(int seed)
    {
        random = new System.Random(seed); // 使用种子初始化随机数生成器
    }
    /// <summary>
    /// 重置种子
    /// </summary>
    /// <param name="seed"></param>
    public void ResetSeed(int seed)
    {
        random = new System.Random(seed);
    }
}