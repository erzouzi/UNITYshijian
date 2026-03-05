using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    // Start is called before the first frame update
    private static BKMusic instance;
    public static BKMusic Instance => instance;

    private AudioSource bkSource;
    private void Awake()
    {
        instance = this;
        bkSource = GetComponent<AudioSource>();

        //通过数据来设置音乐的大小和开关
        MusicData data=GameDataMgr.Instance.musicData;
        SetIsOpen(data.musicOpen);
        ChangeValue(data.musicValue);
    }

    //开关背景音乐的方法
    public void SetIsOpen(bool isOpen)
    {
        bkSource.mute = !isOpen;
    }

    //调整背景音乐大小的方法
    public void ChangeValue(float value)
    {
        bkSource.volume = value;
    }
}
