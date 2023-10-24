using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class MeleeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    private string IsOverColorHex;
    private string IsExitColorHex;

    private float opacity = 0.6f;

    private Image slotImage;
    private Image itemIcon;

    private bool overSlot;

    private InventoryUI inventory;

    void Start()
    {
        IsOverColorHex = "#292929";
        IsExitColorHex = "#141414";
        
        slotImage = GetComponent<Image>();
        itemIcon = transform.Find("MeleeIcon").GetComponent<Image>();

        slotImage.color = GetColor(IsExitColorHex, opacity);

        inventory = GameObject.Find("Canvas").GetComponent<InventoryUI>();

        if (slotImage.GetComponent<EventTrigger>() == null)
        {
            slotImage.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger trigger = slotImage.GetComponent<EventTrigger>();

        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });
        trigger.triggers.Add(entryExit);

        overSlot = false;
        inventory.EquipedMelee = null;
    }

    void Update(){
        if(overSlot && Input.GetKeyUp(KeyCode.Mouse0) && inventory.GetCurPickedItem() != null && inventory.EquipedMelee == null){
            EquipItem();
        }else if(overSlot && Input.GetKeyUp(KeyCode.Mouse0) && inventory.EquipedMelee != null){
            RemoveEquiption();
        }
    }

    private void EquipItem(){
        Item item = inventory.GetCurPickedItem();

        // check if item is a sword (or maybe other melee type in the future)
        if(item.GetType() != typeof(Sword)) return;

        itemIcon.enabled = true;
        itemIcon.sprite = item.itemIcon;

        // remove drag object
        Destroy(GameObject.Find("ItemMouseFollow"));

        // remove item slot
        inventory.RemoveItem(item);

        // set equiped melee
        inventory.EquipedMelee = item;

        inventory.EquipedMelee = item;
    }

    private void RemoveEquiption(){
        // remove from melee slot and create item slot in inventory
        itemIcon.enabled = false;
        itemIcon.sprite = null;

        inventory.AddItem(inventory.EquipedMelee);

        inventory.EquipedMelee = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        slotImage.color = GetColor(IsOverColorHex, opacity);
        overSlot = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        slotImage.color = GetColor(IsExitColorHex, opacity);
        overSlot = false;
    }

    private Color GetColor(string hex, float opacity){
        Color color = default!;
        ColorUtility.TryParseHtmlString(IsOverColorHex, out color);
        color.a = opacity;
        return color;
    }
}
