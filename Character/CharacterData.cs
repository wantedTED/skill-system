using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum CharacterState 
{
    Idle,
    Move,
    Attack,
    UseSkill,
    InBuff,
    Behit,
    Death,
}

public class CharacterData : MonoBehaviour
{
    public int Level;//等级
    public float MaxHp;//最大生命值
    public float MaxMp;//最大法力值
    public float Speed;//速度
    public float TurnSpeed;//转身速度
    public float PhysicalDamge;//物攻
    public float MagicDamge;//法攻
    public float Defense;//物防
    public float Crit;//暴击率
    public float DOD;//闪避率
    public float Ex;//经验值

    public int Strength;//力量
    public int Intellect;//智力
    public int Agility;//敏捷
    public int Stamina;//耐力

    public CharacterState State;
    public Image HealthBar;

    public List<BuffBase> buffs = new List<BuffBase>();

    void FixedUpdate()
    {
        ReFreshBuff();
    }
    public void AddBuff(BuffBase buff) 
    {
        if (buffs.Contains(buff)) 
        {
            Debug.Log("有了");
            return;
        }
        else
        {
            Debug.Log("成功添加Buff");
            buffs.Add(buff);
            buff.onStart();
        }
    }
    public void ReFreshBuff() 
    {
        for (int i = buffs.Count - 1; i >= 0; i --)
        {
            buffs[i].onEffect();
        }
    }
    public void RemoveBuff(BuffBase buff) 
    {
        buffs.Remove(buff);
        buff.onEnd();
    }
}
