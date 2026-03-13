using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelMgr 
{
    private static GameLevelMgr instance = new GameLevelMgr();
	public static GameLevelMgr Instance => instance;

	public PlayerObject player;

	//所有的出怪点
	private List<MonsterPoint> monsterPoints=new List<MonsterPoint>();

	//记录当前还有多少波怪物
	private int nowWaveNum = 0;
    //记录一共有多少波怪物
    private int maxWaveNum = 0;
	//记录当前场景怪物的数量
	private int nowMonsterNum = 0;
	private GameLevelMgr()
	{

	}

	//1.切换到游戏场景时 我们需要动态创建玩家
	public void InitInfo(SceneInfo info)
	{
		//显示游戏界面
		UIManager.Instance.ShowPanel<GamePanel>();

		//玩家的创建
		//获取之前记录的当前选中的玩家数据
		RoleInfo roleInfo = GameDataMgr.Instance.nowSelRole;
		//首先获取到场景当中玩家的出生位置
		Transform heroPos = GameObject.Find("HeroBornPos").transform;
        //实例化玩家预设体 然后把它的位置角度设置为 场景当中的出生点一致
        GameObject heroObj = GameObject.Instantiate(Resources.Load<GameObject>(roleInfo.res),heroPos.position,heroPos.rotation);
        //让摄像机看向动态创建出来的玩家
        Camera.main.GetComponent<CameraMove>().SetTarget(heroObj.transform);
		//对玩家对象进行初始化
		player = heroObj.GetComponent<PlayerObject>();
		//初始化玩家属性
		player.InitPlayerInfo(roleInfo.atk, info.money);

		//初始化保护区的血量
		MainTowerObject.Instance.UpdateHp(info.towerHp, info.towerHp);

	}
	//2.我们需要通过游戏管理器 来判断游戏是否胜利 
	//要知道场景中是否还有怪物没有出 以及 场景中 是否有还没死亡的怪物

	/// <summary>
	/// 用于记录出怪点的方法
	/// </summary>
	/// <param name="monsterPoint"></param>
	public void AddMonsterPoint(MonsterPoint monsterPoint)
	{
		monsterPoints.Add(monsterPoint);
	}

	/// <summary>
	/// 更新一共有多少波怪
	/// </summary>
	/// <param name="num"></param>
	public void UpdataMaxNum(int num)
	{
		maxWaveNum += num;
		nowWaveNum = maxWaveNum;
		UIManager.Instance.GetPanel<GamePanel>().UpdataWaveNum(nowWaveNum, maxWaveNum);
	}

	public void ChangeNowWaveNum(int num)
	{
		nowWaveNum -= num;
        UIManager.Instance.GetPanel<GamePanel>().UpdataWaveNum(nowWaveNum, maxWaveNum);
    }

	/// <summary>
	/// 检测是否胜利
	/// </summary>
	public bool CheckOver()
	{
		for(int i = 0; i < monsterPoints.Count; i++)
		{
			//只要有一个出怪点还没有出完 那么就证明还没有胜利
			if (!monsterPoints[i].CheckOver())
			{
				return false;
			}
		}
		if (nowMonsterNum > 0)
		{
			return false;
		}

		return true;
	}

	/// <summary>
	/// 改变当前场景上怪物的数量
	/// </summary>
	/// <param name="num"></param>
	public void ChangeMonsterNum(int num)
	{
		nowMonsterNum += num;
	}

	/// <summary>
	/// 清空当前关卡数据 避免影响下一次关卡
	/// </summary>
	public void ClearInfo()
	{
		monsterPoints.Clear();
		nowMonsterNum = nowWaveNum = maxWaveNum = 0;
		player = null;

	}

}
 