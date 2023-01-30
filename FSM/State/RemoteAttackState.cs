using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteAttackState : StateTemplate<EnemyCharacter>
{
    Vector3 escapePos;
    float avoidDistance = 3f;
    bool isRandom = false;
    public RemoteAttackState(int id, EnemyCharacter enemy) : base(id, enemy)
    {

    }
    public override void OnEnter(params object[] args)
    {
        base.OnEnter(args);
    }
    public override void OnStay(params object[] args)
    {
        base.OnStay(args);
        CheckPlayerToEscape(owner.chaseTarget);
        AvoidPlayer(owner.chaseTarget);
    }
    public override void OnExit(params object[] args)
    {
        base.OnExit(args);
        owner.ani.SetBool("Attack", false);
    }
    private void CheckPlayerToEscape(Transform player) 
    {
        owner.transform.LookAt(player);
        if (Vector3.Distance(owner.transform.position, player.position) <= avoidDistance) 
        {
            if (!isRandom) 
            {
                float randomX = Random.Range(1.5f, 3f);
                float randomZ = Random.Range(1.5f, 3f);
                escapePos = new Vector3(owner.transform.position.x + randomX, owner.transform.position.y, owner.transform.position.z + randomZ);
                owner.seeker.StartPath(owner.transform.position, escapePos);
                isRandom = true;
            } 
        }
    }
    private void AvoidPlayer(Transform player)
    {
        owner.ani.SetBool("Run", true);
        owner.missingTargetTime += Time.deltaTime;
        if (owner.path == null)
        {
            return;
        }
        if (owner.currentWayPoint >= owner.path.vectorPath.Count)
        {
            owner.currentWayPoint = 0;
            isRandom = false;
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
        if (Vector3.Distance(owner.transform.position, player.position) <= 4f)
        {
            Debug.Log("到达可攻击的范围");
            owner.ani.SetBool("Run", false);
            owner.Speed = 0;
            owner.ani.SetBool("Attack", true);
        }
        else if ((Vector3.Distance(owner.transform.position, player.position) > 4f)
            && (owner.ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
            && (owner.ani.GetCurrentAnimatorStateInfo(0).IsName("Attack")))
        {
            owner.ani.SetBool("Run", true);
            owner.Speed = 7;
            owner.ani.SetBool("Attack", false);
        }

    }
}
