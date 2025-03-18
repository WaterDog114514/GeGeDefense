using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������
/// </summary>
/// <typeparam name="T"></typeparam>
public class EqualRandomSelector<T> : RandomSelector
{
    private List<T> items = new List<T>(); // �洢��Ʒ�б�
    /// <summary>
    /// ���캯������ʼ�����������������������
    /// </summary>
    /// <param name="seed">�������</param>
    public EqualRandomSelector(int seed) : base(seed)
    {
    }
    /// <summary>
    /// ���һ����Ʒ
    /// </summary>
    /// <param name="item">��Ʒ����</param>
    public void AddItem(T item)
    {
        items.Add(item);
    }
    /// <summary>
    /// ���ѡ��һ����Ʒ
    /// </summary>
    /// <returns>���ѡ�е���Ʒ</returns>
    public T GetRandomItem()
    {
        if (items.Count == 0)
        {
            throw new InvalidOperationException("û�п�ѡ�");
        }

        // ʹ�� System.Random �����������
        int randomIndex = random.Next(0, items.Count);
        return items[randomIndex];
    }

}