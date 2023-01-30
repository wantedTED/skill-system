using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBASkill
{
    public class BlowFlyImpact : IImpactEffect
    {
        private SkillData data;
        public void Execute(SkillDeployer deployer)
        {
            data = deployer.SkillData;
            deployer.StartCoroutine(ContinuousBlowFly(deployer));
        }
        public void BlowFly(Transform transform) 
        {
            CharacterData cd = transform.GetComponent<CharacterData>();
            BuffManager.instance.AllBuffs[0].currentTarget = cd;
            cd.AddBuff(BuffManager.instance.AllBuffs[0]);
            Debug.Log(cd.name);
        }
        IEnumerator ContinuousBlowFly(SkillDeployer deployer) 
        {
            float _time = 0;
            Debug.Log("-----------����Э��");
            do
            {
                Debug.Log("�����ж�"+"time: "+_time);
                yield return new WaitForSeconds(0.05f);
                _time += 0.05f;
                deployer.CalculateTargets();               
                if (data.attackTargets.Length != 0) 
                {
                    foreach (var t in data.attackTargets) 
                    {
                        BlowFly(t);
                    }
                }
            } while (_time < 0.4f);
        }
    }
}

