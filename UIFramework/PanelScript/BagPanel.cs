using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPanel : PanelBase
{
    public override void OnEnter(object o = null)
    {
        base.OnEnter(o);
    }
    public void ClosePanel()
    {
        UIManager.instance.PopPanle();
    }
}
