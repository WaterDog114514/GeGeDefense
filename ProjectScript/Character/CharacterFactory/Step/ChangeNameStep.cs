//������ɫ״̬������
public class ChangeNameStep : CharacterStep
{
    public override int Priority => 5;
    //������ɫ״̬������
    public override void ExecuteCharacterPipeline(CharacterProduct product, CharacterConfiguration configuration)
    {
         product.gameObj.name = configuration.Name;
    }

}
