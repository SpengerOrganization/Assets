using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public int itemID;
    public string itemName;
    public Sprite itemIcon;


    public Item(int id, string name, Sprite icon)
    {
        itemName = name;
        itemID = id;
        itemIcon = icon;
    }
}
