using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 随机调整器的item接口
/// </summary>
/// <typeparam name="T"></typeparam>
public interface I_ItemRegulator<T> where T: IComparable<T>
{
    /// <summary>
    /// 要取到需要修正的值
    /// </summary>
    T FixedValue { get;set;}
}