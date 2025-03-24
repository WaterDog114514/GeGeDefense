using UnityEngine;
/// <summary>
/// 生成器与肉鸽系统的通讯事件
/// </summary>
public enum E_GeneratorEvent
{
    /// <summary>
    /// 生成全局设置
    /// </summary>
    GenerateGlobalSetting,
    /// <summary>
    ///模块加载全局设定
    /// </summary>
    LoadGlobalGenerateSetting,
    /// <summary>
    /// 生成所有关卡节点
    /// </summary>
    GenerateAllLevelNode
}