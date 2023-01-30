using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BuffType 
{
    addVertigo,//眩晕
    addFloated,//击飞
    addRepel,//击退
    addFear,//恐惧
    addSilence,//沉默
    addSlowdown,//减速
    addBlindness,//致盲

    addHp,//加血
    addSpeedup,//加速
}
public enum BuffOverlap //叠加类型
{
    none,
    stackedTime,//增加时间
    stackedLayer,//增加层数
    resterTime,//重置时间
}
public enum BuffCloseType 
{
    All,//全部关闭
    Layer,//逐层关闭
}
public enum BuffCalculateType//执行类型 
{
    Once,//一次
    Loop,//每次
}
[System.Serializable]
public class BuffBase
{
    public BuffType buffType;
    public BuffOverlap buffOverlap;
    public BuffCloseType buffCloseType;
    public BuffCalculateType buffCalculateType;
    public int maxLimit;//最大次数
    public float interalTime;//间隔时间
    public float timer;//计时器
    public CharacterData currentTarget;
    public BuffBase(BuffType buffType, BuffOverlap buffOverlap, BuffCloseType buffCloseType, BuffCalculateType buffCalculateType, int maxLimit, float interalTime, float timer, CharacterData character = null)
    {
        this.buffType = buffType;
        this.buffOverlap = buffOverlap;
        this.buffCloseType = buffCloseType;
        this.buffCalculateType = buffCalculateType;
        this.maxLimit = maxLimit;
        this.interalTime = interalTime;
        this.timer = timer;
        this.currentTarget = character;
    }

    public BuffBase()
    {
    }

    public virtual void onStart() 
    {
    }
    public virtual void onEffect() 
    {
    }
    public virtual void onEnd() 
    {
    }
}
