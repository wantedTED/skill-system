using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateTemplate<EnemyCharacter> 
{
    Quaternion initalRot;
    public IdleState(int id, EnemyCharacter enemy) : base(id, enemy) 
    {

    }
    public override void OnEnter(params object[] args)
    {
        base.OnEnter(args);
        initalRot = owner.transform.rotation;
        Debug.Log("½øÈëIdle×´Ì¬");
    }
    public override void OnStay(params object[] args)
    {
        base.OnStay(args);
        owner.ani.SetBool("Idle", true);
        owner.idleTime += Time.deltaTime;
        owner.transform.rotation = initalRot;
    }
    public override void OnExit(params object[] args)
    {
        owner.ani.SetBool("Idle", false);
        base.OnExit(args);
    }
}
