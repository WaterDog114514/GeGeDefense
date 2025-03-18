/// <summary>
/// 生成器的基类，所有具体生成器应继承此类。
/// </summary>
/// <typeparam name="TGeneratorUnit">生成的基本单位类型</typeparam>
public abstract class BaseRogueGenerator<TGeneratorUnit> : IRogueSystemModuel
    where TGeneratorUnit : BaseGeneratorUnit
{
    /// <summary>
    /// 加载配置文件
    /// </summary>
    public abstract void LoadConfiguration();
    /// <summary>
    /// 初始化生成器。
    /// </summary>
    public abstract void Initialize();
    /// <summary>
    /// 生成配置文件。
    /// </summary>
    public abstract BaseRogueConfig Generate(int seed);
    /// <summary>
    /// 生成一个基本单位。
    /// </summary>
    public abstract TGeneratorUnit GenerateUnit(int seed);
}