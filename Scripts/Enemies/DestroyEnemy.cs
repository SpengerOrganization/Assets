using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    public float TimeExisting;

    public void StartTimer(){
        Destroy(gameObject, TimeExisting);
    }
}
