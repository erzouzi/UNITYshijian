using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 组合控件方便控制造塔相关 UI的更新逻辑
/// </summary>
public class TowerBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public Image imgPic;

    public TMP_Text txtTip;

    public TMP_Text txtMoney;


    /// <summary>
    /// 初始化按钮信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="inputStr"></param>
    public void InitInfo(int id, string inputStr)
    {
        TowerInfo info =GameDataMgr.Instance.towerInfoList[id -1];
        imgPic.sprite = Resources.Load<Sprite>(info.imgRes);
        txtMoney.text = "￥" + info.money;
        txtTip.text = inputStr;
        //判断钱够不够
        if (info.money > GameLevelMgr.Instance.player.money)
            txtMoney.text = "金钱不足";
    }

}
