public class CreateAnimationStep : CharacterStep
{
    public override int Priority => 2;

    //ͨ��������ֵ�õ�Ҫ�Ĳ���
    public CreateAnimationStep()
    {

    }
    //��������������
    public override void ExecuteCharacterPipeline(CharacterProduct character, CharacterConfiguration configuration)
    {
        //�õ����Ŷ���������
        var animatorObj =character.gameObj.transform.Find("animatorPlayer").gameObject;
        var AnimationPlayer = new CharacterAnimationDriver(character.characterComponent, animatorObj, configuration.SpineSkeletonPath, configuration.ControllerPath);
        character.characterComponent.AddCharacterComponent(AnimationPlayer);
    }
}
