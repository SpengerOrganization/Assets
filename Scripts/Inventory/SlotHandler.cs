using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class SlotHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public Item item;
    private bool onSlot;
    private Image iconImage;
    private GameObject ItemIconObject;
    public bool curPicked;
    private Sprite icon;

    private GameObject canvas;

    private Transform mouseFollowTransform;

    public void SetItem(Item item){
        this.item = item;
    }

    void Start()
    {
        ItemIconObject = transform.Find("ItemButton/ItemIcon").gameObject;
        iconImage = ItemIconObject.GetComponent<Image>();

        icon = iconImage.sprite;
        mouseFollowTransform = null;
        canvas = GameObject.Find("Canvas");

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
        curPicked = false;
    }

    void Update()
    {
        
        if (onSlot && Input.GetButtonUp("Fire1"))
        {
            iconImage.sprite = null;
            iconImage.enabled = false;

            // make sprite follow mouse
            mouseFollowTransform = GenerateSpriteAtMouse(icon);

            // Aktualisiere die Position des Sprites auf die Position des Mauszeigers
            mouseFollowTransform.position = GetMousePosition();

            curPicked = true;
        }
        else if (Input.GetButtonUp("Fire1") && mouseFollowTransform != null)
        {
            // TODO: check if over Player view slot

            iconImage.enabled = true;
            iconImage.sprite = icon;

            if(mouseFollowTransform != null){
                Destroy(mouseFollowTransform.gameObject);
            }

            curPicked = false;
        }

        if (curPicked)
        {
            mouseFollowTransform.position = GetMousePosition();
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

    private Transform GenerateSpriteAtMouse(Sprite sprite)
    {
        // Erstelle ein neues GameObject und setze den SpriteRenderer
        GameObject newObject = new GameObject("ItemMouseFollow");
        SpriteRenderer spriteRenderer = newObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        // Hol die aktuelle Position des Mauszeigers in Weltkoordinaten
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Setze die Position des neuen GameObjects auf die Position des Mauszeigers
        newObject.transform.position = mousePosition;

        spriteRenderer.transform.localScale = new Vector3(2f, 2f, 1f);

        // TODO: Versuch, Item �ber Canvas anzuzeigen

        // Gib das Transform-Objekt des neu erstellten GameObjects zur�ck
        return newObject.transform;
    }

    private Vector3 GetMousePosition()
    {
        // Hol die aktuelle Position des Mauszeigers in Bildschirmkoordinaten
        Vector3 mousePosition = Input.mousePosition;

        // Setze die Z-Komponente auf die Entfernung der Kamera zur Szene
        mousePosition.z = Camera.main.nearClipPlane;

        // Wandele die Bildschirmkoordinaten in Weltkoordinaten um
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Setze die Z-Komponente auf 0, um sicherzustellen, dass das Sprite auf der Ebene liegt
        worldMousePosition.z = 0;

        return worldMousePosition;
    }
}
