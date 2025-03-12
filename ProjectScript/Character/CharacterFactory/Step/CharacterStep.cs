using UnityEngine;
public abstract class CharacterStep : IPipelineStep
{
    public abstract int Priority { get; }
    public void Execute(IFactoryProduct product, ExcelConfiguration configuration)
    {
        //先转化为角色专有产品和配置表
        var characterProduct = product as CharacterProduct;
        ExecuteCharacterPipeline(characterProduct,configuration as CharacterConfiguration);
    }
    public abstract void ExecuteCharacterPipeline(CharacterProduct product, CharacterConfiguration configuration) ;
}