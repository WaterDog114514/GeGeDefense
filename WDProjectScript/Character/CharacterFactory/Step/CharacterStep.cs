using UnityEngine;
public abstract class CharacterStep : IPipelineStep
{
    public abstract int Priority { get; }
    public void Execute(IFactoryProduct product, ExcelConfiguration configuration)
    {
        //��ת��Ϊ��ɫר�в�Ʒ�����ñ�
        var characterProduct = product as CharacterProduct;
        ExecuteCharacterPipeline(characterProduct,configuration as CharacterConfiguration);
    }
    public abstract void ExecuteCharacterPipeline(CharacterProduct product, CharacterConfiguration configuration) ;
}