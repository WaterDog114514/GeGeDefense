[System.Serializable]
public class SceneRandomConfig : ExcelConfiguration
{
    public int BiomeID;
    public string BiomeName;
    public int[] LeftRightSceneID;
    public int[] UpDownSceneID;
    public int[] LeftUp_RightDownSceneID;
    public int[] LeftDown_RightUpSceneID;

}