/// <summary>
/// ������ɫ��Ϊ������
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
