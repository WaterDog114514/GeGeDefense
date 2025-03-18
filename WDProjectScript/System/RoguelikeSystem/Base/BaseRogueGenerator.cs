/// <summary>
/// �������Ļ��࣬���о���������Ӧ�̳д��ࡣ
/// </summary>
/// <typeparam name="TGeneratorUnit">���ɵĻ�����λ����</typeparam>
public abstract class BaseRogueGenerator<TGeneratorUnit> : IRogueSystemModuel
    where TGeneratorUnit : BaseGeneratorUnit
{
    /// <summary>
    /// ���������ļ�
    /// </summary>
    public abstract void LoadConfiguration();
    /// <summary>
    /// ��ʼ����������
    /// </summary>
    public abstract void Initialize();
    /// <summary>
    /// ���������ļ���
    /// </summary>
    public abstract BaseRogueConfig Generate(int seed);
    /// <summary>
    /// ����һ��������λ��
    /// </summary>
    public abstract TGeneratorUnit GenerateUnit(int seed);
}