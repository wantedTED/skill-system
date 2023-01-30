using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEquipped : Inventory
{
    PlayerCharacter player;
    void Start() 
    {
        player = GameObject.Find("Player").GetComponent<PlayerCharacter>();
        LoadEquipIcon();
    }
    public void LoadEquipIcon() 
    {
        for (int i = 0; i < player.EQUIP.Length - 1; i++) 
        {
            if (player.EQUIP[i] != null) 
            {
                PutSpecifiedGrid(i, player.EQUIP[i].item);
            }
        }
    }
}
