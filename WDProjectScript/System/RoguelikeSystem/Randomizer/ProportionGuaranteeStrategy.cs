// ��Ŀ��/WDProjectCore/Strategies/ProportionGuaranteeStrategy.cs
using System.Collections.Generic;
/// <summary>
/// �ؼ����׻���ʵ�� �������ײ���
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
        // ͳ�Ƶ�ǰ����
        foreach (var type in currentWeights.Keys)
        {
            _countMap.TryGetValue(type, out var count);
            float currentRatio = (float)count / _totalGenerated;

            // ���������ֵ
            float diff = _targetProportions[type] - currentRatio;
            // ��̬����Ȩ�أ���ֵԽ��Ȩ������Խ�ߣ�
            currentWeights[type] *= (1 + diff * 2);
        }
        return currentWeights;
    }
}