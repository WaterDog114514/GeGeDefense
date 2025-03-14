// 项目层/RogueSystem/Interfaces/IRogueGenerator.cs
public interface IRogueGenerator
{
    void Initialize(RogueConfigBase config); // 初始化配置
    void Generate(int seed);                 // 生成主入口
    void Clear();                            // 清理资源
}

