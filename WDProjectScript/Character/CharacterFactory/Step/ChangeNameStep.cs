//创建角色状态持有者
public class ChangeNameStep : CharacterStep
{
    public override int Priority => 5;
    //创建角色状态持有者
    public override void ExecuteCharacterPipeline(CharacterProduct product, CharacterConfiguration configuration)
    {
         product.gameObj.name = configuration.Name;
    }

}
