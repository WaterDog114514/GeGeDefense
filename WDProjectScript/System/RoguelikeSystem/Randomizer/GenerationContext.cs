using System;
using System.Collections.Generic;
using System.Linq;
public class GenerationContext
{
    // 当前关卡深度
    public int CurrentDepth { get; set; }
    // 最大关卡深度
    public int MaxDepth { get; private set; }
    // 生成历史记录
    private Dictionary<Type, Queue<object>> _history = new Dictionary<Type, Queue<object>>();
    public void RecordGeneration<T>(T generated)
    {
        var type = typeof(T);
        if (!_history.ContainsKey(type))
        {
            _history[type] = new Queue<object>();
        }
        _history[type].Enqueue(generated);
    }

    public List<T> GetRecentGenerations<T>(int count)
    {
        return _history[typeof(T)].TakeLast(count).Cast<T>().ToList();
    }
}