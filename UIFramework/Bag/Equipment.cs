using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item
{
    public int AddValue { get; set; }
    public int Strength { get; set; }
    public int Intellect { get; set; }
    public int Agility { get; set; }
    public int Stamina { get; set; }
    public EquipmentType EquipType { get; set; }
    public Equipment
        (int id, string name, string description, ItemType type, ItemQuality quality, int num, int buyprice, int sellprice,
        string spritename, int addValue, int strength, int intellect, int agility, int stamina, EquipmentType equiptype)
        : base(id, name, description,type,quality,num,buyprice,sellprice,spritename) 
    {
        this.AddValue = addValue;
        this.Strength = strength;
        this.Intellect = intellect;
        this.Agility = agility;
        this.Stamina = stamina;
        this.EquipType = equiptype;
    }
    public override string GetItemInformation()
    {
        string eTypetxt = " ";
        switch (EquipType)
        {
            case EquipmentType.Head:
                eTypetxt = "脑壳";
                break;
            case EquipmentType.Neck:
                eTypetxt = "颈子";
                break;
            case EquipmentType.Ring:
                eTypetxt = "指姆";
                break;
            case EquipmentType.Armor:
                eTypetxt = "上半身";
                break;
            case EquipmentType.Bracer:
                eTypetxt = "手腕腕";
                break;
            case EquipmentType.Boots:
                eTypetxt = "脚";
                break;
            case EquipmentType.PhysicalWeapon:
                eTypetxt = "武器";
                break;
            case EquipmentType.MagicWeapon:
                eTypetxt = "武器";
                break;
        }
        string str = string.Format("{0}\n装备类型：{1}\n伤害值：{2}\n力量：  {3}   智力：  {4}   \n敏捷：  {5}   耐力：  {6}   ",
            Description, eTypetxt, AddValue, Strength, Intellect, Agility, Stamina);
        return str;
    }
}
public enum EquipmentType 
{
    Head,//头盔
    Neck,//项链
    Ring,//戒指
    Armor,//盔甲
    Bracer,//护腕
    Boots,//鞋子
    PhysicalWeapon,//物理武器
    MagicWeapon,//魔法武器
}

