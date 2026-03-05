using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家数据
/// </summary>
public class PlayerData 
{
    //玩家拥有的金钱数量
    public int haveMoney = 0;
    //玩家解锁了那些角色
    public List<int> buyHero=new List<int>();
}
