using System;
using WDFramework;



/// <summary>
/// 框架设置数据 可以获取AB包的资源路径，日志文件路径等等 
/// </summary>
[Serializable]
public class FrameworkSettingData : BaseSettingData
{
    public ABLoadSettingData abLoadSetting;
    public LoadContainerSettingData loadContainerSetting;
    public override void IntiValue()
    {
        abLoadSetting = new ABLoadSettingData();
        loadContainerSetting = new LoadContainerSettingData();
        defaultPoolSetting = new PoolSetting() {  MaxCount = 20, PoolType = Pool.E_PoolType.Expansion};
    }
    /// <summary>
    /// AB包加载设置
    /// </summary>
    [Serializable]
    public class ABLoadSettingData
    {
        /// <summary>
        /// AB包主包名
        /// </summary>
        public string ABMainName = null;
        /// <summary>
        /// 是否开启AB包调试，开启后从Editor开始读取
        /// </summary>
        public bool IsDebugABLoad = false;
        /// <summary>
        /// 开启从Streaming加载AB包，勾选后，ab包加载路径设置为StreamingAsset
        /// </summary>
        public bool IsStreamingABLoad = false;
        /// <summary>
        /// AB包在编辑器中加载位置
        /// </summary>
        public string ABEditorLoadPath;
        /// <summary>
        /// AB包游戏目录相对路径中读取路径
        /// </summary>
        public string ABRuntimeLoadPath;
    }

    /// <summary>
    /// 游戏配置文件加载设置
    /// </summary>
    [Serializable]
    public class LoadContainerSettingData
    {

        public string DataPath;
        public string SuffixName;
        public bool IsDebugStreamingAssetLoad = false;

    }
    /// <summary>
    /// 默认没有设置组的对象池预设
    /// </summary>
    public PoolSetting defaultPoolSetting;

}