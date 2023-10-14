using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject Inventory;
    private bool open;


    void Start()
    {
        open = false;
    }


    void Update()
    {
        if(Input.GetKeyDown("r")){
            if(open){
                Inventory.SetActive(false);
                open = false;
            }else{
                Inventory.SetActive(true);
                open = true;
            }
        }
    }
}
