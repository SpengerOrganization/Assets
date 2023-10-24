using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{

    public float Damage;
    public float Health;

    public Transform AttackPoint;
    public Transform AttackAnim;
    public SpriteRenderer AttackAnimRenderer;
    public float AttackRange;
    public LayerMask enemyLayer;

    private Renderer SpriteRenderer;

    private InventoryUI inventory;

    void Start()
    {
        SpriteRenderer = GetComponent<Renderer>();
        AttackAnimRenderer = AttackAnim.GetComponent<SpriteRenderer>();
        inventory = GameObject.Find("Canvas").GetComponent<InventoryUI>();
    }

    void Update()
    {
        WatchIfDead(); 

        if(Input.GetKeyDown(KeyCode.Mouse0) && inventory.EquipedMelee != null){
            Attack();
        }
    }

    private void Attack(){
        AttackAnimRenderer.color = Color.white;
        StartCoroutine(GetAttackTimer());
        Collider2D[] hit = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayer);
        foreach(Collider2D c in hit)
        {
            if(c.gameObject.tag.Equals("Enemy")) DealDamage(c, Damage);
        }
    }

    public void GetDamage(float Damage)
    {
        SpriteRenderer.material.color = Color.red;
        Health -= Damage;
        StartCoroutine(GetDamageTimer());
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

            SceneManager.LoadScene("DeathScene");
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

    IEnumerator GetDamageTimer(){
        yield return new WaitForSeconds(0.3f);
        SpriteRenderer.material.color = Color.white;
    }

    IEnumerator GetAttackTimer(){
        yield return new WaitForSeconds(0.1f);
        AttackAnimRenderer.color = new Color(1f,1f,1f,0f);
    }
}
