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

        item = new Sword(0, "", icon, 0, 0);
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

    public void SetItem(Item item){
        this.item = item;
    }
}
