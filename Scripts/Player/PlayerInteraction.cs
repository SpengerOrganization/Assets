using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    void Start()
    { }


    void Update()
    { }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.transform.name);
        if (collision.gameObject.name.ToLower().Contains("chest"))
        {
            Debug.Log("Spieler kollidiert mit einer Kiste!");
            // Hier kannst du Aktionen für die Kollisionsbehandlung durchführen
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.ToLower().Contains("chest"))
        {
            Debug.Log("Spieler hat die Kiste verlassen!");
            // Hier kannst du Aktionen für die Kollisionsbehandlung durchführen
        }
    }
}
