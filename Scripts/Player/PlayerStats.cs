using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public float Damage;
    public float Health;

    public Transform AttackPoint;
    public float AttackRange;
    public LayerMask enemyLayer;

    void Start()
    {
        
    }

    void Update()
    {
        WatchIfDead(); 

        if(Input.GetKeyDown(KeyCode.Mouse0)){
            Attack();
        }
    }

    private void Attack(){
        Collider2D[] hit = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayer);
        foreach(Collider2D c in hit)
        {
            DealDamage(c, Damage);
        }
    }

    public void GetDamage(float Damage)
    {
        Health -= Damage;
    }

    private void DealDamage(Collider2D HitEnemyCol, float Damage){
        GameObject HitEnemy = HitEnemyCol.gameObject;
        HitEnemy.BroadcastMessage("ApplyDamage", Damage);
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

    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
