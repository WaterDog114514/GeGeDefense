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
public class DefenseHeroFactory : CharacterFactory<DefenseHeroFactory>
{
    public override void InitializeFactory()
    {
        pipeline = new DefenseHeroPipeline();
    }
}
