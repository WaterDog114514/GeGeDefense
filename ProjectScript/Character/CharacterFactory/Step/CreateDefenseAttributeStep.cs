using UnityEngine.TextCore.Text;

public class CreateDefenseAttributeStep : CharacterStep
{
    public override int Priority => 2;
    //��������������
    public override void ExecuteCharacterPipeline(CharacterProduct product, CharacterConfiguration configuration)
    {
        var config = configuration as DefenseHeroConfiguration;
        var AttributeHolder = new CharacterAttributeHolder(product.characterComponent);
        AttributeHolder.AddAttribute(E_CharacterAttributeType.DamageValue, config.DamageValue);
        AttributeHolder.AddAttribute(E_CharacterAttributeType.AttackRange, config.AttackRange);
        AttributeHolder.AddAttribute(E_CharacterAttributeType.AttackCD, config.AttackCD);
        AttributeHolder.AddAttribute(E_CharacterAttributeType.DamageType, config.DamageType);
        AttributeHolder.AddAttribute(E_CharacterAttributeType.RarityLevel, config.RarityLevel);
        AttributeHolder.AddAttribute(E_CharacterAttributeType.FetterType, config.FetterType);
        AttributeHolder.AddAttribute(E_CharacterAttributeType.Price, config.Price);
        product.characterComponent.AddCharacterComponent(AttributeHolder);
    }
    //��ˮ�ߡ�����������
    public void CreateAttribute(BaseCharacter character)
    {
        //�������
        //var AttributeHolder = new CharacterAttributeHolder(character);
        //AttributeHolder.AddAttribute(E_CharacterAttributeType.Level, info.Level);
        //AttributeHolder.AddAttribute(E_CharacterAttributeType.MoveSpeed, info.MoveSpeed);
        //AttributeHolder.AddAttribute(E_CharacterAttributeType.AttackPattern, info.AttackPattern);
        //������
        //
    }
}
