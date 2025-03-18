// 项目层/WDProjectCore/Strategies/ProportionGuaranteeStrategy.cs
using System.Collections.Generic;
/// <summary>
/// 关键保底机制实现 比例保底策略
/// </summary>
/// <typeparam name="T"></typeparam>
public class ProportionGuaranteeStrategy<T> : IWeightAdjustStrategy<T>
{
    private Dictionary<T, float> _targetProportions;
    private int _totalGenerated;
    private Dictionary<T, int> _countMap = new Dictionary<T, int>();

    public ProportionGuaranteeStrategy(Dictionary<T, float> targetProportions)
    {
        _targetProportions = targetProportions;
    }

    public Dictionary<T, float> Adjust(Dictionary<T, float> currentWeights,
                                      GenerationContext context)
    {
        // 统计当前比例
        foreach (var type in currentWeights.Keys)
        {
            _countMap.TryGetValue(type, out var count);
            float currentRatio = (float)count / _totalGenerated;

            // 计算比例差值
            float diff = _targetProportions[type] - currentRatio;
            // 动态调整权重（差值越大，权重增幅越高）
            currentWeights[type] *= (1 + diff * 2);
        }
        return currentWeights;
    }
}