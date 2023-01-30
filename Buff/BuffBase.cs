using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BuffType 
{
    addVertigo,//ѣ��
    addFloated,//����
    addRepel,//����
    addFear,//�־�
    addSilence,//��Ĭ
    addSlowdown,//����
    addBlindness,//��ä

    addHp,//��Ѫ
    addSpeedup,//����
}
public enum BuffOverlap //��������
{
    none,
    stackedTime,//����ʱ��
    stackedLayer,//���Ӳ���
    resterTime,//����ʱ��
}
public enum BuffCloseType 
{
    All,//ȫ���ر�
    Layer,//���ر�
}
public enum BuffCalculateType//ִ������ 
{
    Once,//һ��
    Loop,//ÿ��
}
[System.Serializable]
public class BuffBase
{
    public BuffType buffType;
    public BuffOverlap buffOverlap;
    public BuffCloseType buffCloseType;
    public BuffCalculateType buffCalculateType;
    public int maxLimit;//������
    public float interalTime;//���ʱ��
    public float timer;//��ʱ��
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
