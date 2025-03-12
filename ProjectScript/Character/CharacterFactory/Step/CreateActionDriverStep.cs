/// <summary>
/// 创建角色行为驱动者
/// </summary>
public class CreateActionDriverStep : CharacterStep
{
    public override int Priority => 2;
    public override void ExecuteCharacterPipeline(CharacterProduct product, CharacterConfiguration configuration)
    {
        var actionDriver = new CharacterActionDriver(product.characterComponent);
        product.characterComponent.AddCharacterComponent(actionDriver);
    }
}
