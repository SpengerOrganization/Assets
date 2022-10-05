using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    // Attributes
    public float MovingSpeed = 3;
    public float Health = 10;
    public float Damage = 1;

    public float RealisationDistance = 20;
    public float AttackingDistance = 1;
    public float AttackingDistanceRange = 1;
    public float AttackSpeed = 1.5f;
    public float AttackRange = 0.5f;

    // Components
    private Animator animator;
    private Rigidbody2D rb;
    public Transform AttackPoint;
    public LayerMask PlayerLayer;

    // Player
    private GameObject player;
    private PlayerStats PlayerStats;

    // Other
    private float NextAttack;
    private float LastXPosition;
    private int? LastDirection = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        PlayerStats = player.GetComponent<PlayerStats>();
        NextAttack = Time.time;
        LastXPosition = transform.position.x;
    }


    void Update(){
        if(IsDead()){
            animator.Play("Death");
            Destroy(GetComponent<Collider2D>());
            Destroy(this);
            BroadcastMessage("StartTimer");
        }
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

    private bool IsDead(){
        return Health<=0;
    }

    public void ApplyDamage(float Damage){
        Health-=Damage;
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

        rb.MovePosition(Vector2.MoveTowards(transform.position, player.transform.position, MovingSpeed * Time.fixedDeltaTime));

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
            if (!AnimatorIsPlaying() && CurrentAnimationName().Equals("Attacking"))
            {
                // attack done

                // make damage
                Collider2D[] hit = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, PlayerLayer);

                foreach(Collider2D c in hit)
                {
                    if (c.name.Equals("Player"))
                    {
                        // make damage to player
                        PlayerStats.GetDamage(Damage);
                    }
                }

                // continue flying
                animator.Play("Flying");
            }
        }
    }

    private string CurrentAnimationName(){
        return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    }

    private bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1;
    }

    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
