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
                eTypetxt = "�Կ�";
                break;
            case EquipmentType.Neck:
                eTypetxt = "����";
                break;
            case EquipmentType.Ring:
                eTypetxt = "ָķ";
                break;
            case EquipmentType.Armor:
                eTypetxt = "�ϰ���";
                break;
            case EquipmentType.Bracer:
                eTypetxt = "������";
                break;
            case EquipmentType.Boots:
                eTypetxt = "��";
                break;
            case EquipmentType.PhysicalWeapon:
                eTypetxt = "����";
                break;
            case EquipmentType.MagicWeapon:
                eTypetxt = "����";
                break;
        }
        string str = string.Format("{0}\nװ�����ͣ�{1}\n�˺�ֵ��{2}\n������  {3}   ������  {4}   \n���ݣ�  {5}   ������  {6}   ",
            Description, eTypetxt, AddValue, Strength, Intellect, Agility, Stamina);
        return str;
    }
}
public enum EquipmentType 
{
    Head,//ͷ��
    Neck,//����
    Ring,//��ָ
    Armor,//����
    Bracer,//����
    Boots,//Ь��
    PhysicalWeapon,//��������
    MagicWeapon,//ħ������
}

