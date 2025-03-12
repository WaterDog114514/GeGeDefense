using System;
using System.Collections.Generic;
using UnityEngine;
using WDFramework;
//框架层代码
/// <summary>
/// 角色流水线基类，用来按照特定顺序完善
/// </summary>
public abstract class CharacterPipeline<TConfigType> : FactoryPipeline<TConfigType> where TConfigType : ExcelConfiguration
{
    public CharacterPipeline()
    {
        // 
        // 流水线步骤配置（框架层定义接口）

    }
    //初始化角色的底膜
    public override IFactoryProduct InitializeProduct()
    {
        //从预制体加载工具加载
        var prefab = PrefabLoader.Instance.GetPrefab("CharacterPrefab", PrefabLoader.E_LoadPattren.AB);
        //然后进行实例化呀
        GameObject characterObj = GameObject.Instantiate(prefab);
        var characterComponent  = characterObj.AddComponent<BaseCharacter>();
        var product = new CharacterProduct(characterComponent); 
        return product;
    }
}
