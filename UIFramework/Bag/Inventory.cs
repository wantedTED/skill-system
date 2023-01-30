using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 存储基类
/// </summary>
public class Inventory : MonoBehaviour
{
    protected Slot[] slots;
    private void Awake()
    {
        slots = GetComponentsInChildren<Slot>();
    }
    public bool PutItem(int id) 
    {
        Item item = BagManager.instance.GetItemByID(id);
        Debug.Log(item.ID+"--------"+item.Name);
        return PutItem(item);
    }
    public bool PutItem(Item item) 
    {
        if (item == null) 
        {
            return false;
        }
        if (item.Numerical == 1) 
        {
            Slot slot = FindEmptySlot();
            if (slot == null)
            {
                Debug.Log("没有空的物品格子");
                return false;
            }
            else 
            {
                slot.LoadItem(item);
            }
        }
        else 
        {
            Slot slot = FindSameItemSlot(item);
            if (slot != null)
            {
                slot.LoadItem(item);
            }
            else 
            {
                Slot slot1 = FindEmptySlot();
                if (slot1 != null)
                {
                    slot1.LoadItem(item);
                }
                else 
                {
                    Debug.Log("没有空的格子");
                    return false;
                }
            }
        }
        return true;
    }
    public void PutSpecifiedGrid(int index,Item item) 
    {
        Debug.Log(slots);
        if (index >= slots.Length)
        {
            return;
        }
        if (slots[index].transform.childCount >= 1)
        {
            GameObject go = slots[index].GetComponentInChildren<Transform>().gameObject;
            Destroy(go);
        }
        slots[index].LoadItem(item);
    }
    private Slot FindEmptySlot() 
    {
        foreach (var s in slots)
        {
            if (s.transform.childCount == 0) 
            {
                return s;
            }
        }
        return null;
    }
    private Slot FindSameItemSlot(Item item) 
    {
        foreach (var s in slots)
        {
            if (s.transform.childCount >= 1 && s.GetItemID() == item.ID && s.IsFilled() == false) //差两个条件是否同一个物品和一个格子满没满
            {
                return s;
            }
        }
        return null;
    }
}
