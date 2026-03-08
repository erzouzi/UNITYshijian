using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    //左上角控件
    public Image imgHP;
    public TMP_Text txtHP;
    public TMP_Text txtWave;
    public TMP_Text txtMoney;

    //HP的初始宽度 可以在外部进行控制
    public float hpW=500;

    //右上角控件
    public Button btnQuit;

    //下方造塔组合控件的父对象 用于控制显隐
    public Transform botTrans;

    //管理3个复合控件
    public List<TowerBtn> towerBtns=new List<TowerBtn>();
    public override void Init()
    {
        //监听按钮事件
        btnQuit.onClick.AddListener(() =>
        {
            //隐藏游戏界面
            UIManager.Instance.HidePanel<GamePanel>();
            //返回开始界面
            SceneManager.LoadScene("BeginScene");
            //其他

        });

        //一开始隐藏造塔相关UI
        botTrans.gameObject.SetActive(false);
    }

    public void UpdateTowerHP(int hp,int maxHp)
    {
        //更新HP文本
        txtHP.text = hp + "/" + maxHp;
        //更新HP图片的宽度
        imgHP.rectTransform.sizeDelta = new Vector2((float)hp / maxHp*hpW, imgHP.rectTransform.sizeDelta.y);
    }

    /// <summary>
    /// 更新剩余波数
    /// </summary>
    /// <param name="nowNum">当前波数</param>
    /// <param name="maxNum">最大波数</param>
    public void UpdataWaveNum(int nowNum,int maxNum)
    {
        txtWave.text = "第"+nowNum+"波/"+maxNum+"波";
    }

    /// <summary>
    /// 更新金币
    /// </summary>
    /// <param name="money">金币</param>
    public void UpdateMoney(int money)
    {
        txtMoney.text = money.ToString();
    }
}
