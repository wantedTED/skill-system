using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Werewolf.StatusIndicators.Components;

namespace MOBASkill 
{
    public class CharacterSkillSystem : MonoBehaviour //��װ����ϵͳ���ṩ�򵥵ļ����ͷŹ���
    {
        private CharacterSkillManager skillManager;
        private Animator animator;
        private SplatManager sm;

        public SkillData skill;

        int count;
        private void Start()
        {
            skillManager = GetComponent<CharacterSkillManager>();
            animator = GetComponent<Animator>();
            sm = GetComponent<SplatManager>();

            count = 0;
        }
        public void DeploySkill() 
        {
            skillManager.GenerateSkill(skill);
        }
        public bool OpenSkillIndicator(int skillID) 
        {
            skill = skillManager.PrepareSkill(skillID);
            if (skill == null || skill.skillIndicator == "")
            {
                Debug.Log("�����ڼ���ָʾ��" );
                return false;
            }
            else
            {
                Debug.Log("�򿪼���ָʾ��");
                sm.SelectSpellIndicator(skill.skillIndicator);
                return true;
            }
        }
        public void CloseSkillIndicator() 
        {
            sm.CancelSpellIndicator();
        }
        public void AttackUseSkill(int skillID)
        {
            skill = skillManager.PrepareSkill(skillID);
            if (skill == null) { return; }
            if (skill.animationName.Length != 1)
            {
                animator.Play(skill.animationName[count]);
                count += 1;
                if (count >= skill.animationName.Length) 
                {
                    count = 0;
                }
            }
            else 
            {
                animator.Play(skill.animationName[0]);
            }
            DeploySkill();
        }
    }
}


