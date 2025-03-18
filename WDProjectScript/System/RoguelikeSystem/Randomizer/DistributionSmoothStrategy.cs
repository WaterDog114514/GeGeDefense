// 项目层/WDProjectCore/Strategies/DistributionSmoothStrategy.cs
using System.Collections.Generic;

public class DistributionSmoothStrategy<T> : IWeightAdjustStrategy<T>
{
    private int _historySize = 3; // 检查最近3次生成

    public Dictionary<T, float> Adjust(Dictionary<T, float> currentWeights,
                                      GenerationContext context)
    {
        // 获取最近生成记录
        var recentTypes = context.GetRecentGenerations<T>(_historySize);

        // 惩罚最近出现过的类型
        foreach (var type in recentTypes)
        {
            if (currentWeights.ContainsKey(type))
            {
                currentWeights[type] *= 0.3f; // 出现过的类型权重降低70%
            }
        }
        return currentWeights;
    }
}