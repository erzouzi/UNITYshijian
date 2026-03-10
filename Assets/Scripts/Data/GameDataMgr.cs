using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 专门用来管理数据的类
/// </summary>
public class GameDataMgr 
{
    private static GameDataMgr _instance=new GameDataMgr();
    public static GameDataMgr Instance
    {
        get
        {
            return _instance;
        }
    }
    //记录选择的角色数据 用于之后在游戏场景中创建
    public RoleInfo nowSelRole;

    //音效相关数据
    public MusicData musicData;

    //玩家相关数据
    public PlayerData playerData;

    //所有的角色数据
    public List<RoleInfo> roleInfoList;

    //所有的场景数据
    public List<SceneInfo> sceneInfoList;

    //所有的怪物数据
    public List<MonsterInfo> monsterInfoList;
    private GameDataMgr()
    {
        //初始一些默认数据
        musicData=JsonMgr.Instance.LoadData<MusicData>("MusicData");
        //读取角色数据
        roleInfoList=JsonMgr.Instance.LoadData<List<RoleInfo>>("RoleInfo");
        //获取玩家数据
        playerData=JsonMgr.Instance.LoadData<PlayerData>("PlayerData");
        //读取场景数据
        sceneInfoList=JsonMgr.Instance.LoadData<List<SceneInfo>>("SceneInfo");
        //读取怪物数据
        monsterInfoList=JsonMgr.Instance.LoadData<List<MonsterInfo>>("MonsterInfo");
    }

    /// <summary>
    /// 存储音效数据
    /// </summary>
    public  void SaveMusicData()
    {
        JsonMgr.Instance.SaveData(musicData, "MusicData");
    }

    /// <summary>
    /// 存储玩家数据
    /// </summary>
    public void SavePlayerData()
    {
        JsonMgr.Instance.SaveData(playerData, "PlayerData");
    }
}
