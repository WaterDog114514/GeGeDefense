using UnityEngine;

// ��Ŀ��/RogueSystem/Base/RogueGeneratorBase.cs
public abstract class RogueGeneratorBase : IRogueGenerator
{
    //protected WeightedRandomSelector<T> weightedSelector;
    //protected EqualRandomSelector<T> equalSelector;
    protected RogueConfigBase config;

    public virtual void Initialize(RogueConfigBase config)
    {
        this.config = config;
        // ��ʼ��ѡ����
    }

    public abstract void Generate(int seed);
    public abstract void Clear();
}