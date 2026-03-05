using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public TMP_Text txtInfo;
    public Button btnSure;
    public override void Init()
    {

        btnSure.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }
    /// <summary>
    /// 改变提示内容的方法
    /// </summary>
    /// <param name="info"></param>
    public void changeInfo(string info)
    {
        txtInfo.text = info;
    }
}
