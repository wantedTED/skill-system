using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 物品基类
/// </summary>
public class Item
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ItemType Type { get; set; }
    public ItemQuality Quality { get; set; }
    public int Numerical { get; set; }
    public int BuyPrice { get; set; }
    public int SellPrice { get; set; }
    public string SpriteName { get; set; }
    public Item() 
    {

    }
    public Item(int id, string name, string description, ItemType type, ItemQuality quality, int num, int buyprice, int sellprice, string spritename) 
    {
        this.ID = id;
        this.Name = name;
        this.Description = description;
        this.Type = type;
        this.Quality = quality;
        this.Numerical = num;
        this.BuyPrice = buyprice;
        this.SellPrice = sellprice;
        this.SpriteName = spritename;
    }
    public Color GetItemNameColor() 
    {
        Color c = Color.black;
        switch (Quality)
        {
            case ItemQuality.White:
                c = Color.white;
                break;
            case ItemQuality.Green:
                c = Color.green;
                break;
            case ItemQuality.Blue:
                c = Color.blue;
                break;
            case ItemQuality.Pink:
                c = Color.red;
                break;
            case ItemQuality.Orange:
                c = Color.yellow;
                break;
        }
        return c;
    }
    public virtual string GetItemInformation() 
    {
        string str = " ";
        Debug.Log("物品基类中的显示方法");
        return str;
    }
}

public enum ItemType 
{
    Consumable,
    Equipment,
    Material
}
public enum ItemQuality 
{
    White,
    Green,
    Blue,
    Pink,
    Orange
}
