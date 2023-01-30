using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : PanelBase
{
    public List<Button> buttonList;
    public override void OnEnter(object o = null)
    {
        base.OnEnter(o);
        foreach (var b in buttonList)
        {
            b.enabled = true;
        }
    }
    public override void OnStay(object o = null)
    {
        base.OnStay();
        foreach (var b in buttonList)
        {
            b.enabled = false;
        }
    }
    public override void OnResume()
    {
        base.OnResume();
        foreach (var b in buttonList)
        {
            b.enabled = true;
        }
    }
    public override void OnExit()
    {

    }
    public void OnPushPanel(string type) 
    {
        UIType t = (UIType)System.Enum.Parse(typeof(UIType), type);
        UIManager.instance.PushPanel(t);
    }
}
