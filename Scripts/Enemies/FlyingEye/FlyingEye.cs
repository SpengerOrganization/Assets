using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    // Attributes
    public float Speed = 3;
    public float RealisationDistance = 20;
    public float AttackingDistance = 1;
    public float AttackingDistanceRange = 1;
    public float AttackSpeed = 1.5f;

    // Components
    private Animator animator;
    private Rigidbody2D rb;

    // Player
    private GameObject player;

    // Other
    private float NextAttack;
    private float LastXPosition;
    private int? LastDirection = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        NextAttack = Time.time + AttackSpeed;
        LastXPosition = transform.position.x;
    }


    void FixedUpdate()
    {
        float DistanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if(DistanceToPlayer <= RealisationDistance)
        {
            if(DistanceToPlayer >= AttackingDistance-AttackingDistanceRange && DistanceToPlayer <= AttackingDistance+AttackingDistanceRange)
            {
                Attack();
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
    }


    private void MoveTowardsPlayer()
    {
        if(LastXPosition > transform.position.x)    // moving left
        {
            if(LastDirection == 1||LastDirection==null)
            {
                transform.Rotate(0, 180, 0);
            }
            LastDirection = 2;
        }else if(LastXPosition < transform.position.x)  // moving right
        {
            if (LastDirection == 2)
            {
                transform.Rotate(0, 180, 0);
            }
            LastDirection = 1;
        }

        LastXPosition = transform.position.x;

        rb.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, Speed * Time.fixedDeltaTime));

        if (!AnimatorIsPlaying())
        {
            animator.Play("Flying");
        }
    }

    private void Attack()
    {
        if(NextAttack <= Time.time)
        {
            animator.Play("Attacking");
            NextAttack = Time.time + AttackSpeed;
        }
        else
        {
            if (!AnimatorIsPlaying())
            {
                animator.Play("Flying");
            }
        }
    }

    private bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }
}
