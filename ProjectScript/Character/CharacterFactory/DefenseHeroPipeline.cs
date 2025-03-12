using System.Collections.Generic;
using UnityEngine;
using WDFramework;

/// <summary>
/// ������ɫ��ˮ��
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
        //�����ȼ���������˳��
        SortStep();
            
    }
}
