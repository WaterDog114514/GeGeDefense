using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : Singleton<InputManager>, IFrameworkSystem
{
    /// <summary>
    /// һ���¼�ֻ�ܰ�һ��������Ϣ��һ��������Ϣ���������ȼ��򵥸�����¼�
    /// </summary>
    public Dictionary<E_InputEvent, InputInfo> dic_Input = new Dictionary<E_InputEvent, InputInfo>();
    public IFrameworkSystem InitializedSystem()
    {
        Debug.Log("�Ѿ���ʼ������ϵͳ");
        //����������
        UpdateSystem.Instance.AddUpdateListener(E_UpdateLayer.FrameworkSystem,InputUpdate);
        return Instance ;
    }

    //�Ƿ���������ϵͳ���
    private bool isOpenInputCheck = true;
    public InputManager()
    {
    }



    /// <summary>
    /// �������߹ر����ǵ��������ģ��ļ��
    /// </summary>
    /// <param name="isStart"></param>
    public void StartOrCloseInputMgr(bool isStart)
    {
        this.isOpenInputCheck = isStart;
    }
    //���뷽ʽ������ʱ�򶨺ã�������Ҫ�޸����뷽ʽ����������ע��
    /// <summary>
    /// ע����з���Ŀ�ݼ�
    /// </summary>
    /// <param name="key"></param>
    /// <param name="inputType"></param>
    public void RegisterDirectionKeyInfo(E_InputEvent Event, InputKey PositiveKey, InputKey NegativeKey, bool isFaded = true)
    {
        //���ڵĻ��͸ļ� �ļ��ɹ�ֱ��OK
        if (dic_Input.ContainsKey(Event)) RemoveInputInfo(Event);

        //��һ�γ�ʼ��
        DirectionKeyInputInfo inputInfo = new DirectionKeyInputInfo(Event, PositiveKey, NegativeKey, isFaded);
        dic_Input.Add(Event, inputInfo);

    }
    /// <summary>
    /// ע����������¼�
    /// </summary>
    public void RegisterMouseKeyInputInfo(E_InputEvent Event, E_MouseInputType e_MouseInputType, int mouseID = 0, bool isFaded = true)
    {
        //���ڵĻ���ɾ������ע��
        if (dic_Input.ContainsKey(Event))
            RemoveInputInfo(Event);

        //��һ�γ�ʼ��
        MouseMoveOrScrollInfo inputInfo = new MouseMoveOrScrollInfo(Event, e_MouseInputType);
        dic_Input.Add(Event, inputInfo);

    }
    /// <summary>
    /// ע����ͨ���̰�ť�����¼�
    /// </summary>
    public void RegisterButtonKeyInputInfo(E_InputEvent Event, InputKey ButtonKey, E_KeyInputType e_KeyInputType, bool isFaded = true)
    {
        //���ڵĻ��͸ļ� �ļ��ɹ�ֱ��OK
        if (dic_Input.ContainsKey(Event)) RemoveInputInfo(Event);

        //��һ�β���
        ButtonKeyInputInfo inputInfo = new ButtonKeyInputInfo(Event, ButtonKey, e_KeyInputType);
        dic_Input.Add(Event, inputInfo);
    }
    //�����ļ��߼�
    /// <summary>
    /// �ķ������ֻ��ͬʱ��һ�����ɹ�����true
    /// </summary>
    /// <param name="Event"></param>
    /// <param name="Key"></param>
    /// <returns></returns>
    public bool ChangeDirectionKeyInfo(E_InputEvent Event, bool isPositive, InputKey Key)
    {
        if (!dic_Input.ContainsKey(Event))
        {
            Debug.LogError("�ļ�ʧ��,û�и��������¼�ע�ᰴ������");
            return false;
        }
        DirectionKeyInputInfo inputInfo = dic_Input[Event] as DirectionKeyInputInfo;
        if (inputInfo == null)
        {
            Debug.LogError("�ļ�ʧ��,�ÿ�ݼ����Ƿ����ݼ�����");
            return false;
        }
        //��NoneΪ����
        if (isPositive) inputInfo.positiveKey = Key;
        else inputInfo.negativeKey = Key;
        return true;
    }
    /// <summary>
    /// �޸������������ ���ǹ��ֻ�������ƶ�
    /// </summary>
    /// <param name="Event"></param>
    /// <param name="mouseID"></param>
    /// <returns></returns>
    public bool ChangeMouseKeyDownInfo(E_InputEvent Event, E_MouseInputType e_MouseInputType)
    {
        if (!dic_Input.ContainsKey(Event))
        {
            Debug.LogError("�ļ�ʧ��,û�и��������¼�ע�ᰴ������");
            return false;
        }
        MouseMoveOrScrollInfo inputInfo = dic_Input[Event] as MouseMoveOrScrollInfo;
        //��ô�ͽ����Ϊ
        if (inputInfo == null)
        {
            Debug.LogError("�ļ�ʧ��,�޸ĵĲ�����������¼�");
            return false;
        }
        inputInfo.inputType = e_MouseInputType;
        return true;
    }
    /// <summary>
    /// ����ͨ���̿�ݼ�
    /// </summary>
    /// <param name="Event"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool ChangeButtonKeyInfo(E_InputEvent Event, InputKey key)
    {
        if (!dic_Input.ContainsKey(Event))
        {
            Debug.LogError("�ļ�ʧ��,û�и��������¼�ע�ᰴ������");
            return false;
        }
        ButtonKeyInputInfo inputInfo = dic_Input[Event] as ButtonKeyInputInfo;
        if (inputInfo == null)
        {
            Debug.LogError("�ļ�ʧ��,�ÿ�ݼ�������ͨ��ť����");
            return false;
        }
        inputInfo.ButtonKey = key;
        return true;
    }
    /// <summary>
    /// �Ƴ�ָ�������¼���Ϊ���������
    /// </summary>
    public void RemoveInputInfo(E_InputEvent Event)
    {
        if (dic_Input.ContainsKey(Event))
            dic_Input.Remove(Event);
    }
    /// <summary>
    /// ȡ��ĳ������Ϣ
    /// </summary>
    /// <returns></returns>
    public InputInfo GetKeyInfo(E_InputEvent Event)
    {
        if (dic_Input.ContainsKey(Event))
            return dic_Input[Event];
        else return null;
    }
    #region �������ļ����
    /// <summary>
    /// ͨ������������޸���ͨ����
    /// </summary>
    /// <param name="callBack"></param>
    public void ChangeButtonInfoFromInput(E_InputEvent Event)
    {
        UpdateSystem.Instance.StartCoroutine(StartChangeButtonFromInput(Event));
    }

    /// <summary>
    /// ͨ������������޸ķ����ȼ�
    /// </summary>
    /// <param name="callBack"></param>
    public void ChangeDirctionInfoFromInput(E_InputEvent Event, bool isPostive)
    {
        UpdateSystem.Instance.StartCoroutine(StartChangeDirectionFromInput(Event, isPostive));
    }

    /// <summary>
    /// �ļ������⣬������ʱ�������е����������ֱ������һ����λΪֹ
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitKeyInputCheckFromInput(UnityAction<int, KeyCode> callback)
    {
        //��һ֡
        yield return 0;
        //�����ѭ����⣬ֱ������һ����λ��

        //û�а����͵���һ֡
        while (!Input.anyKeyDown) yield return null;
        Debug.Log("����");
        //������ɹ��󴫳�ȥ�ļ�λ
        KeyCode key = KeyCode.None;
        int mouseId = -1;
        //���̺����İ�������
        //����
        Array keyCodes = Enum.GetValues(typeof(KeyCode));
        foreach (KeyCode inputKey in keyCodes)
        {
            //�жϵ�����˭�������� ��ô�Ϳ��Եõ���Ӧ������ļ�����Ϣ
            if (Input.GetKeyDown(inputKey))
            {
                key = inputKey;
                break;
            }
        }
        //���
        for (int i = 0; i < 3; i++)
        {
            if (Input.GetMouseButtonDown(i))
            {
                mouseId = i;
                break;
            }
        }
        callback?.Invoke(mouseId, key);

    }
    //��ʼͨ�����������иļ� �ĵ���ͨ����
    private IEnumerator StartChangeButtonFromInput(E_InputEvent Event)
    {
        yield return UpdateSystem.Instance.StartCoroutine(WaitKeyInputCheckFromInput((mouseId, key) =>
        {
            InputInfo changeInfo = dic_Input[Event];
            //�������
            if (mouseId != -1)
                ChangeButtonKeyInfo(Event, new InputKey(mouseId));
            //���˼���
            else if (key != KeyCode.None && mouseId == -1)
                ChangeButtonKeyInfo(Event, new InputKey(key));
        }));

    }
    private IEnumerator StartChangeDirectionFromInput(E_InputEvent Event, bool isPositive)
    {
        yield return UpdateSystem.Instance.StartCoroutine(WaitKeyInputCheckFromInput((mouseId, key) =>
        {
            InputInfo changeInfo = dic_Input[Event];
            //�������
            if (mouseId != -1)
                ChangeDirectionKeyInfo(Event, isPositive, new InputKey(mouseId));
            //���˼���
            else if (key != KeyCode.None && mouseId == -1)
                ChangeDirectionKeyInfo(Event, isPositive, new InputKey(key));
        }));

    }
    #endregion
    private void InputUpdate()
    {
        //����ⲿû�п�����⹦�� �Ͳ�Ҫ���
        if (!isOpenInputCheck)
            return;

        foreach (InputInfo info in dic_Input.Values)
        {
            //�ȼ�ⰴ���������¼�
            info.KeyUpdate();
            //�ٸ����Ƿ��������ֵ
            info.ValueUpdate();
            //�������Ƿ��������ȫ�ּ���
            info.TriggerUpdate();
        }
    }
    //��ȡ�����ļ��߼�
    public void ReadConfig()
    {
        throw new NotImplementedException();
    }
    //���������ļ��߼�
    public void SaveConfig()
    {
        throw new NotImplementedException("meixie");

    }
  

}
