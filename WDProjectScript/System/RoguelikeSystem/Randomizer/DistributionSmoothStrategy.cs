// ��Ŀ��/WDProjectCore/Strategies/DistributionSmoothStrategy.cs
using System.Collections.Generic;

public class DistributionSmoothStrategy<T> : IWeightAdjustStrategy<T>
{
    private int _historySize = 3; // ������3������

    public Dictionary<T, float> Adjust(Dictionary<T, float> currentWeights,
                                      GenerationContext context)
    {
        // ��ȡ������ɼ�¼
        var recentTypes = context.GetRecentGenerations<T>(_historySize);

        // �ͷ�������ֹ�������
        foreach (var type in recentTypes)
        {
            if (currentWeights.ContainsKey(type))
            {
                currentWeights[type] *= 0.3f; // ���ֹ�������Ȩ�ؽ���70%
            }
        }
        return currentWeights;
    }
}