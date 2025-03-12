using System.Collections.Generic;
using UnityEngine;

public class DEMO2 : MonoBehaviour
{
    private void Awake()
    {
       
    }
    private void Update()
    {
        
        
    }
}
class SystemA
{
    public SystemA()
    {
        EventCenterSystem.Instance.AddEventListener(E_ActionName.Attack, () =>
        {
            Debug.Log("gji5555");
        }, 5);
        EventCenterSystem.Instance.AddEventListener(E_ActionName.Attack, () =>
        {
            Debug.Log("gji2222");
        }, 2);

    }
}
class SystemB
{
    public SystemB()
    {
        EventCenterSystem.Instance.AddEventListener(E_ActionName.Attack, () =>
        {
            Debug.Log("gji3333");
        }, 3);
        EventCenterSystem.Instance.AddEventListener(E_ActionName.Attack, () =>
        {
            Debug.Log("BB11111");
        }, 1);
    }
}