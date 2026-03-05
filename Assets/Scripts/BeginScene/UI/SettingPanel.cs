using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Button btnClose;
    public Toggle togMusic;
    public Toggle togSound;
    public Slider sliderMusic;
    public Slider sliderSound; 
    public override void Init()
    {
        //初始化面板显示内容 根据本地存储的设置数据来进行初始化
        MusicData data=GameDataMgr.Instance.musicData;
        //初始化背景音乐和音效的开关
        togMusic.isOn = data.musicOpen;
        togSound.isOn = data.soundOpen;
        //初始化背景音乐和音效的大小
        sliderMusic.value = data.musicValue;
        sliderSound.value = data.soundValue;


        btnClose.onClick.AddListener(() =>
        {
            //为了节约性能 只有当设置完成 关闭面板时才会将数据进行保存
            GameDataMgr.Instance.SaveMusicData();
            UIManager.Instance.HidePanel<SettingPanel>();
        });

        togMusic.onValueChanged.AddListener((isOn) =>
        {
            //让背景音乐进行开关
            BKMusic.Instance.SetIsOpen(isOn);
            //记录开关数据
            GameDataMgr.Instance.musicData.musicOpen = isOn;
        });

        togSound.onValueChanged.AddListener((isOn) =>
        {
            //记录音效开关数据
            GameDataMgr.Instance.musicData.soundOpen = isOn;
        });

        sliderMusic.onValueChanged.AddListener((value) =>
        {
            //调整背景音乐的大小
            BKMusic.Instance.ChangeValue(value);
           //记录背景音乐大小数据
            GameDataMgr.Instance.musicData.musicValue = value;
        });

        sliderSound.onValueChanged.AddListener((value) =>
        {
            GameDataMgr.Instance.musicData.soundValue = value;
        });

    }
}
