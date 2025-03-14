using UnityEngine;

public class CharacterProduct : IFactoryProduct
{
    public CharacterProduct(BaseCharacter characterComponent)
    {
        this.characterComponent = characterComponent;
    }

    public GameObject gameObj => characterComponent.gameObject;
    /// <summary>
    /// 整个角色的中枢
    /// </summary>
    public BaseCharacter characterComponent { get; private set; }


}