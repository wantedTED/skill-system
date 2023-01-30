using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBASkill 
{
    //���� Բ��
    public class SectorAttackSelector : IAttackSelector
    {
        GameObject[] tempGOArray;
        public Transform[] SelectTarget(SkillData data, Transform skillTF)
        {
            //���ݼ��������еñ�ǩ ��ȡ����Ŀ��
            List<Transform> taragets = new List<Transform>();
            for (int i = 0; i < data.attackTargetTags.Length; i++)
            {
                tempGOArray = GameObject.FindGameObjectsWithTag(data.attackTargetTags[i]);
            }
            for (int i = 0; i < tempGOArray.Length; i++)
            {
                taragets.Add(tempGOArray[i].GetComponent<Transform>());
            }
            //�жϹ�����Χ
            taragets = taragets.FindAll(t =>
              Vector3.Distance(t.position, skillTF.position) <= data.attackDistance &&
              Vector3.Angle(skillTF.forward, t.position - skillTF.position) <= data.attackAngle / 2
            );
            //����Ŀ��
            Transform[] result = taragets.ToArray();
            if (result.Length == 0)
            {
                Debug.Log("û�е���");
                return result;
            }
            else 
            {
                if (data.attackType == SkillAttackType.single)
                {
                    Transform[] temp = new Transform[result.Length];
                    float minD = Vector3.Distance(result[0].position, skillTF.position);
                    temp[0] = result[0];
                    for (int i = 1; i < result.Length; i++)
                    {
                        float nextD = Vector3.Distance(result[i].position, skillTF.position);
                        if (nextD < minD)
                        {
                            minD = nextD;
                            temp[0] = result[i];
                        }
                    }
                    Debug.Log(temp[0].name);
                    return temp;
                }
                else 
                {
                    foreach (var item in result)
                    {
                        Debug.Log(item.name);
                    }
                    return result;
                }
            }
        }
    }
}

