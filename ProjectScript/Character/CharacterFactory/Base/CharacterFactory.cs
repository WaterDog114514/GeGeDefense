using System.Collections.Generic;
using UnityEngine;
using WDFramework;

/*
 ģ�黯�����Ķ���
1.����������ˮ��ʽ
 */
/// <summary>
/// ��ɫ��������ɽ�ɫ�Ĵ�������
/// ���ݽ�ɫ�����ñ������д���
/// </summary>
public abstract class CharacterFactory<T> : BaseFactory<T> where T : class, new()
{

    /// <summary>
    /// ʵ����һ����ɫidΪXX�Ľ�ɫ����������ɫ�Ѿ�ͨ����ˮ�ߴ�������ôֱ�Ӵ�
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public GameObject CreateCharacter(int ID)
    {
        var product = pipeline.CreateNewProduct(ID) as CharacterProduct;
        return product.gameObj;
    }

}


/// <summary>
/// ��ˮ�ߴ�������
/// </summary>
//class CharacterStepHelper
//{


//    //��ˮ�ߡ�����������

//}