using System.Collections;
using System.Collections.Generic;
using System.Net;
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

    //当前进入和选中的造塔点
    private TowerPoint nowSelTowerPoint;

    //用来标识 是否检测造塔输入
    private bool checkInput;

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
        //锁定鼠标
        Cursor.lockState = CursorLockMode.Confined;
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

    /// <summary>
    /// 更新当前选中造塔点 界面的一些变化 
    /// </summary>
    public void UpdataSelTower( TowerPoint point )
    {
        //根据造塔点的信息决定界面上显示的内容
        nowSelTowerPoint = point;
        //如果传入数据是空 直接隐藏造塔点界面
        if( nowSelTowerPoint == null )
        {
            checkInput = false;
            //隐藏下方造塔按钮
            botTrans.gameObject.SetActive(false);
        }
        else
        {
            checkInput = true;
            //显示下方造塔按钮
            botTrans.gameObject.SetActive(true);
            //如果没有造过塔
            if (nowSelTowerPoint.nowTowerInfo == null)
            {
                for (int i = 0; i < towerBtns.Count; i++)
                {
                    towerBtns[i].gameObject.SetActive(true);
                    towerBtns[i].InitInfo(nowSelTowerPoint.chooseIDs[i], "数字键" + (i + 1));
                }
            }
            //如果造过塔
            else
            {
                for (int i = 0; i < towerBtns.Count; i++)
                {
                    towerBtns[i].gameObject.SetActive(false);
                }
                towerBtns[1].gameObject.SetActive(true);
                towerBtns[1].InitInfo(nowSelTowerPoint.nowTowerInfo.nextLev, "空格键");
            }
        }


    }

    protected override void Update()
    {
        base.Update();

        //主要用于造塔点 键盘输入 造塔
        if(!checkInput)
            return;
        //如果没有造过塔就检测 1 2 3 按键建造塔
        if(nowSelTowerPoint.nowTowerInfo == null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                nowSelTowerPoint.CreatTower(nowSelTowerPoint.chooseIDs[0]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                nowSelTowerPoint.CreatTower(nowSelTowerPoint.chooseIDs[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                nowSelTowerPoint.CreatTower(nowSelTowerPoint.chooseIDs[2]);
            }
        }
        //如果造过塔 就检测空格键 升级塔
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                nowSelTowerPoint.CreatTower(nowSelTowerPoint.nowTowerInfo.nextLev);
            }
        }
    }
}
