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
public class DefenseHeroFactory : CharacterFactory<DefenseHeroFactory>
{
    public override void InitializeFactory()
    {
        pipeline = new DefenseHeroPipeline();
    }
}
