// ÏîÄ¿²ã/WDProjectCore/Randomization/IWeightAdjustStrategy.cs
using System.Collections.Generic;

public interface IWeightAdjustStrategy<T>
{
    Dictionary<T, float> Adjust(Dictionary<T, float> currentWeights,
                              GenerationContext context);
}