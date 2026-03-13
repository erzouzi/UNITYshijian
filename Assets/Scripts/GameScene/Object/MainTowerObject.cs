using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTowerObject : MonoBehaviour
{
    //血量相关
    private int hp;
    private int maxHp;
    //是否死亡
    private bool isDead;

    //能够被别人快速获取位置
    private static MainTowerObject instance;
    public static MainTowerObject Instance => instance;
    private void Awake()
    {
        instance = this;
    }


    //更新血量
    public void UpdateHp(int hp,int maxHp)
    {
        this.hp = hp;
        this.maxHp = maxHp;

        //更新界面血量显示
        UIManager.Instance.GetPanel<GamePanel>().UpdateTowerHP(hp, maxHp);
    }

    //自己受到伤害
    public void Wound(int dmg)
    {
        //如果已经死亡了 就不再受到伤害了
        if (isDead)
            return;
        //收到伤害
        hp -= dmg;
        //死亡逻辑
        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
            //游戏结束
            GameOverPanel panel =UIManager.Instance.ShowPanel<GameOverPanel>();
            //得到奖励的一半
            panel.InitInfo((int)(GameLevelMgr.Instance.player.money * 0.5f), false);
        }
        UpdateHp(hp, maxHp);
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }
}
