using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ŒÔ∆∑ø’∏Ò
/// </summary>
public class Slot : Inventory
{
    public GameObject ItemPrefab;
    public void LoadItem(Item item) 
    {
        if (transform.childCount == 0)
        {
            GameObject itemPre = GameObject.Instantiate(ItemPrefab);
            itemPre.transform.SetParent(transform);
            itemPre.transform.localPosition = Vector3.zero;
            transform.GetChild(0).GetComponent<ItemUI>().SetItem(item);
        }
        else 
        {
            transform.GetChild(0).GetComponent<ItemUI>().AddCount();
        }
    }
    public ItemType GetItemType() 
    {
        return transform.GetChild(0).GetComponent<ItemUI>().item.Type;
    }
    public int GetItemID() 
    {
        return transform.GetChild(0).GetComponent<ItemUI>().item.ID;
    }
    public bool IsFilled() 
    {
        ItemUI iu = transform.GetChild(0).GetComponent<ItemUI>();
        return iu.count >= iu.item.Numerical;
    }
}
