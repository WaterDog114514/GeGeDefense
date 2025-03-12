using System;
using WDFramework;



/// <summary>
/// ����������� ���Ի�ȡAB������Դ·������־�ļ�·���ȵ� 
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
    /// AB����������
    /// </summary>
    [Serializable]
    public class ABLoadSettingData
    {
        /// <summary>
        /// AB��������
        /// </summary>
        public string ABMainName = null;
        /// <summary>
        /// �Ƿ���AB�����ԣ��������Editor��ʼ��ȡ
        /// </summary>
        public bool IsDebugABLoad = false;
        /// <summary>
        /// ������Streaming����AB������ѡ��ab������·������ΪStreamingAsset
        /// </summary>
        public bool IsStreamingABLoad = false;
        /// <summary>
        /// AB���ڱ༭���м���λ��
        /// </summary>
        public string ABEditorLoadPath;
        /// <summary>
        /// AB����ϷĿ¼���·���ж�ȡ·��
        /// </summary>
        public string ABRuntimeLoadPath;
    }

    /// <summary>
    /// ��Ϸ�����ļ���������
    /// </summary>
    [Serializable]
    public class LoadContainerSettingData
    {

        public string DataPath;
        public string SuffixName;
        public bool IsDebugStreamingAssetLoad = false;

    }
    /// <summary>
    /// Ĭ��û��������Ķ����Ԥ��
    /// </summary>
    public PoolSetting defaultPoolSetting;

}