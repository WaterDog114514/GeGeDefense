using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;
using WDFramework;

public class DEMO : MonoBehaviour
{
    public int seed;
    Randomizer<int> randomizer;
    RandomRegulator<int> randomRegulator;
    Dictionary<int, int> result;
    void Start()
    {
        result = new Dictionary<int, int>();
        randomRegulator = new RandomRegulator<int>(seed);
        randomizer = new Randomizer<int>(seed);
        randomizer.Additems(
            new List<int>() { 114514, 8890, 6666 },
            new List<int>() { 10, 5, 3 }
            );
    }
    private void OnGUI()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            randomizer.ResetSeed(seed);
            result.Clear();
            var list = randomizer.GetProportionRandom(18);
            for (int i = 0; i < list.Count; i++)
            {
                int res = list[i];
                if (result.ContainsKey(res))
                {
                    result[res]++;
                }
                else
                {
                    result[res] = 1;
                }
            }
        }
    }
}
