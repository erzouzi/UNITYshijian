using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public TMP_Text txtWin;
    public TMP_Text txtInfo;
    public TMP_Text txtMoney;

    public Button btnSure;

    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            //隐藏面板 清空当前关卡数据 切换场景
            UIManager.Instance.HidePanel<GamePanel>();
            UIManager.Instance.HidePanel<GameOverPanel>();

            GameLevelMgr.Instance.ClearInfo();

            SceneManager.LoadScene("BeginScene");

        });
    }

    public void InitInfo(int money,bool isWin)
    {
        txtWin.text = isWin ? "通关" : "失败";
        txtInfo.text = isWin ? "获得胜利奖励" : "获得失败奖励";

        txtMoney.text = "￥" + money;

        //根据奖励改变玩家数据
        GameDataMgr.Instance.playerData.haveMoney += money;
        GameDataMgr.Instance.SavePlayerData();

    }

    public override void ShowMe()
    {
        base.ShowMe();
        Cursor.lockState = CursorLockMode.None;
    }
}
