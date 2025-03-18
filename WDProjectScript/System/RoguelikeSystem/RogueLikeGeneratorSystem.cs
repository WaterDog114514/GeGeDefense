using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���ϵͳ 
/// һ������������ɵ�ϵͳ
/// </summary>
public class RogueLikeGeneratorSystem : ManagedSingleton<RogueLikeGeneratorSystem>, IGameSystem, IEventManager<E_GeneratorEvent>
{
    private GlobalRandomSetting globalSetting;
    //ȫ��random
    /// <summary>
    /// �������ģ��
    /// </summary>
    private List<IRogueSystemModuel> moduels;
    private GlobalSettingGenerator globalSettingGenerator;
    public EventManager<E_GeneratorEvent> eventManager { get; private set; }
    /// <summary>
    /// ��ʼ���������ϵͳ
    /// </summary>
    public override void Initialized()
    {
        //����ģ���б��ͨѶ��
        moduels = new List<IRogueSystemModuel>();
        eventManager = new EventManager<E_GeneratorEvent>();
        //ȫ���趨������
        globalSettingGenerator = new GlobalSettingGenerator();
        globalSettingGenerator.Initialize();
        Debug.Log("���ģ�顪����ʼ���ɹ�");
    }
    public void LoadConfig()
    {

    }
    /// <summary>
    /// ��ʼ����ȫ���ؿ�����
    /// </summary>
    public void StartGenerate(int globalSeed = -1)
    {
        //������ȫ���趨
        globalSetting = globalSettingGenerator.GenerateGlobalSetting(globalSeed);
    }

    /// <summary>
    /// �������йؿ�
    /// </summary>
    /// <param name="globalSeed"></param>
    private void GenerateAllLevel()
    {
        //��Ⱥϵ��������
        for (int i = 0; i < globalSetting.biomeProcessDatas.Count; i++)
        {

        }
    }

    /// <summary>
    /// ����������ȡ�
    /// </summary>
    public void UpdateNextProgress(float progress)
    {
        _randomProgress = Mathf.Clamp(progress, 0, 100);
    }

    /// <summary>
    /// ����ֲ����ӡ�
    /// </summary>
    private int CalculateLocalSeed(int offset)
    {
        return _globalSeed + offset + (int)(_randomProgress * 100);
    }

    /// <summary>
    /// ��ʼ���������ϵͳ����ģ��
    /// </summary>
    public void InitializeGeneratorModules()
    {
        //ȫ�����ӹ�����
        moduels.Add(new SeedManager());
        //����������
        moduels.Add(new SceneGenerator());
        //ÿ��ģ���ʼ��
        foreach (var moduel in moduels)
        {
            moduel.Initialize();
        }
    }
}
