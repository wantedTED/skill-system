using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBASkill 
{
    public interface IAttackSelector
    {
        Transform[] SelectTarget(SkillData data, Transform skillTF);//skillTF�������ڵñ仯���
    }

}
