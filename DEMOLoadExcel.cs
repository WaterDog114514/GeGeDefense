using UnityEngine;

public class DEMOLoadExcel :MonoBehaviour
{
    private void Awake()
    {
        var dic = ExcelBinarayLoader.Instance.GetDataContainer<EnemyConfiguration>("Enemy1");
        var dic2 = ExcelBinarayLoader.Instance.GetDataContainer<EnemyConfiguration>("Enemy2");
        Debug.Log(123);
    }
}
