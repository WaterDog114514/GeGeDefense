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
/// ����������
/// </summary>
public abstract class BaseWindowData
{
    /// <summary>
    /// ������
    /// </summary>
    public string Title;
    /// <summary>
    /// ͼ��
    /// </summary>
    public SerializableEditorTexture2D Icon;
    /// <summary>
    /// ����ͼƬ
    /// </summary>
    public SerializableEditorTexture2D BackgroundTexture;
    /// <summary>
    /// �Ƿ�ʹ�ñ�����ɫ
    /// </summary>
    public bool isUseBlackground;
    /// <summary>
    /// ��ǰ���ڴ�С ��¼�������´δ򿪻���һ����
    /// </summary>
    public SerializableVector2 currentWindowSize;
    [HideInInspector]
    public bool isFirstCreated = true;
    // ��һ�δ�����ʼ������ ���಻�ܵ�
    public void IntiFirst()
    {
        
        //ֻ�ܵ�һ�δ���ʱ������
        if (!isFirstCreated) return;
        isFirstCreated = false;
        //ִ�е�һ�δ����߼�
        IntiWinSize();
        IntiWinTexture();
        IntiFirstCreate();

    }
    /// <summary>
    /// ����ʱ�����ֵ����window��ʼ������
    /// </summary>
    public virtual void IntiLoad()
    {

    }
    /// <summary>
    /// ��ʼ������data��ֵֻ��������д
    /// </summary>
    public virtual void IntiFirstCreate()
    {


    }
    //��ʼ�����ڴ�С
    private void IntiWinSize()
    {
        //Ĭ�����ô��ڴ�С
        currentWindowSize.vector2 = new Vector2(768,512);
    }
    //��ʼ������ͼƬ
    private void IntiWinTexture()
    {
        Icon = new SerializableEditorTexture2D();
        BackgroundTexture = new SerializableEditorTexture2D();

        //��ȡ����·��
        string TexturePath = EditorPathHelper.GetRelativeAssetPath(Path.Combine(EditorPathHelper.EditorAssetPath, "Texture"));
        //����ͼ��ͱ���ͼ
        Icon.texture = AssetDatabase.LoadAssetAtPath<Texture2D>(Path.Combine(TexturePath, "defaultIcon.png"));
        BackgroundTexture.texture = AssetDatabase.LoadAssetAtPath<Texture2D>(Path.Combine(TexturePath, "defaultBG.jpg"));
    }
}
