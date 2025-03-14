using UnityEngine;

// 项目层/RogueSystem/Base/RogueGeneratorBase.cs
public abstract class RogueGeneratorBase : IRogueGenerator
{
    //protected WeightedRandomSelector<T> weightedSelector;
    //protected EqualRandomSelector<T> equalSelector;
    protected RogueConfigBase config;

    public virtual void Initialize(RogueConfigBase config)
    {
        this.config = config;
        // 初始化选择器
    }

    public abstract void Generate(int seed);
    public abstract void Clear();
}