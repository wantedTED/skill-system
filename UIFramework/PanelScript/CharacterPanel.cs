using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : PanelBase 
{
    public Text statText;
    public Text[] texts;
    public PlayerCharacter player;

    public int currentStat;
    public override void OnEnter(object o = null)
    {
        base.OnEnter(o);
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        currentStat = player.Stat;
        TextShow();
    }
    public override void OnStay(object o = null)
    {
        base.OnStay(o);
    }
    public void ClosePanel() 
    {
        UIManager.instance.PopPanle();
    }
    public void AddPoints(int index) 
    {
        if (player.Stat == 0) 
        {
            return;
        }
        player.Stat -= 1;

        switch (index) 
        {
            case 0:
                player.Strength += 1;
                player.PhysicalDamge = NumericalManager.instance.RealDamage(player.Strength, 10);
                break;
            case 1:
                player.Intellect += 1;
                player.MaxMp = NumericalManager.instance.RealMaxHPorMP(player.Intellect, 100);
                break;
            case 2:
                player.Agility += 1;
                player.Crit = NumericalManager.instance.RealCrit(player.Agility, 0.05f);
                break;
            case 3:
                player.Stamina += 1;
                player.MaxHp = NumericalManager.instance.RealMaxHPorMP(player.Stamina, 100);
                player.Defense = NumericalManager.instance.RealDefense(player.Stamina, 0.1f);
                break;
        }
        TextShow();
    }
    public void ReducePoints(int index) 
    {
        if (player.Stat == currentStat)
        {
            return;
        }
        player.Stat += 1;
        switch (index)
        {
            case 0:
                if (player.Strength == 0) 
                {
                    return;
                }
                player.Strength -= 1;
                player.PhysicalDamge = NumericalManager.instance.RealDamage(player.Strength, player.PhysicalDamge);
                break;
            case 1:
                if (player.Intellect == 0)
                {
                    return;
                }
                player.Intellect -= 1;
                player.MaxMp = NumericalManager.instance.RealMaxHPorMP(player.Intellect, player.MaxMp);
                break;
            case 2:
                if (player.Agility == 0)
                {
                    return;
                }
                player.Agility -= 1;
                player.Crit = NumericalManager.instance.RealCrit(player.Agility, player.Crit);
                break;
            case 3:
                if (player.Stamina == 0) 
                {
                    return;
                }
                player.Stamina -= 1;
                player.MaxHp = NumericalManager.instance.RealMaxHPorMP(player.Stamina, player.MaxHp);
                player.Defense = NumericalManager.instance.RealDefense(player.Stamina, player.Defense);
                break;
        }
        TextShow();
    }
    public void TextShow() 
    {
        texts[0].text = "力量：    " + player.Strength.ToString();
        texts[1].text = "智力：    " + player.Intellect.ToString();
        texts[2].text = "敏捷：    " + player.Agility.ToString();
        texts[3].text = "耐力：    " + player.Stamina.ToString();
        statText.text = player.Stat.ToString();
    }
}
