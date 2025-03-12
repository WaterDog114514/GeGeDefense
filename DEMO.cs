using Spine.Unity;
using UnityEngine;
using WDFramework;

public class DEMO : MonoBehaviour
{
    private void Start()
    {
        //ResLoader.Instance.LoadAB_Async<SkeletonDataAsset>(E_ABPackName.character, "Àû×¦É½¼¦_SkeletonData", (RES) =>
        //{
        var dede = DefenseHeroFactory.Instance.CreateCharacter(1);
        var js = dede.GetComponent<BaseCharacter>();
        //});
        //);
        //(E_ABPackName, string)[] cc = { () };
        //ResLoader.Instance.CreatePreloadTaskFromPaths(cc
        // , (res) => {


        //     Debug.Log(res);
        //     Debug.Log(ResLoader.Instance.dic_LoadedRes);
        // });
        //ResLoader.Instance.StartPreload();
        //var DATA = ExcelBinarayLoader.Instance.GetDataContainer(typeof(DefenseHeroConfiguration));
        //var DATA2 = ExcelBinarayLoader.Instance.GetDataContainer<EnemyConfiguration>();
        //var DATA3 = ExcelBinarayLoader.Instance.GetDataContainer<EnemyConfiguration>();
        //Debug.Log(123);


    }
}
