using System.Collections.Generic;
using UnityEngine;

public class GameLevelNodeTest : MonoBehaviour
{
    public GameObject meun;
    public GameObject levelnode;
    public GameObject BattlePrefab;
    public GameObject ShopPrefab;
    public GameObject EventPrefab;
    private LevelNodeGeneratorConfig _config;
    public float layerInterval;
    public float nodeInterval;
    private void Awake()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            meun.SetActive(false);
            levelnode.SetActive(true);
            _config = RogueLikeGeneratorSystem.Instance.StartGenerate(114514).levelNodeGeneratorConfig;
            ShowNode();
            Debug.Log("生成成功");
        }

    }
    private void ShowNode()
    {
        var layers = _config.biomes[0].layers;
        Vector2 center = (levelnode.transform as RectTransform).pivot;
        for (int i = 0; i < layers.Count; i++)
        {
            Vector2 layerpos = Vector2.up * i * layerInterval;
            var nodes = layers[i].layerNodes;
            for (int j = 0; j < nodes.Count; j++)
            {
                GameObject obj = null;
                Vector2 nodepos = center + Vector2.right * j * nodeInterval + layerpos;
                switch (nodes[j].NodeID)
                {
                    case (int)E_LevelNodeType.Battle:
                      Instantiate(BattlePrefab, nodepos, Quaternion.identity);
                        break;
                    case (int)E_LevelNodeType.Event:
                        Instantiate(EventPrefab, nodepos, Quaternion.identity);
                        break;
                    case (int)E_LevelNodeType.Shop:
                        Instantiate(ShopPrefab, nodepos, Quaternion.identity);
                        break;


                }
            }
        }
    }
}