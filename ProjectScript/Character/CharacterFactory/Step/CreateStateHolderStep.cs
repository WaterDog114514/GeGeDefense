//创建角色状态持有者
public class CreateStateHolderStep : CharacterStep
{
    public override int Priority => 5;
    //创建角色状态持有者
    public override void ExecuteCharacterPipeline(CharacterProduct product, CharacterConfiguration configuration)
    {
        var StateHolder = new CharacterStateHolder(product.characterComponent);
        product.characterComponent.AddCharacterComponent(StateHolder);
    }

}
