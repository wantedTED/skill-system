using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBASkill 
{
    public class DamageImpact : IImpactEffect
    {
        private SkillData data;
        public void Execute(SkillDeployer deployer)
        {
            data = deployer.SkillData;
            deployer.StartCoroutine(RepeatDamage(deployer));
        }
        private IEnumerator RepeatDamage(SkillDeployer deployer)//�ظ��˺� 
        {
            float atkTime = 0;
            do
            {
                yield return new WaitForSeconds(data.attackInterval);
                float realDamage = data.attackNum + data.owner.GetComponent<PlayerCharacter>().PhysicalDamge * 1.2f;
                bool isCrit = false;
                switch (data.disappearType)
                {
                    case DisappearType.CheckOver:
                        if (data.attackTargets.Length > 0) 
                        {
                            Debug.Log("�����ɹ������ټ���");
                            foreach (var e in data.attackTargets)
                            {
                                e.GetComponent<EnemyCharacter>().BeHit(realDamage,false);
                            }
                            deployer.Destroy();

                        }
                        break;
                    case DisappearType.TimeOver:
                        Debug.Log("����������");
                        foreach (var e in data.attackTargets)
                        {
                            if (data.skillId == 0)
                            {
                                isCrit = data.owner.GetComponent<PlayerCharacter>().CritController();
                                e.GetComponent<EnemyCharacter>().BeHit(realDamage, isCrit);
                            }
                            else 
                            {
                                e.GetComponent<EnemyCharacter>().BeHit(realDamage, false);
                            }
                        }
                        break;
                }
                atkTime += data.attackInterval;
                deployer.CalculateTargets();
            } while (atkTime <= data.durationTime);
        }
    }
}

