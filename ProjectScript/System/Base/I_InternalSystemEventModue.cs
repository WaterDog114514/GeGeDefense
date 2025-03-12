using System;
using UnityEngine;

public interface I_InternalSystemEventModue<T> where T : Enum
{
    EventManager<T> eventManager { get; }
}
