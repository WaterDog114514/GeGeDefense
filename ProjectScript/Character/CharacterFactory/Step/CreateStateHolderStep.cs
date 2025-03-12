//������ɫ״̬������
public class CreateStateHolderStep : CharacterStep
{
    public override int Priority => 5;
    //������ɫ״̬������
    public override void ExecuteCharacterPipeline(CharacterProduct product, CharacterConfiguration configuration)
    {
        var StateHolder = new CharacterStateHolder(product.characterComponent);
        product.characterComponent.AddCharacterComponent(StateHolder);
    }

}
