public class CreateAnimationStep : CharacterStep
{
    public override int Priority => 2;

    //通过参数传值得到要改参数
    public CreateAnimationStep()
    {

    }
    //创建动画播放器
    public override void ExecuteCharacterPipeline(CharacterProduct character, CharacterConfiguration configuration)
    {
        //得到播放动画的物体
        var animatorObj =character.gameObj.transform.Find("animatorPlayer").gameObject;
        var AnimationPlayer = new CharacterAnimationDriver(character.characterComponent, animatorObj, configuration.SpineSkeletonPath, configuration.ControllerPath);
        character.characterComponent.AddCharacterComponent(AnimationPlayer);
    }
}
