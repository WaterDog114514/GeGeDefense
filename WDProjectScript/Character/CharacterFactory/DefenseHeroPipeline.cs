using System.Collections.Generic;
using UnityEngine;
using WDFramework;

/// <summary>
/// 塔防角色流水线
/// </summary>
public class DefenseHeroPipeline : CharacterPipeline<DefenseHeroConfiguration>
{
    public override void InitializePipeline()
    {
        container = ExcelBinarayLoader.Instance.GetDataContainer<DefenseHeroConfiguration>();
        AddStep(new CreateDefenseAttributeStep());
        AddStep(new CreateStateHolderStep());
        AddStep(new CreateActionDriverStep());
        AddStep(new CreateAnimationStep());
        AddStep(new ChangeNameStep());
        //按优先级排序所有顺序
        SortStep();
            
    }
}
