using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int itemID;
    public string itemName;
    public Sprite itemIcon;

    public Item(int itemID, string itemName, Sprite itemIcon){
        this.itemID = itemID;
        this.itemName = itemName;
        this.itemIcon = itemIcon;
    }
}
