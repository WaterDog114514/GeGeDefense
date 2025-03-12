using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using WDEditor;
[Serializable]
/// <summary>
/// 基类数据类
/// </summary>
public abstract class BaseWindowData
{
    /// <summary>
    /// 窗口名
    /// </summary>
    public string Title;
    /// <summary>
    /// 图标
    /// </summary>
    public SerializableEditorTexture2D Icon;
    /// <summary>
    /// 背景图片
    /// </summary>
    public SerializableEditorTexture2D BackgroundTexture;
    /// <summary>
    /// 是否使用背景颜色
    /// </summary>
    public bool isUseBlackground;
    /// <summary>
    /// 当前窗口大小 记录下来，下次打开还是一样大
    /// </summary>
    public SerializableVector2 currentWindowSize;
    [HideInInspector]
    public bool isFirstCreated = true;
    // 第一次创建初始化方法 子类不能调
    public void IntiFirst()
    {
        
        //只能第一次创建时候运行
        if (!isFirstCreated) return;
        isFirstCreated = false;
        //执行第一次创建逻辑
        IntiWinSize();
        IntiWinTexture();
        IntiFirstCreate();

    }
    /// <summary>
    /// 加载时候赋予初值，由window初始化调用
    /// </summary>
    public virtual void IntiLoad()
    {

    }
    /// <summary>
    /// 初始化设置data的值只能子类重写
    /// </summary>
    public virtual void IntiFirstCreate()
    {


    }
    //初始化窗口大小
    private void IntiWinSize()
    {
        //默认设置窗口大小
        currentWindowSize.vector2 = new Vector2(768,512);
    }
    //初始化窗口图片
    private void IntiWinTexture()
    {
        Icon = new SerializableEditorTexture2D();
        BackgroundTexture = new SerializableEditorTexture2D();

        //获取美术路径
        string TexturePath = EditorPathHelper.GetRelativeAssetPath(Path.Combine(EditorPathHelper.EditorAssetPath, "Texture"));
        //设置图标和背景图
        Icon.texture = AssetDatabase.LoadAssetAtPath<Texture2D>(Path.Combine(TexturePath, "defaultIcon.png"));
        BackgroundTexture.texture = AssetDatabase.LoadAssetAtPath<Texture2D>(Path.Combine(TexturePath, "defaultBG.jpg"));
    }
}
