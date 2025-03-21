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
    private List<IRogueGenerator> generators;
    private GlobalSettingGenerator globalSettingGenerator;
    public EventManager<E_GeneratorEvent> eventManager { get; private set; }
    /// <summary>
    /// ��ʼ���������ϵͳ
    /// </summary>
    public override void Initialized()
    {
        //����ģ���б��ͨѶ��
        generators = new List<IRogueGenerator>();
        eventManager = new EventManager<E_GeneratorEvent>();
        //ȫ���趨������
        globalSettingGenerator = new GlobalSettingGenerator();
        Debug.Log("���ģ�顪����ʼ���ɹ�");
        InitializedAllGenerator();
        InitializedAllEmbeddedModules();
    }
    private void InitializedAllEmbeddedModules()
    {

    }
    private void InitializedAllGenerator()
    {
        generators.Add(new SceneGenerator());
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
        //������ģ�����Config
        foreach (var generator in generators)
        {
            (generator).LoadGlobalSetting(globalSetting);
        }
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
    }

}