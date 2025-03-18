using System.Collections.Generic;

/// <summary>
/// ͳһ���������֧�־�������ͼ�Ȩ�����
/// ��Ҫ��Unit���ֶν������
/// </summary>
/// <typeparam name="T">��������ͣ��� int��enum �ȣ�</typeparam>
public class RoguelikeRandomizer
{
    private bool isUseWeightedRandom = false;
    // ��Ȩ���ѡ����
    private WeightedRandomSelector<int> weightedSelector;

    /// <summary>
    /// ���캯����
    /// </summary>
    /// <param name="seed">�������</param>
    /// <param name="useWeightedRandom">�Ƿ�ʹ�ü�Ȩ���</param>
    public RoguelikeRandomizer(bool useWeightedRandom = true)
    {
        isUseWeightedRandom = useWeightedRandom;
    }
    /// <summary>
    /// ��ȡ��һ�����ֵ��
    /// </summary>
    public int GetNext()
    {
        return weightedSelector.GetRandomItem();
    }
    /// <summary>
    /// ���ģ����
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
    /// �����������������
    /// </summary>
    /// <param name="seed">�µ��������</param>
    public void ResetSeed(int seed)
    {
        weightedSelector.ResetSeed(seed);
    }
    public void ResetItems(Dictionary<int, float> items)
    {
        //��Ϊ�����������Ӧ����Ȩ��һ��
        if (!isUseWeightedRandom)
        {
            foreach (var key in items.Keys)
            {
                items[key] = 1;
            }
        }
        //���ÿգ����������
        weightedSelector.ClearItems();
        foreach (var key in items.Keys)
        {
            weightedSelector.AddItem(key, items[key]);
        }

    }
}