// ��Ŀ��/RogueSystem/Interfaces/IRogueGenerator.cs
public interface IRogueGenerator
{
    void Initialize(RogueConfigBase config); // ��ʼ������
    void Generate(int seed);                 // ���������
    void Clear();                            // ������Դ
}

