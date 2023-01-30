using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MOBASkill;

public class SkillMessagePanel : PanelBase
{
    Image Icon;
    Text Name;
    Text Describe;
    public SkillData skill;
    private void Awake()
    {
        Icon = transform.Find("SkillIcon").GetComponent<Image>();
        Name = transform.Find("SkillName").GetComponent<Text>();
        Describe = transform.Find("SkillDescribe").GetComponent<Text>();
    }
    public override void OnEnter(object o = null)
    {
        base.OnEnter(o); 
        LoadSkillMessage(o);
    }
    public override void OnStay(object o = null) 
    {
        base.OnStay();
        LoadSkillMessage(o);
    }
    public void LoadSkillMessage(object o) 
    {
        skill = o as SkillData;
        Icon.sprite = skill.skillIcon;
        Name.text = skill.name;
        Describe.text = skill.description;
    }
}
