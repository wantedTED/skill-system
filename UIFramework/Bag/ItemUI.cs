using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, ICanvasRaycastFilter
{
    public int count { get; set; }//持有的数量
    public Item item { get; set; }//物品类
    public bool isEquipped { get; set; }//装备类是否被装备上

    public Text text;
    public Image image;

    PlayerCharacter player;
    Transform lastPos;
    Vector2 fixedSize = new Vector2(70, 70);

    bool isRayThrough = true;
    public void SetItem(Item item,int count =1) 
    {
        this.item = item;
        this.count = count;
        this.isEquipped = false;
        image.sprite = Resources.Load<Sprite>("ItemPic/"+item.SpriteName);
        text.text = count.ToString();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCharacter>();
    }
    public void AddCount(int num=1) 
    {
        count += num;
        text.text = count.ToString();
    }
    public void ReduceCount() 
    {
        ItemType t = item.Type;
        switch (t)
        {
            case ItemType.Consumable:
                count -= 1;
                if (count == 0)
                {
                    Destroy(this.gameObject);
                }
                else 
                {
                    text.text = count.ToString();
                }
                break;
            case ItemType.Equipment:
                EquipmentUsed();
                break;
            case ItemType.Material:
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.instance.PushPanel(UIType.ItemMessage, item);
        UIManager.instance.PushPanel(UIType.ItemMessage, this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastPos = transform.parent;
        transform.position = Input.mousePosition;
        transform.parent = transform.parent.parent;
        isRayThrough = false;
        Debug.Log("背包物品开始拖拽");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject go = eventData.pointerCurrentRaycast.gameObject;
        if (go != null && go.layer == 9)  
        {
            if (go.CompareTag("ItemGrid"))
            {
                transform.SetParent(go.transform);
                transform.position = go.transform.position;
            }
            else if (go.CompareTag("ItemUI"))
            {
                transform.SetParent(go.transform.parent);
                transform.position = go.transform.parent.position;
                go.transform.SetParent(lastPos);
                go.transform.position = lastPos.transform.position;
            }
        }
        else 
        {
            transform.SetParent(lastPos);
            transform.position = lastPos.transform.position;
        }
        isRayThrough = true;
        transform.GetComponent<RectTransform>().sizeDelta = fixedSize;
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return isRayThrough;
    }
    public void UseThisItem() 
    {
        ItemType type = item.Type;
        switch (type) 
        {
            case ItemType.Consumable:
                Consumable c = item as Consumable;
                player.Hp += c.Hp;
                player.Mp += c.Mp;
                ReduceCount();
                break;
            case ItemType.Equipment:
                Equipment e = item as Equipment;
                EquipmentType t = e.EquipType;
                CalculateEquipmentBonuses(e);
                ReduceCount();
                player.InstallEquipment(this);
                switch (t)
                {
                    case EquipmentType.Head:
                        player.MaxHp += e.AddValue;
                        break;
                    case EquipmentType.Neck:
                        player.MaxMp += e.AddValue;
                        break;
                    case EquipmentType.Ring:
                        player.Crit += e.AddValue / 100;
                        break;
                    case EquipmentType.Armor:
                        player.Crit += e.AddValue / 100;
                        break;
                    case EquipmentType.Bracer:
                        player.MaxHp += e.AddValue;
                        break;
                    case EquipmentType.Boots:
                        player.Speed += e.AddValue;
                        break;
                    case EquipmentType.PhysicalWeapon:
                        player.PhysicalDamge += e.AddValue;
                        break;
                    case EquipmentType.MagicWeapon:
                        player.MagicDamge += e.AddValue;
                        break;
                }
                break;
            case ItemType.Material:
                break;
        }
    }
    public void CalculateEquipmentBonuses(Equipment e) 
    {
        player.Strength += e.Strength;
        player.Intellect += e.Intellect;
        player.Agility += e.Agility;
        player.Stamina += e.Stamina;
        player.PhysicalDamge = NumericalManager.instance.RealDamage(player.Strength, player.PhysicalDamge);
        player.MagicDamge = NumericalManager.instance.RealDamage(player.Intellect, player.MagicDamge);
        player.MaxHp = NumericalManager.instance.RealMaxHPorMP(player.Stamina, player.MaxHp);
        player.MaxMp = NumericalManager.instance.RealMaxHPorMP(player.Intellect, player.MaxMp);
        player.Crit = NumericalManager.instance.RealCrit(player.Agility, player.Crit);
        player.Defense = NumericalManager.instance.RealDefense(player.Stamina, player.Defense);
    }
    public void EquipmentUsed() 
    {
        if (isEquipped)
        {
            image.color = new Color(255, 255, 255);
            image.raycastTarget = true;
            isEquipped = false;
        }
        else 
        {
            image.color = new Color(0.5f, 0.5f, 0.5f);
            image.raycastTarget = false;
            isEquipped = true;
        }

    }
}
