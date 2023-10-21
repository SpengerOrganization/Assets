using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class SlotItemIdentifier : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public Item item;
    private bool onSlot;

    public void SetItem(Item item){
        this.item = item;
    }

    void Start()
    { 
        Image iconImage = transform.Find("ItemButton/ItemIcon").gameObject.GetComponent<Image>();

        if (iconImage.GetComponent<EventTrigger>() == null)
        {
            iconImage.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger trigger = iconImage.GetComponent<EventTrigger>();

        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });
        trigger.triggers.Add(entryExit);

        onSlot = false;
    }

    void Update()
    { 
        if(onSlot && Input.GetButtonUp("Fire1")){
            // remove sprite from slot, make it follow the mouse until clicked again
            Debug.Log("Clicked on "+item.itemName);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onSlot = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onSlot = false;
    }
}
