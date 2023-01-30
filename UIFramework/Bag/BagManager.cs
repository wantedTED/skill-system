using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;
public class BagManager : MonoBehaviour 
{
    public static object _object = new object();
    public static BagManager _instance;
    public static BagManager instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_object)
                {
                    if (_instance == null)
                    {
                        _instance = new BagManager();
                    }
                }
            }
            return _instance;
        }
    }
    public List<Item> Items = new List<Item>();
    public BagManager()
    {
        LoadItemsJSON();
    }
    public Item GetItemByID(int id) 
    {
        Item item = new Item();
        foreach (var i in Items) 
        {
            if (i.ID == id)
            {
                item = i;
            }
        }
        return item;
    }
    private void LoadItemsJSON() 
    {
        TextAsset ta = Resources.Load<TextAsset>("Items");
        JSONObject jo = new JSONObject(ta.text);
        foreach (JSONObject j in jo.list)
        {
            string itemType = j["Type"].str;
            ItemType type = (ItemType)Enum.Parse(typeof(ItemType), itemType);
            int id = (int)j["ID"].n;
            string name = j["Name"].str;
            string itemQuality = j["Quality"].str;
            ItemQuality quality = (ItemQuality)Enum.Parse(typeof(ItemQuality), itemQuality);
            string description = j["Description"].str;
            int num = (int)j["Num"].n;
            int buyPrice = (int)(j["BuyPrice"].n);
            int sellPrice = (int)(j["SellPrice"].n);
            string spriteName = j["SpriteName"].str;

            Item item = null;
            switch (type)
            {
                case ItemType.Consumable:
                    int hp = (int)j["Hp"].n;
                    int mp = (int)j["Mp"].n;
                    item = new Consumable(id, name, description, type, quality, num, buyPrice, sellPrice, spriteName, hp, mp);
                    break;
                case ItemType.Equipment:
                    int addValue = (int)j["AddValue"].n;
                    int strength = (int)j["Strength"].n;
                    int intellect = (int)j["Intellect"].n;
                    int agility = (int)j["Agility"].n;
                    int stamina = (int)j["Stamina"].n;
                    string itemEquip = j["EquipType"].str;
                    EquipmentType eType = (EquipmentType)Enum.Parse(typeof(EquipmentType), itemEquip);
                    item = new Equipment(id, name, description, type, quality, num, buyPrice, sellPrice, spriteName,
                        addValue, strength, intellect, agility, stamina, eType);
                    break;
                case ItemType.Material:
                    item = new MAterial(id, name, description, type, quality, num, buyPrice, sellPrice, spriteName);
                    break;
            }
            Items.Add(item);
            Debug.Log("JSON加载的时候的Item" + item.Name);
        }
    }
}
