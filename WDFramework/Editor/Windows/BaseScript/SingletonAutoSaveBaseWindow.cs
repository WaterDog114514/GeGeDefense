//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEditor.Compilation;
//using UnityEngine;

///// <summary>
///// �����Զ��洢�ͼ��ش������ݵĵ�һ��������
///// </summary>
//public abstract class SingletonAutoSaveBaseWindow : BaseWindow
//{
//    #region ���ڻ������
//    protected override void OnDestroy()
//    {
//        base.OnDestroy();
//        m_CloseSaveWindowsData();
//    }
//    protected override void OnGUI()
//    {
//        base.OnGUI();
//        //���ƴ��ڱ����߼�
//        this.SettingSaveLoadDraw();

//    }
//    protected override void OnEnable()
//    {
//        base.OnEnable();
//        m_LoadWindowsSaveInfo();
//        CompilationPipeline.compilationStarted += m_callback_CompipeAutoSave;
//    }
//    #endregion


//    #region ���洰�������Զ������������

//    //���ɱ������ݣ����в����Զ�����
//    /// <summary>
//    /// Ĭ�ϵı༭���ǿ��Ա������ݵ�
//    /// </summary>
//    protected bool b_IsCanSaveData = true;

//    /// <summary>
//    /// �Զ�����·��
//    /// </summary>
//    protected string AutoSavePath;

//    /// <summary>
//    /// ���ش���ʱ��ʼ���߼�
//    /// </summary>
//    /// <param name="OnReloadWin"></param>
//    public void m_LoadWindowsSaveInfo()
//    {
//        if (!b_IsCanSaveData) return;
//        //�����Զ�����·��
//        AutoSavePath = EM_WinSetting.Instance.SettingData.AutoSavePath + GetType().Name + ".json";
//        if (File.Exists(AutoSavePath))
//        {
//            //�Զ������߼�
//            EditorMain tempMain = this.LoadWindowsData(AutoSavePath);
//            //Ҫ�Ǽ��ص��ļ��в��Զ����أ�ȡ����ʵ�������Զ����ذ�
//            if (tempMain == null)
//                editorMain = Activator.CreateInstance(MainType) as EditorMain;
//            else editorMain = tempMain;
//        }
//        else
//        {
//            //�������ļ����½�һ��ʵ��
//            editorMain = Activator.CreateInstance(MainType) as EditorMain;
//        }
//    }
//    /// <summary>
//    /// �رմ���ʱ�Զ��������
//    /// </summary>
//    public void m_CloseSaveWindowsData()
//    {
//        if (b_IsCanSaveData)
//            this.SaveWindowsDataToPath(AutoSavePath);
//    }
//    /// <summary>
//    /// ��ʼ����ʱ��Ҫ�Զ�������
//    /// </summary>
//    /// <param name="obj"></param>
//    public void m_callback_CompipeAutoSave(object obj)
//    {
//        Debug.Log("��ʼ������" + obj);
//        m_CloseSaveWindowsData();
//    }
//    #endregion
//}
