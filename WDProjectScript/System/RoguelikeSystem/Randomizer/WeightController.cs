// ��ܲ�/WDFramework/Randomization/WeightController.cs
using System.Collections.Generic;

public class WeightController<T>
{
    // ԭʼȨ������
    private Dictionary<T, float> _baseWeights;
    // ��̬Ȩ�ػ���
    private Dictionary<T, float> _currentWeights;
    // ��ʷ��¼�����ڷֲ��Ż���
    private Queue<T> _history = new Queue<T>();

    public WeightController(Dictionary<T, float> baseWeights)
    {
        _baseWeights = baseWeights;
        Reset();
    }

    // ����Ϊ��ʼ״̬
    public void Reset()
    {
        _currentWeights = new Dictionary<T, float>(_baseWeights);
        _history.Clear();
    }

    // ��ȡ��ǰȨ�ر�����̬������
    //public Dictionary<T, float> GetWeights()
    //{
    //    return ApplyDynamicAdjustments();
    //}
}