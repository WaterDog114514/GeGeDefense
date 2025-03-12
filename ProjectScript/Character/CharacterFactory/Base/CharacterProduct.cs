using UnityEngine;

public class CharacterProduct : IFactoryProduct
{
    public CharacterProduct(BaseCharacter characterComponent)
    {
        this.characterComponent = characterComponent;
    }

    public GameObject gameObj => characterComponent.gameObject;
    /// <summary>
    /// ������ɫ������
    /// </summary>
    public BaseCharacter characterComponent { get; private set; }


}