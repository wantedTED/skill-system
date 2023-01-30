using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBASkill 
{
    public class DeployerConfigFactory//������ʵ��
    {
        public static IAttackSelector CreateAttackSelector(SkillData data) 
        {
            //ѡ���㷨
            string className = string.Format("MOBASkill.{0}AttackSelector", data.selectorType);
            return CreateObject<IAttackSelector>(className);
        }
        public static IImpactEffect[] CreateImpactEffects(SkillData data) 
        {
            IImpactEffect[] impacts = new IImpactEffect[data.impactType.Length];
            //Ӱ��Ч��
            for (int i = 0; i < data.impactType.Length; i++)
            {
                string classname = string.Format("MOBASkill.{0}Impact", data.impactType[i]);
                impacts[i] = CreateObject<IImpactEffect>(classname);
            }
            return impacts;
        }
        private static T CreateObject<T>(string className) where T : class
        {
            Type type = Type.GetType(className);
            return Activator.CreateInstance(type) as T;
        }
    }

}

