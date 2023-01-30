using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAterial : Item
{
    public MAterial
         (int id, string name, string description, ItemType type, ItemQuality quality, int num, int buyprice, int sellprice, string spritename)
         : base(id, name, description, type, quality, num, buyprice, sellprice, spritename)
    {
    }
}
