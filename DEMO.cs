using Spine.Unity;
using UnityEngine;
using WDFramework;

public class DEMO : MonoBehaviour
{
    void Start()
    {

    }
    public int seed = 12345;
    private void OnGUI()
    {
       
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // ʹ�����ӳ�ʼ��
            var weightedSelector = new WeightedRandomSelector<string>(seed);
            var equalSelector = new EqualRandomSelector<string>(seed);

            // �����Ʒ
            weightedSelector.AddItem("Apple", 1);
            weightedSelector.AddItem("Banana", 2);
            weightedSelector.AddItem("Cherry", 3);

            equalSelector.AddItem("Apple");
            equalSelector.AddItem("Banana");
            equalSelector.AddItem("Cherry");

            // ��ȡ�����Ʒ
            Debug.Log("Weighted Random Item: " + weightedSelector.GetRandomItem());
            Debug.Log("Equal Random Item: " + equalSelector.GetRandomItem());
        }
    }
}
