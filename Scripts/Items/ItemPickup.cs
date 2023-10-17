using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public bool pickUp;
    

    void Start()
    { pickUp = false; }


    void Update()
    { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit Item!");
            pickUp = true;
            // Zum Beispiel: other.GetComponent<Inventory>().AddItem(gameObject);
        }
    }
}
