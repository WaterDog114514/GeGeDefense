//// ��Ŀ��/WDProjectCore/Strategies/DistributionSmoothStrategy.cs
//using System.Collections.Generic;
//using System.Linq;

//public class DifficultyProgressionStrategy : IWeightAdjustStrategy<EnemyType>
//{
//    public Dictionary<EnemyType, float> Adjust(
//        Dictionary<EnemyType, float> currentWeights,
//        GenerationContext context)
//    {
//        // ���ݹؿ���ȵ���Ȩ��
//        float depthFactor = context.CurrentDepth / context.MaxDepth;

//        foreach (var type in currentWeights.Keys.ToList())
//        {
//            var config = GetEnemyConfig(type);
//            // ���Ѷȵ���Ȩ�����������
//            if (config.IsHighDifficulty)
//            {
//                currentWeights[type] *= (1 + depthFactor);
//            }
//            // ���Ѷȵ���Ȩ������ȼ���
//            else
//            {
//                currentWeights[type] *= (1 - depthFactor * 0.5f);
//            }
//        }
//        return currentWeights;
//    }
//}