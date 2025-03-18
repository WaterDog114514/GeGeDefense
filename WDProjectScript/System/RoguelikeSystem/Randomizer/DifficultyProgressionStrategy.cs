//// 项目层/WDProjectCore/Strategies/DistributionSmoothStrategy.cs
//using System.Collections.Generic;
//using System.Linq;

//public class DifficultyProgressionStrategy : IWeightAdjustStrategy<EnemyType>
//{
//    public Dictionary<EnemyType, float> Adjust(
//        Dictionary<EnemyType, float> currentWeights,
//        GenerationContext context)
//    {
//        // 根据关卡深度调整权重
//        float depthFactor = context.CurrentDepth / context.MaxDepth;

//        foreach (var type in currentWeights.Keys.ToList())
//        {
//            var config = GetEnemyConfig(type);
//            // 高难度敌人权重随进度增加
//            if (config.IsHighDifficulty)
//            {
//                currentWeights[type] *= (1 + depthFactor);
//            }
//            // 低难度敌人权重随进度减少
//            else
//            {
//                currentWeights[type] *= (1 - depthFactor * 0.5f);
//            }
//        }
//        return currentWeights;
//    }
//}