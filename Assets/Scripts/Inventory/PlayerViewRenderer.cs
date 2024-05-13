using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class PlayerViewRenderer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    private string IsOverColorHex;
    private string IsExitColorHex;

    private float opacity = 0.6f;

    private Image image;

    void Start()
    {
        IsOverColorHex = "#292929";
        IsExitColorHex = "#141414";
        
        image = GetComponent<Image>();

        image.color = GetColor(IsExitColorHex, opacity);

        if (image.GetComponent<EventTrigger>() == null)
        {
            image.gameObject.AddComponent<EventTrigger>();
        }

        EventTrigger trigger = image.GetComponent<EventTrigger>();

        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });
        trigger.triggers.Add(entryExit);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = GetColor(IsOverColorHex, opacity);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = GetColor(IsExitColorHex, opacity);
    }

    private Color GetColor(string hex, float opacity){
        Color color = default!;
        ColorUtility.TryParseHtmlString(IsOverColorHex, out color);
        color.a = opacity;
        return color;
    }
}
