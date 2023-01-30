using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBASkill 
{
    public class CharacterSkillManager : MonoBehaviour
    {
        public SkillData[] Skills;//�����б�

        private void Awake()
        {
            foreach (var s in Skills)
            {
                InitSkill(s);
                Debug.Log(s.skillIndicator);
            }
        }
        //��ʼ������
        private void InitSkill(SkillData data)
        {
            if (data.prefabName != null)
            {
                data.skillPrefab = Resources.Load<GameObject>("SkillPrefab/"+data.prefabName);
                data.skillIcon = Resources.Load<Sprite>("SkillIcon/" + data.skillIconName);
                data.owner = this.gameObject;
            }
        }
        //�����ͷ������ж�
        public SkillData PrepareSkill(int id)
        {
            SkillData data = new SkillData();
            foreach (var s in Skills)
            {
                if (s.skillId == id)
                {
                    data = s;
                }
            }
            if (data != null && data.cdRemain <= 0)//���з���ֵ�ж�
            {
                return data;
            }
            else
            {
                return null;
            }
        }
        //���ɼ���
        public void GenerateSkill(SkillData data)
        {
            //��������Ԥ����
            GameObject skillgo = Instantiate(data.skillPrefab, transform.position, transform.rotation);
            Destroy(skillgo, data.durationTime);
            //���ݼ�������
            SkillDeployer deployer = skillgo.GetComponent<SkillDeployer>();
            deployer.SkillData = data;
            deployer.DeploySkill();
            StartCoroutine(CoolTimeDown(data));//������ȴ
        }
        //������ȴ
        private IEnumerator CoolTimeDown(SkillData data)
        {
            data.cdRemain = data.skillCd;
            while (data.cdRemain > 0)
            {
                yield return new WaitForSeconds(1f);
                data.cdRemain -= 1;
                Debug.Log("��ǰ����" + data.name + "��ȴʣ��ʱ��" + data.cdRemain);
            }
        }
        //��������
        public void SkillLevelUp(int id) 
        {
            foreach (SkillData s in Skills)
            {
                if (s.skillId == id) 
                {
                    s.level += 1;
                }
            }
        }
    }
}

