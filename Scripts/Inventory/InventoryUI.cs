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

    private List<Item> InventoryItems;


    void Start()
    {
        open = false;
        InventoryItems = new List<Item>();
        Inventory = transform.Find("Inventory");
        ItemsParent = Inventory.Find("ItemGrid/ItemsParent");

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
        SlotItemIdentifier id = slot.GetComponent<SlotItemIdentifier>();
        if(id == null) Debug.LogError("id is not set");
        id.SetItem(item);
        GameObject iconObject = slot.transform.Find("ItemButton/ItemIcon").gameObject;

        Image iconImage = iconObject.GetComponent<Image>();
        iconImage.enabled = true;
        iconImage.sprite = item.itemIcon;

        InventoryItems.Add(item);
    }
}
