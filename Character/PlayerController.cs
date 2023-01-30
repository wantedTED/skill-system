using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MOBASkill;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    CharacterSkillSystem skillSystem;
    PlayerCharacter player;
    NavMeshAgent nma;

    public bool canInput;
    public bool isSkillChooseTime;

    int currentSkillnum;
    void Awake()
    {
        skillSystem = GetComponent<CharacterSkillSystem>();
        player = GetComponent<PlayerCharacter>();
        nma = GetComponent<NavMeshAgent>();

        canInput = true;
        isSkillChooseTime = false;

        currentSkillnum = -1;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && canInput)   
        {
            player.GetTargetByAStar();
        }
        if (!isSkillChooseTime) 
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                currentSkillnum = GameManager.instance.skillButton[0];
                if (!skillSystem.OpenSkillIndicator(currentSkillnum))
                {
                    ReleaseSkill(currentSkillnum);
                }
                else
                {
                    isSkillChooseTime = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                currentSkillnum = GameManager.instance.skillButton[1];
                if (!skillSystem.OpenSkillIndicator(currentSkillnum))
                {
                    ReleaseSkill(currentSkillnum);
                }
                else
                {
                    isSkillChooseTime = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                currentSkillnum = GameManager.instance.skillButton[2];
                if (!skillSystem.OpenSkillIndicator(currentSkillnum))
                {
                    ReleaseSkill(currentSkillnum);
                }
                else
                {
                    isSkillChooseTime = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                currentSkillnum = GameManager.instance.skillButton[3];
                if (!skillSystem.OpenSkillIndicator(currentSkillnum))
                {
                    ReleaseSkill(currentSkillnum);
                }
                else
                {
                    isSkillChooseTime = true;
                }
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                currentSkillnum = GameManager.instance.skillButton[4];
                if (!skillSystem.OpenSkillIndicator(currentSkillnum))
                {
                    ReleaseSkill(currentSkillnum);
                }
                else
                {
                    isSkillChooseTime = true;
                }
            }
        }
        if (isSkillChooseTime && Input.GetMouseButtonDown(0))  
        {
            ReleaseSkill(currentSkillnum);
        }
        if (isSkillChooseTime && Input.GetMouseButtonDown(1)) 
        {
            skillSystem.CloseSkillIndicator();
            player.GetTargetByAStar();
            isSkillChooseTime = false;
        }
    }
    public void ExitTheSkill() //¶¯»­Ö¡ÊÂ¼þ
    {
        canInput = true;
    }
    public void ReleaseSkill(int id) 
    {
        skillSystem.AttackUseSkill(id);
        skillSystem.CloseSkillIndicator();
        isSkillChooseTime = false;
        if (id == 1 || id == 3)
        {
            canInput = false;
        }
    }
}