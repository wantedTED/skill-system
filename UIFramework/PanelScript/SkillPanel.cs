using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using MOBASkill;

public class SkillPanel : PanelBase
{
    SkillData[] playerSkills;
    GameObject skillIcon;

    public List<Transform> skillGirds;
    public Text text;
    public PlayerCharacter player;

    private void Awake()
    {
        skillIcon = Resources.Load("UIIcon/SkillIcon") as GameObject;
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        playerSkills = GameObject.Find("Player").GetComponent<CharacterSkillManager>().Skills;
        LoadSkillIcon();
    }
    public override void OnEnter(object o = null)
    {
        base.OnEnter(o);
        text.text = player.SkillPoint.ToString();
    }
    public void ClosePanel() 
    {
        UIManager.instance.PopPanle();
    }
    public void LoadSkillIcon() 
    {
        foreach (SkillData skill in playerSkills)
        {
            GameObject go = Instantiate(skillIcon);
            Sprite icon = skill.skillIcon;
            go.GetComponent<Image>().sprite = icon;
            go.GetComponentInChildren<Text>().text = skill.level.ToString();
            go.GetComponent<SkillIconGrid>().skill = skill;
            for (int i = 0; i < skillGirds.Count - 1; i++)
            {
                if (skillGirds[i].GetComponentsInChildren<Transform>().Length <= 1)
                {
                    go.transform.SetParent(skillGirds[i],false);
                }
            }
        }
    }

}
