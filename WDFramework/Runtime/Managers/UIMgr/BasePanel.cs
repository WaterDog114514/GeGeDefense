using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// ����Ui���Ļ���
/// </summary>
public abstract class UIBasePanel : MonoBehaviour
{
    /// <summary>
    /// ���ڴ洢����Ҫ�õ���UI�ؼ�������ʷ�滻ԭ�� ����װ����
    /// </summary>
    protected Dictionary<string, UIBehaviour> controlDic = new Dictionary<string, UIBehaviour>();

    //����ÿ�ֲ�ͬ�Ŀؼ��¼���ʹ�ò�ͬ���ֵ���м���
    protected Dictionary<string, UnityAction> dic_events_ClickButton = new Dictionary<string, UnityAction>();
    protected Dictionary<string, UnityAction<float>> dic_events_ChangeSlider = new Dictionary<string, UnityAction<float>>();
    protected Dictionary<string, UnityAction<bool>> dic_events_ClickToggel = new Dictionary<string, UnityAction<bool>>();

    /// <summary>
    /// �ؼ�Ĭ������ ����õ��Ŀؼ����ִ������������ ��ζ�����ǲ���ͨ������ȥʹ���� ��ֻ��������ʾ���õĿؼ�
    /// </summary>
    private static List<string> defaultNameList = new List<string>() { "Image",
                                                                   "Text (TMP)",
                                                                   "RawImage",
                                                                   "Background",
                                                                   "Checkmark",
                                                                   "Label",
                                                                   "Text (Legacy)",
                                                                   "Arrow",
                                                                   "Placeholder",
                                                                   "Fill",
                                                                   "Handle",
                                                                   "Viewport",
                                                                   "Scrollbar Horizontal",
                                                                   "Scrollbar Vertical"};
    protected virtual void Awake()
    {
        FindChildControl();
    }
    /// <summary>
    /// �����ʾʱ����õ��߼�
    /// </summary>


    public virtual void ShowMe()
    {
        gameObject.SetActive(true);
    }
    /// <summary>
    /// �������ʱ����õ��߼�
    /// </summary>
    public virtual void HideMe()
    {
        gameObject.SetActive(false);
    }
    /// <summary>
    /// ��ȡָ�������Լ�ָ�����͵����
    /// </summary>
    /// <typeparam name="T">�������</typeparam>
    /// <param name="name">�������</param>
    /// <returns></returns>
    public T GetControl<T>(string name) where T : UIBehaviour
    {
        if (controlDic.ContainsKey(name))
        {
            T control = controlDic[name] as T;
            if (control == null)
                Debug.LogError($"�����ڶ�Ӧ����{name}����Ϊ{typeof(T)}�����");
            return control;
        }
        else
        {
            Debug.LogError($"�����ڶ�Ӧ����{name}�����");
            return null;
        }
    }
    //ͨ�������Ӧ�ؼ����Ŀؼ���Ӽ�������
    public void AddListenerButtonClickEvent(string controlName, UnityAction operation)
    {
        if (!dic_events_ClickButton.ContainsKey(controlName))
        {
            Debug.LogError($"����岻������Ϊ{controlName}�Ŀؼ�����������ʧ��");
            return;
        }
        dic_events_ClickButton[controlName] = operation;
    }
    public void AddListenerChangeToggelEvent(string controlName, UnityAction<bool> operation)
    {
        if (!dic_events_ClickToggel.ContainsKey(controlName))
        {
            Debug.LogError($"����岻������Ϊ{controlName}�Ŀؼ�����������ʧ��");
            return;
        }
        dic_events_ClickToggel[controlName] = operation;
    }

    public void AddListenerChangeSliderEvent(string controlName, UnityAction<float> operation)
    {
        if (!dic_events_ChangeSlider.ContainsKey(controlName))
        {
            Debug.LogError($"����岻������Ϊ{controlName}�Ŀؼ�����������ʧ��");
            return;
        }
        dic_events_ChangeSlider[controlName] = operation;
    }

    //�ܺ͵��÷���
    private void FindChildControl()
    {
        //����Ӧ�����Ȳ�����Ҫ�����
        FindChildrenControl<Button>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
        FindChildrenControl<InputField>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<Dropdown>();
        //��ʹ�����Ϲ����˶����� ֻҪ�����ҵ�����Ҫ���
        //֮��Ҳ����ͨ����Ҫ����õ������������ص�����
        FindChildrenControl<Text>();
        FindChildrenControl<TextMeshPro>();
        FindChildrenControl<Image>();
    }
    //����ĳһ��������еĿؼ�
    private void FindChildrenControl<T>() where T : UIBehaviour
    {
        //��ȡ���пؼ�
        T[] controls = this.GetComponentsInChildren<T>(true);
        for (int i = 0; i < controls.Length; i++)
        {
            //��ȡ��ǰ�ؼ�������
            string controlName = controls[i].gameObject.name;
            //�Ѿ���¼�ģ�����Ĭ�����Ŀؼ��������¼����
            if (controlDic.ContainsKey(controlName) || defaultNameList.Contains(controlName)) continue;

            controlDic.Add(controlName, controls[i]);
            //�жϿؼ������� �����Ƿ���¼�����
            if (controls[i] is Button)
            {
                dic_events_ClickButton.Add(controlName, null);
                (controls[i] as Button).onClick.AddListener(() =>
                {
                    dic_events_ClickButton[controlName].Invoke();
                });
            }
            else if (controls[i] is Slider)
            {
                dic_events_ChangeSlider.Add(controlName, null);
                (controls[i] as Slider).onValueChanged.AddListener((value) =>
                {
                    dic_events_ChangeSlider[controlName].Invoke(value);
                });
            }
            else if (controls[i] is Toggle)
            {
                dic_events_ClickToggel.Add(controlName, null);
                (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    dic_events_ClickToggel[controlName].Invoke(value);
                });
            }
        }

    }
}
