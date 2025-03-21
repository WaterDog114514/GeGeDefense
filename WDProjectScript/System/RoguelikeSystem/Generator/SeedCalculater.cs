/// <summary>
/// ��ǰ���Ӽ�����
/// </summary>
public class SeedCalculater
{
    /// <summary>
    /// �������̵�ȫ������
    /// </summary>
    private int GlobalSeed;
    /// <summary>
    /// ��ǰ���ɲ�
    /// </summary>
    private int currentLayer;
    /// <summary>
    /// ��ǰ���ڲ�Ľڵ�����
    /// </summary>
    private int currentLayerIndex;

    public SeedCalculater(int globalSeed)
    {
        GlobalSeed = globalSeed;
        currentLayer = 0;
        currentLayerIndex = 0;
    }

    /// <summary>
    /// ����ֲ����ӡ�
    /// </summary>
    public int CalculateLocalSeed()
    {
        //���㵱ǰ����
        int localSeed = GlobalSeed + currentLayer * 1000 + currentLayerIndex * 5;
        currentLayerIndex++;
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