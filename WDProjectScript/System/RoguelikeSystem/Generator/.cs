/// <summary>
/// ��ǰ���Ӽ�����
/// </summary>
public class SeedCalculater
{
    /// <summary>
    /// �������̵�ȫ������
    /// </summary>
    private int GlobalSeed;
    private int Offset;
    /// <summary>
    /// ��ǰ�ؿ��ڵ�����
    /// </summary>
    private int LevelNodeIndex;
    public SeedCalculater(int globalSeed, int offset)
    {
        GlobalSeed = globalSeed;
        this.Offset = offset;
    }

    /// <summary>
    /// ����ֲ����ӡ�
    /// </summary>
    public int CalculateLocalSeed()
    {
        //���㹫ʽ
        int localSeed = GlobalSeed + LevelNodeIndex * 500 + Offset * 5;
        LevelNodeIndex++;
        return localSeed;
    }
    /// <summary>
    /// ��һ��¥
    /// </summary>
    public void UpdateNextLayer()
    {
        currentLayer++;
        currentLayerIndex = 0;
    }
}