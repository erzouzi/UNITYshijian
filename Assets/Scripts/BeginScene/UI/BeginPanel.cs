using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnStart;
    public Button btnSetting;
    public Button btnQuit;
    public override void Init()
    {
        btnStart.onClick.AddListener(() =>
        {
            //播放摄像机左转动画 播放结束后 显示显角面板
            Camera.main.GetComponent<CameraAnimator>().TurnLeft(() =>
            {
                UIManager.Instance.ShowPanel<ChooseHeroPanel>();
            });

            //隐藏开始面板
            UIManager.Instance.HidePanel<BeginPanel>();
        });
        btnSetting.onClick.AddListener(() =>
        {
            //显示设置面板
            UIManager.Instance.ShowPanel<SettingPanel>();
        });
        btnQuit.onClick.AddListener(() =>
        {
            //退出游戏
            Application.Quit(); 
        });
    }
}
