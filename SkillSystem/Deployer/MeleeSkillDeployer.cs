using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBASkill 
{
    public class MeleeSkillDeployer : SkillDeployer
    {
        public override void DeploySkill()
        {
            //ִ��ѡ���㷨
            CalculateTargets();
            //ִ��Ӱ���㷨
            ImpactTargets();
        }
    }
}

