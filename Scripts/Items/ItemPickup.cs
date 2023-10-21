using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public Sprite icon;
    private Item item;
    private SpriteRenderer spriteRenderer;

    void Start()
    { 
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = icon;
        // somehow generate the item for the specific gameobject-item ... random?
        item = new Sword(1, "Olaf", icon, 2, 5);
    }


    void Update()
    { }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player picked up Item! ("+item.itemName+")");

            GameObject Canvas = GameObject.Find("Canvas");
            if(Canvas == null){
                Debug.Log("Can't find Canvas!");
            }

            // add Item via InventoryUI Class
            Canvas.GetComponent<InventoryUI>().AddItem(item);

            Destroy(gameObject);
        }
    }
}
