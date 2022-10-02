using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    // Attributes
    public float Speed = 3;

    // Components
    private Animator animator;
    private Rigidbody2D rb;

    // Player
    private GameObject player;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }


    void Update()
    {
        rb.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, Speed*Time.fixedDeltaTime));
        if(transform.position==player.transform.position){
                animator.Play("Death");
        }
    }
}
