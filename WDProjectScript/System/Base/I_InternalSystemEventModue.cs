using System;
using UnityEngine;

public interface IEventManager<T> where T : Enum
{
    EventManager<T> eventManager { get; }
}
