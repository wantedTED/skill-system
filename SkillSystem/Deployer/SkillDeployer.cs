using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBASkill 
{
    public abstract class SkillDeployer : MonoBehaviour//�����ͷ���
    {
        protected SkillData skillData;
        public SkillData SkillData //���ܹ������ṩ
        {
            get { return skillData; }
            set { skillData = value; InitDeplopyer(); }
        }
        //ѡ���㷨
        private IAttackSelector selector;
        //Ӱ���㷨���� 
        private IImpactEffect[] impactArray;
        //��ʼ���ͷ���
        private void InitDeplopyer()//��ʼ���ͷ��� 
        {
            //ѡ��
            selector = DeployerConfigFactory.CreateAttackSelector(skillData);
            //Ӱ��
            impactArray = DeployerConfigFactory.CreateImpactEffects(skillData);
        }
        //ѡ��
        public void CalculateTargets() 
        {
            skillData.attackTargets = selector.SelectTarget(skillData, this.transform);
        }
        //Ӱ��
        public void ImpactTargets() 
        {
            for (int i = 0; i < impactArray.Length; i++)
            {
                impactArray[i].Execute(this);
            }
        }
        public void Destroy()
        {
            Destroy(this.gameObject);
        }
        public abstract void DeploySkill();//�����ܹ��������ã�������ʵ�֣���������ͷŲ���
    }
}

