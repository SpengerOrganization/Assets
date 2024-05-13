using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject InventorySlotPrefab;
    private Transform ItemsParent;
    private Transform Inventory;
    private bool open;

    public List<GameObject> InventoryItems;

    // Equiped Items
    public Item EquipedMelee;
    public Item EquipedBow;
    public Item EquipedShield;

    void Start()
    {
        open = false;
        InventoryItems = new List<GameObject>();
        Inventory = transform.Find("Inventory");
        ItemsParent = Inventory.Find("ItemGrid/ItemsParent");
        EquipedShield = null;
        EquipedBow = null;
        EquipedMelee = null;

        if(Inventory == null){
            Debug.Log("Can't find Inventory GameObject!");
        }else if(ItemsParent == null) {
            Debug.Log("Can't find ItemsParent GameObject!");
        }
    }


    void Update()
    {
        if(Input.GetKeyDown("r")){
            if(open){
                // check if mouse follow object exists and delete it
                GameObject itemFollow = GameObject.Find("ItemMouseFollow");
                if(itemFollow != null){
                    Destroy(itemFollow);
                } 

                // reset all slots
                foreach (GameObject slot in InventoryItems){
                    SlotHandler id = slot.GetComponent<SlotHandler>();
                    GameObject iconObject = slot.transform.Find("ItemButton/ItemIcon").gameObject;
                    Image iconImage = iconObject.GetComponent<Image>();
                    iconImage.enabled = true;
                    iconImage.sprite = id.item.itemIcon;
                    id.curPicked = false;
                }

                Inventory.gameObject.SetActive(false);
                open = false;
            }else{
                Inventory.gameObject.SetActive(true);
                open = true;
            }
        }
    }

    public void AddItem(Item item){
        // has to be instantiated with the correct sprite and a defined factor
        GameObject slot = Instantiate(InventorySlotPrefab, ItemsParent);
        if(slot == null) Debug.LogError("slot is not set");
        SlotHandler id = slot.GetComponent<SlotHandler>();
        if(id == null) Debug.LogError("id is not set");
        id.SetItem(item);
        GameObject buttonObject = slot.transform.Find("ItemButton").gameObject;
        GameObject iconObject = buttonObject.transform.Find("ItemIcon").gameObject;

        Image iconImage = iconObject.GetComponent<Image>();
        iconImage.enabled = true;
        iconImage.sprite = id.item.itemIcon;

        InventoryItems.Add(slot);
    }

    public void RemoveItem(Item item){
        foreach(GameObject slot in InventoryItems){
            Item itemFromSlot = slot.GetComponent<SlotHandler>().item;
            if(itemFromSlot == item){
                Destroy(slot);
                InventoryItems.Remove(slot);
                return;
            }
        }
    }

    public bool ContainsItemType(Item item){
        foreach(GameObject slot in InventoryItems){
            SlotHandler id = slot.GetComponent<SlotHandler>();
            if(id.item.GetType() == item.GetType()){
                return true;
            }
        }
        return false;
    }

    public Item GetCurPickedItem(){
        foreach(GameObject item in InventoryItems){
            SlotHandler handler = item.GetComponent<SlotHandler>();
            if(handler.curPicked){
                return handler.item;
            }
        }
        return null;
    }
}
