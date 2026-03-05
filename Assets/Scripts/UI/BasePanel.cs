using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    //专门用于控制面板透明度的组件
    private CanvasGroup canvasGroup;
    //淡入淡出的速度
    private float alphaSpeed = 10f;
    //是否处于显示状态
    public bool isShow=false;

    //当隐藏完毕后回调的方法
    private UnityAction hideCallBack=null;
    protected virtual void Awake()
    {
        //获取CanvasGroup组件
        canvasGroup = GetComponent<CanvasGroup>();
        //如果忘记添加CanvasGroup组件 就自动添加一个
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    protected virtual void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        //当处于显示时，如果alpha不为1就会不停加到1 加到1就停止
        //淡入
        if (isShow && canvasGroup.alpha!=1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if(canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        //淡出
        else if (!isShow && canvasGroup.alpha!=0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                //让面板完全隐藏后调用回调方法
                hideCallBack?.Invoke();
            }
        }
    }

    /// <summary>
    /// 注册控件的事件方法 所有的子面板都必须重写这个方法 来注册自己的事件
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// 显示自己时做的逻辑
    /// </summary>
    public virtual void ShowMe()
    {
        canvasGroup.alpha = 0;
        isShow= true;
    }
    /// <summary>
    /// 隐藏自己时做的逻辑
    /// </summary>
    public virtual void HideMe(UnityAction callBack)
    {
        canvasGroup.alpha = 1;
        isShow= false;
        hideCallBack = callBack;
    }
}
