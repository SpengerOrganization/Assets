using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public float Damage;
    public float Health;

    void Start()
    {
        
    }


    void Update()
    {
        WatchIfDead(); 
    }

    public void GetDamage(float Damage)
    {
        Health -= Damage;
    }

    private void WatchIfDead()
    {
        if (Health <= 0)
        {
            Time.timeScale = 0;

            // some death animation 

            // Death gui
        }
    }
}
