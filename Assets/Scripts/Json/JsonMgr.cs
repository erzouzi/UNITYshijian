using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// 序列化和反序列化json时使用的哪种方案
/// </summary>
public enum jsonType
{
    jsonUtlity,
    LitJson
}

/// <summary>
/// json数据管理类 主要用于json的序列化和反序列化 
/// </summary>
public class JsonMgr 
{
    private static JsonMgr instance=new JsonMgr();
    public static  JsonMgr Instance=>instance;

    private JsonMgr() { }
    
    //存储json数据 序列化
    public void SaveData(object data,string fileName, jsonType type = jsonType.LitJson)
    {
        //确定存储路径
        string path= Application.persistentDataPath+"/"+fileName+".json";
        //序列化得到json字符串
        string jsonStr = "";

        switch (type)
        {
            case jsonType.jsonUtlity:
                jsonStr=JsonUtility.ToJson(data);
                break;
            case jsonType.LitJson:
                jsonStr=JsonMapper.ToJson(data);
                break;
            default:
                break;
        }
        //把序列化的字符串存储到指定路径的文件中
        File.WriteAllText(path, jsonStr);
    }

    //读取指定文件中的json数据 反序列化
    public T LoadData<T>(string fileName,jsonType type = jsonType.LitJson) where T:new()
    {
        //确定从哪个路径读取
        //先判断默认数据文件夹中是否有我们想要的数据 如果有就从中获取
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        //先判断 是否存在这个文件
        //如果不存在默认文件 就从读写文件夹中去找
        if (!File.Exists(path))
        {
            path = Application.persistentDataPath + "/" + fileName + ".json";
        }
        //如果读写文件夹中还没有 就返回默认对象
        if(!File.Exists(path))
        {
            return new T();
        }
        //进行反序列化
        string jsonStr=File.ReadAllText(path);
        T data = default(T);
        switch (type)
        {
            case jsonType.jsonUtlity:
                data=JsonUtility.FromJson<T>(jsonStr);
                break;
            case jsonType.LitJson:
                data=JsonMapper.ToObject<T>(jsonStr);
                break;
            default:
                break;
        }
        //把对象返回出去
        return data;
    }
}