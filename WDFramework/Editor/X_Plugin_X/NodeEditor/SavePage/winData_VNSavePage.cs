using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace WDEditor
{
    /// <summary>
    /// VNPreset���ô��ڵ�����
    /// </summary>
    [Serializable]
    public class winData_VNSavePage : BaseWindowData
    {
        [NonSerialized]
        public VNSavePage SavePage;
    }
}