using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MoveState : StateTemplate<EnemyCharacter>
{
    public MoveState(int id, EnemyCharacter enemy) : base(id, enemy) 
    {

    }
    public override void OnEnter(params object[] args)
    {
        base.OnEnter(args);
        Debug.Log("进入巡逻状态");
        owner.seeker.pathCallback += OnPathComplete;
        owner.seeker.StartPath(owner.transform.position, owner.currentPos);
        owner.transform.LookAt(owner.currentPos);
        owner.ani.SetBool("Run", true);
    }
    public override void OnStay(params object[] args)
    {
        base.OnStay(args);
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
    }
    public override void OnExit(params object[] args)
    {
        owner.ani.SetBool("Run", false);
        base.OnExit(args);
    }
    private void OnPathComplete(Path p) 
    {
        Debug.Log("敌人发现这个路线" + p.error);
        if (!p.error) 
        {
            owner.path = p;
            owner.currentWayPoint = 0;
        }
    }
}
