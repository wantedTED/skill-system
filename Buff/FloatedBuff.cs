using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FloatedBuff : BuffBase
{
    public FloatedBuff(BuffType buffType, BuffOverlap buffOverlap, BuffCloseType buffCloseType, BuffCalculateType buffCalculateType, int maxLimit, float interalTime, float timer, CharacterData character = null)
        : base(buffType, buffOverlap, buffCloseType, buffCalculateType, maxLimit, interalTime, timer, character) 
    {

    }
    public override void onStart()
    {
        base.onStart();
        currentTarget.GetComponent<NavMeshAgent>().enabled = false;
        currentTarget.GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
        currentTarget.GetComponent<Animator>().Play("BeHit");
        currentTarget.GetComponent<EnemyCharacter>().beControl = true;
        Debug.Log("»÷·É");
    }
    public override void onEffect()
    {
        base.onEffect();
        if (timer >= interalTime) 
        {
            currentTarget.RemoveBuff(this);
        }
        timer += Time.fixedDeltaTime;
    }
    public override void onEnd()
    {
        base.onEnd();
        timer = 0;
        currentTarget.GetComponent<NavMeshAgent>().enabled = true;
        currentTarget.GetComponent<EnemyCharacter>().beControl = false;
    }
}
