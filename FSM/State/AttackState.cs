using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateTemplate<EnemyCharacter> 
{
    public AttackState(int id, EnemyCharacter enemy) : base(id, enemy) 
    {

    }
    public override void OnEnter(params object[] args)
    {
        base.OnEnter(args);
        owner.seeker.StartPath(owner.transform.position, owner.chaseTarget.position);
    }
    public override void OnStay(params object[] args)
    {
        base.OnStay(args);
        ChasePlayer(owner.chaseTarget);
    }
    public override void OnExit(params object[] args)
    {
        base.OnExit(args);
        owner.ani.SetBool("Attack", false);
    }
    private void ChasePlayer(Transform player) 
    {
        owner.ani.SetBool("Run", true);
        owner.missingTargetTime += Time.deltaTime;
        owner.transform.LookAt(player);
        if (owner.path == null)
        {
            return;
        }
        if (owner.currentWayPoint >= owner.path.vectorPath.Count)
        {
            owner.currentWayPoint = 0;
            return;
        }
        Vector3 dir = (owner.path.vectorPath[owner.currentWayPoint] - owner.transform.position).normalized;
        dir *= owner.Speed * Time.fixedDeltaTime;
        owner.transform.Translate(Vector3.forward * Time.fixedDeltaTime * owner.Speed);
        Quaternion targetRotation = Quaternion.LookRotation(dir);
        owner.transform.rotation = Quaternion.Slerp(owner.transform.rotation, targetRotation, Time.fixedDeltaTime * owner.TurnSpeed);
        if (Vector3.Distance(owner.transform.position, owner.path.vectorPath[owner.currentWayPoint]) < owner.nextWayPointDistance)
        {
            owner.currentWayPoint++;
        }
        if (Vector3.Distance(owner.transform.position, player.position) <= 1f)
        {
            Debug.Log("到达可攻击的范围");
            owner.ani.SetBool("Run", false);
            owner.Speed = 0;
            owner.ani.SetBool("Attack", true);
        }
        else if ((Vector3.Distance(owner.transform.position, player.position) > 1f)
            && (owner.ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            && (owner.ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))) 
        {
            owner.ani.SetBool("Run", true);
            owner.Speed = 7;
            owner.ani.SetBool("Attack", false);
        } 

    }
}
