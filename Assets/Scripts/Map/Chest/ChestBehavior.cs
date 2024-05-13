using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehavior : MonoBehaviour
{

    public Item Item;
    public GameObject ItemPrefab;

    public bool interactive;
    public bool open;

    // Start is called before the first frame update
    void Start()
    {
        interactive = false;
        open = false;

        //Item = new Sword(0, "Sword", new Sprite(""), 2, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if(interactive && Input.GetKeyUp("e") && !open)
        {
            GameObject item = Instantiate(ItemPrefab);
            item.GetComponent<ItemPickup>().SetItem(Item);

            open = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactive = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactive = false;
        }
    }
}
