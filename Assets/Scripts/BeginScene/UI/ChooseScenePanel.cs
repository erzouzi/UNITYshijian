using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseScenePanel : BasePanel
{
    //四个按钮组件
    public Button btnLeft;
    public Button btnRight;
    public Button btnStart;
    public Button btnBack;

    //用于文本和图片更新
    public TMP_Text txtInfo;
    public Image imgScene;

    //记录当前数据索引
    private int nowIndex = 0;
    //记录当前选择的数据
    private SceneInfo nowSceneInfo;


    public override void Init()
    {
        btnLeft.onClick.AddListener(() =>
        {
            --nowIndex;
            if (nowIndex < 0)
            {
                nowIndex =GameDataMgr.Instance.sceneInfoList.Count-1;
            }
            ChangeScene();
        });

        btnRight.onClick.AddListener(() =>
        {
            ++nowIndex;
            if (nowIndex >= GameDataMgr.Instance.sceneInfoList.Count)
            {
                nowIndex = 0;
            }
            ChangeScene();
        });

        btnStart.onClick.AddListener(() =>
        {
            //隐藏当前面板
            UIManager.Instance.HidePanel<ChooseScenePanel>();

            //切换场景


        });

        btnBack.onClick.AddListener(() =>
        {
            //隐藏当前面板
            UIManager.Instance.HidePanel<ChooseScenePanel>();

            //显示角色选择面板
            UIManager.Instance.ShowPanel<ChooseHeroPanel>();
        });

        //打开面板就要初始化一次 更新信息
        ChangeScene();
    }

    /// <summary>
    /// 切换界面显示的场景信息
    /// </summary>
    public void ChangeScene()
    {
        nowSceneInfo =GameDataMgr.Instance.sceneInfoList[nowIndex];
        //更新图片和文字信息
        imgScene.sprite = Resources.Load<Sprite>(nowSceneInfo.imgRes);

        txtInfo.text="名称:\n"+nowSceneInfo.name+"\n"+"描述:\n"+nowSceneInfo.tips;
    }
}
