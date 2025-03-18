using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ȩ���ѡ������Weighted Random Selector��
/// �������ڶ��Ԫ���У����ݸ��Ե�Ȩ��ֵ���������ȡ��
/// </summary>
/// <typeparam name="T">���� T����ʾ�������κ����ͣ����ַ����������������ȣ���</typeparam>
public class WeightedRandomSelector<T> : RandomSelector
{
    // �洢��Ʒ����Ȩ��
    private Dictionary<T, float> itemWeights = new Dictionary<T, float>();
    // ��¼����Ȩ�ص��ܺ�
    private float totalWeight = 0;
    /// <summary>
    /// ���캯������ʼ�����������������������
    /// </summary>
    /// <param name="seed">�������</param>
    public WeightedRandomSelector(int seed) : base(seed)
    {
    }
    /// <summary>
    /// ���һ����Ʒ����ָ����Ȩ�ء�
    /// </summary>
    /// <param name="item">��Ʒ����</param>
    /// <param name="weight">����Ʒ��Ȩ�أ��������0</param>
    public void AddItem(T item, float weight)
    {
        if (weight <= 0)
        {
            Debug.LogWarning("Ȩ�ر������0��");
            return;
        }

        if (itemWeights.ContainsKey(item))
        {
            // �����Ʒ�Ѵ��ڣ������Ȩ��
            totalWeight -= itemWeights[item]; // ���Ƴ���Ȩ��
            itemWeights[item] = weight;
        }
        else
        {
            // �����������Ʒ
            itemWeights.Add(item, weight);
        }

        totalWeight += weight; // ������Ȩ��
    }

    /// <summary>
    /// ����Ȩ�����ѡ��һ����Ʒ
    /// </summary>
    /// <returns>���ѡ�е���Ʒ</returns>
    public T GetRandomItem()
    {
        if (itemWeights.Count == 0)
        {
            throw new InvalidOperationException("û�п�ѡ�");
        }

        // ʹ�� System.Random �������ֵ
        float randomValue = (float)random.NextDouble() * totalWeight;
        float cumulativeSum = 0;

        // �����ֵ��е���Ʒ���������ۻ�Ȩ���ж�ѡ����һ��
        foreach (var item in itemWeights)
        {
            cumulativeSum += item.Value; // �ۼӵ�ǰ��Ʒ��Ȩ��
            if (randomValue < cumulativeSum)
            {
                return item.Key; // ѡ�и���Ʒ
            }
        }

        return default; // �����ϲ��ᵽ������Ǽ������
    }
    public void ClearItems()
    {
        itemWeights.Clear();
    }
}