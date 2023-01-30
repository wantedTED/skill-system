using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeHitState : StateTemplate<EnemyCharacter> 
{
    public BeHitState(int id, EnemyCharacter enemy) : base(id, enemy) 
    {

    }
    public override void OnEnter(params object[] args)
    {
        base.OnEnter(args);
        owner.ani.Play("BeHit");
        owner.Speed = 0;
    }
    public override void OnStay(params object[] args)
    {
        base.OnStay(args);
        if ((owner.ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f)
           && owner.ani.GetCurrentAnimatorStateInfo(0).IsName("BeHit")) 
        {
            owner.stateManager.TranslateState(2);
        }
    }
    public override void OnExit(params object[] args)
    {
        base.OnExit(args);
        owner.Speed= 7;
        owner.damageMemory = 0;
    }
}
