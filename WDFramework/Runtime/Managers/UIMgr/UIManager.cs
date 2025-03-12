using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace WDFramework
{


    /// <summary>
    /// �㼶ö��
    /// </summary>
    public enum E_UILayer
    {
        /// <summary>
        /// ��ײ�
        /// </summary>
        Bottom,
        /// <summary>
        /// �в�
        /// </summary>
        Middle,
        /// <summary>
        /// �߲�
        /// </summary>
        Top,
        /// <summary>
        /// ϵͳ�� ��߲�
        /// </summary>
        System,
    }

    /// <summary>
    /// ��������UI���Ĺ�����
    /// ע�⣺���Ԥ������Ҫ���������һ�£���������
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {

        //ui�����ؼ�
        private Camera uiCamera;
        private Canvas uiCanvas;
        private EventSystem uiEventSystem;

        //�㼶������
        private Transform bottomLayer;
        private Transform middleLayer;
        private Transform topLayer;
        private Transform systemLayer;

        /// <summary>
        /// ���ڴ洢���е�������
        /// </summary>
        private Dictionary<string, UIBasePanel> panelDic = new Dictionary<string, UIBasePanel>();
        private FrameworkSettingData SettingData;
        public UIManager()
        {
            IntiManager();
        }

        /// <summary>
        /// ��ʼ����������ʵ����������UI�ؼ�
        /// </summary>
        public void IntiManager()
        {
            //���������ļ�
            SettingData = SettingDataLoader.Instance.LoadData<FrameworkSettingData>();
            //��̬����Ψһ��Canvas��EventSystem���������
            uiCamera = GameObject.Instantiate(ResLoader.Instance.LoadRes_Sync<GameObject>("UI/UICamera")).GetComponent<Camera>();

            //��̬����Canvas
            uiCanvas = GameObject.Instantiate(ResLoader.Instance.LoadRes_Sync<GameObject>("UI/UICanvas")).GetComponent<Canvas>();
            //����ʹ�õ�UI�����
            uiCanvas.worldCamera = uiCamera;
            //�ҵ��㼶������
            bottomLayer = uiCanvas.transform.Find("Bottom");
            middleLayer = uiCanvas.transform.Find("Middle");
            topLayer = uiCanvas.transform.Find("Top");
            systemLayer = uiCanvas.transform.Find("System");

            //��̬����EventSystem
            uiEventSystem = GameObject.Instantiate(ResLoader.Instance.LoadRes_Sync<GameObject>("UI/UIEventSystem")).GetComponent<EventSystem>();

            //���������Ƴ������ؼ�
            Object.DontDestroyOnLoad(uiEventSystem.gameObject);
            Object.DontDestroyOnLoad(uiCanvas.gameObject);
            Object.DontDestroyOnLoad(uiCamera.gameObject);
        }
        /// <summary>
        /// Ԥ�������
        /// </summary>
        /// <param name="abname"></param>
        /// <param name="panels"></param>
        public void PreLoadUIPanel(string abname, params string[] panelNames)
        {
            string[] paths = new string[panelNames.Length];
            for (int i = 0; i < panelNames.Length; i++)
            {
                if (!panelDic.ContainsKey(panelNames[i]))
                    panelDic.Add(panelNames[i], null);
                paths[i] = abname + "/" + panelNames[i];

            }
            //ResLoader.Instance.CreatePreloadTaskFromPaths(paths, (Panels) =>
            //{
            //    for (int i = 0; i < Panels.Length; i++)
            //    {
            //        GameObject panelObj = Object.Instantiate(Panels[i].Asset as GameObject);
            //        UIBasePanel panelInfo = (panelObj).GetComponent<UIBasePanel>();
            //        panelDic[panelInfo.GetType().Name] = panelInfo;
            //        panelObj.transform.SetParent(middleLayer, false);
            //        panelInfo.HideMe();
            //    }
            //});

        }

        /// <summary>
        /// ��ȡ��Ӧ�㼶�ĸ�����
        /// </summary>
        /// <param name="layer">�㼶ö��ֵ</param>
        /// <returns></returns>
        public Transform GetLayerFather(E_UILayer layer)
        {
            switch (layer)
            {
                case E_UILayer.Bottom:
                    return bottomLayer;
                case E_UILayer.Middle:
                    return middleLayer;
                case E_UILayer.Top:
                    return topLayer;
                case E_UILayer.System:
                    return systemLayer;
                default:
                    return null;
            }
        }

        /// <summary>
        /// ��ʾ���
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        /// <param name="layer">�����ʾ�Ĳ㼶</param>
        /// <param name="callBack">���ڿ������첽���� ���ͨ��ί�лص�����ʽ ��������ɵ���崫�ݳ�ȥ����ʹ��</param>
        /// <param name="isSync">�Ƿ����ͬ������ Ĭ��Ϊfalse</param>
        //public void ShowPanel<T>(E_UILayer layer = E_UILayer.Middle) where T : UIBasePanel
        //{
        //    //ͨ���������ȡ��� Ԥ������������������һ�� 
        //    T panelInfo = GetPanel<T>();

        //    //�����Ԥ���崴������Ӧ�������� ���ұ���ԭ�������Ŵ�С
        //    panelInfo.gameObject.transform.SetParent(GetLayerFather(layer), false);
        //    //����һ�о��� ���Ҳ���ˣ���ֱ�Ӳ�����
        //    //���Ҫ��ʾ��� ��ִ��һ������Ĭ����ʾ�߼�
        //    panelInfo.ShowMe();
        //}

        /// <summary>
        /// �������
        /// </summary>
        /// <typeparam name="T">�������</typeparam>
        public void HidePanel<T>() where T : UIBasePanel
        {
            if (!panelDic.ContainsKey(typeof(T).Name))
            {
                Debug.LogWarning($"����{typeof(T).Name}���ʧ�ܣ������ڴ����");
                return;
            }
            //��ȡ
            T panelInfo = panelDic[typeof(T).Name] as T;

            //����
            panelInfo.HideMe();
        }
        /// <summary>
        /// ������壬���Ǽ��ص���Դ�����ڴ��ֻ����ֻ�ǻ����ټ�����
        /// </summary>
        public void DestroyPanel<T>() where T : UIBasePanel
        {
            if (!panelDic.ContainsKey(typeof(T).Name))
            {
                Debug.LogWarning($"����{typeof(T).Name}���ʧ�ܣ������ڴ����");
                return;
            }
            //��ȡ
            T panelInfo = panelDic[typeof(T).Name] as T;

            //�������
            Object.Destroy(panelInfo.gameObject);

            //���������Ƴ�
            panelDic.Remove(typeof(T).Name);
        }

        //ͬ��������塣�첽����Ԥ���أ���
        //public T LoadPanelSync<T>(string abName = null) where T : UIBasePanel
        //{

        //    //if (abName == null)
        //    //    abName = SettingData.abLoadSetting.UIPrefabPackName;
        //    //���ط�null��֤
        //    if (!panelDic.ContainsKey(typeof(T).Name))
        //    {
        //        panelDic.Add(typeof(T).Name, null);
        //    }
        //    GameObject uiObj = Object.Instantiate(ResLoader.Instance.LoadAB_Sync(abName, typeof(T).Name) as GameObject);
        //    T panelInfo = uiObj.GetComponent<T>();
        //    panelDic[typeof(T).Name] = panelInfo;
        //    return panelInfo;
        //}

        /// <summary>
        /// ��ȡ���
        /// </summary>
        /// <typeparam name="T">��������</typeparam>
        //public T GetPanel<T>() where T : UIBasePanel
        //{
        //    string panelName = typeof(T).Name;
        //    //�������
        //    if (panelDic.ContainsKey(panelName) && panelDic[panelName] != null)
        //    {   //��������� �ȴ����ֵ䵱�� ռ��λ�� ֮���������ʾ �Ҳ��ܵõ��ֵ��е���Ϣ�����ж�
        //        return panelDic[panelName] as T;
        //    }
        //    //�����ھ�ͬ�����ذ�
        //    return LoadPanelSync<T>();
        //}


        /// <summary>
        /// Ϊ�ؼ�����Զ����¼�
        /// </summary>
        /// <param name="control">��Ӧ�Ŀؼ�</param>
        /// <param name="type">�¼�������</param>
        /// <param name="callBack">��Ӧ�ĺ���</param>
        public static void AddCustomEventListener(UIBehaviour control, EventTriggerType type, UnityAction<BaseEventData> callBack)
        {
            //�����߼���Ҫ�����ڱ�֤ �ؼ���ֻ�����һ��EventTrigger
            EventTrigger trigger = control.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = control.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener(callBack);

            trigger.triggers.Add(entry);
        }
    }

}