using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public int Hp { get; set; }
    public int Mp { get; set; }
    public Consumable
        (int id, string name, string description, ItemType type, ItemQuality quality, int num, int buyprice, int sellprice, string spritename, int hp, int mp)
        : base(id, name, description, type, quality, num, buyprice, sellprice, spritename) 
    {
        this.Hp = hp;
        this.Mp = mp;
    }
    public override string GetItemInformation()
    {
        string str = string.Format("{0}", Description);
        return str;
    }
}
