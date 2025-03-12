using System;
using System.Collections.Generic;
using UnityEngine;

public class EqualRandomSelector<T>
{
    private List<T> items = new List<T>();

    public void AddItem(T item)
    {
        items.Add(item);
    }

    public T GetRandomItem()
    {
        if (items.Count == 0)
        {
            throw new InvalidOperationException("没有可选项！");
        }

        int randomIndex = UnityEngine.Random.Range(0, items.Count);
        return items[randomIndex];
    }
}
