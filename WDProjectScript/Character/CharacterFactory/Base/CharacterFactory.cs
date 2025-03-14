using System.Collections.Generic;
using UnityEngine;
using WDFramework;

/*
 模块化开发改动：
1.做成生产流水线式
 */
/// <summary>
/// 角色工厂，完成角色的创建工作
/// 根据角色的配置表来进行创建
/// </summary>
public abstract class CharacterFactory<T> : BaseFactory<T> where T : class, new()
{

    /// <summary>
    /// 实例化一个角色id为XX的角色，如果这个角色已经通过流水线创建，那么直接从
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public GameObject CreateCharacter(int ID)
    {
        var product = pipeline.CreateNewProduct(ID) as CharacterProduct;
        return product.gameObj;
    }

}


/// <summary>
/// 流水线创建助手
/// </summary>
//class CharacterStepHelper
//{


//    //流水线——创建动画

//}