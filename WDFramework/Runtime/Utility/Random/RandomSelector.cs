using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���������
/// </summary>
/// <typeparam name="T"></typeparam>
public class RandomSelector
{ 
    // ʹ�� System.Random ��� UnityEngine.Random
    protected System.Random random;
    /// <summary>
    /// ���캯������ʼ�����������������������
    /// </summary>
    /// <param name="seed">�������</param>
    public RandomSelector(int seed)
    {
        random = new System.Random(seed); // ʹ�����ӳ�ʼ�������������
    }
    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="seed"></param>
    public void ResetSeed(int seed)
    {
        random = new System.Random(seed);
    }
}