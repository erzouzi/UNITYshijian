using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPoint : MonoBehaviour
{
    //造塔点关联的对象
    private GameObject towerObj = null;
    //造塔点关联的塔的数据
    public TowerInfo nowTowerInfo = null;

    //可以建造的三个塔的ID是多少
    public List<int> chooseIDs;

    /// <summary>
    /// 建造一个塔
    /// </summary>
    /// <param name="id"></param>
    public void CreatTower(int id)
    {
        TowerInfo info = GameDataMgr.Instance.towerInfoList[id - 1];
        //如果钱不够就不用建造
        if (info.money > GameLevelMgr.Instance.player.money)
            return;

        //扣钱
        GameLevelMgr.Instance.player.AddMoney(-info.money);
        //创建塔
        //先判断之前是否有塔 如果有就删除
        if(towerObj != null)
        {
            Destroy(towerObj);
            towerObj = null;
        }
        //实例化塔对象
        towerObj = Instantiate(Resources.Load<GameObject>(info.res),this.transform.position, Quaternion.identity);
        //初始化塔
        towerObj.GetComponent<TowerObject>().InitInfo(info);

        //记录当前塔的数据
        nowTowerInfo = info;
        
        //塔建造完毕后 更新游戏界面上的内容
        if(nowTowerInfo.nextLev != 0)
        {
            UIManager.Instance.GetPanel<GamePanel>().UpdataSelTower(this);
        }
        else
        {
            UIManager.Instance.GetPanel<GamePanel>().UpdataSelTower(null);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //如果现在已经有塔了 并且满级 就没用必要显示升级界面
        if (nowTowerInfo != null && nowTowerInfo.nextLev == 0)
            return;
        UIManager.Instance.GetPanel<GamePanel>().UpdataSelTower(this);
    }

    private void OnTriggerExit(Collider other)
    {
        //如果不希望造塔界面显示 直接传空
        UIManager.Instance.GetPanel<GamePanel>().UpdataSelTower(null);
    }

}
