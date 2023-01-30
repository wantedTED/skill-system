using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMessagePanel : PanelBase
{
    public Image ItemImage;
    public Text ItemName;
    public Text ItemInformation;
    public Text UsingTxt;
    public Button UsingButton;

    ItemUI currentItemUI;
    public override void OnEnter(object o = null)
    {
        base.OnEnter(o);
        if (o is Item)
        {
            LoadItemData(o);
        }
        else if (o is ItemUI) 
        {
            GetCurrentItemUI(o);
        }
    }
    public override void OnStay(object o = null) 
    {
        base.OnStay();
        if (o is Item)
        {
            LoadItemData(o);
        }
        else if (o is ItemUI)
        {
            GetCurrentItemUI(o);
        }
    }
    public override void OnResume()
    {
        base.OnResume();
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public void LoadItemData(object o) 
    {
        Item i = o as Item;
        ItemType type = i.Type;
        switch (type) 
        {
            case ItemType.Consumable:
                Consumable c = o as Consumable;
                ItemInformation.text = c.GetItemInformation();
                UsingTxt.text = "使用";
                break;
            case ItemType.Equipment:
                Equipment e = o as Equipment;
                ItemInformation.text = e.GetItemInformation();
                UsingTxt.text = "装备";
                break;
            case ItemType.Material:
                break;
        }
        Sprite s = Resources.Load<Sprite>("ItemPic/" + i.SpriteName);
        ItemImage.sprite = s;
        ItemName.text = i.Name;
        ItemName.color = i.GetItemNameColor();
    }
    public void GetCurrentItemUI(object o) 
    {
        ItemUI u = o as ItemUI;
        currentItemUI = u;
        UsingButton.onClick.AddListener(delegate () { currentItemUI.UseThisItem(); });
    }
}
