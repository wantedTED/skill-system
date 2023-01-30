using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MOBASkill
{
    public enum SkillAttackType
    {
        single,
        aoe,
    }
    public enum SelectorType
    {
        none,
        Sector,
        Rectangular,
    }
    public enum DisappearType 
    {
        TimeOver,
        CheckOver,
    }

    [Serializable]
    public class SkillData
    {
        public int skillId;//����ID
        public string name;//��������
        public string description;//��������
        public float skillCd;//����CD
        public float cdRemain;//����CD������
        public int costMp;//������
        public float attackDistance;//���ܾ���
        public float attackAngle;//���ܹ����Ƕ�
        public string[] attackTargetTags = { "Enemy" };//�ɹ�����Ŀ��Tag
        [HideInInspector]
        public Transform[] attackTargets;//����Ŀ���������
        public string[] impactType = { "CostMP", "Damage" };//����Ӱ������
        public int nextBatterld;//������дһ�����ܱ��----��ʱ����
        public float attackNum;//�˺���ֵ
        public float durationTime;//����ʱ��
        public float attackInterval;//�˺����
        [HideInInspector]
        public GameObject owner;//���������Ľ�ɫ
        public string prefabName;//����Ԥ��������
        [HideInInspector]
        public GameObject skillPrefab;//Ԥ�������
        public string[] animationName ;//��������
        public string hitFxName;//�ܻ���Ч����
        [HideInInspector]
        public GameObject hitFxPrefab;//�ܻ���ЧԤ����
        public int level;//���ܵȼ�
        public SkillAttackType attackType;//AOE���ߵ���
        public SelectorType selectorType;//�ͷ����ͣ�Բ�Σ����Σ����Σ�
        public string skillIndicator;//����ָʾ������
        public string skillIconName;//������ʾͼ������
        [HideInInspector]
        public Sprite skillIcon;//�����¼�ͼ��
        public DisappearType disappearType;//����Ԥ������ʧ��ʽ      
    }
}

