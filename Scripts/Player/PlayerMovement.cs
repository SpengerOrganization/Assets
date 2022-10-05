using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Directions (Animation stuff)
    private const int UP = 3;
    private const int DOWN = 1;
    private const int SIDE = 2;
    private int Direction;
    private int? LastYDirection = null; 
    private const int LAST_RIGHT = 1;
    private const int LAST_LEFT = 2;

    // Component References
    private Animator animator;
    private Rigidbody2D rb;
    public Transform AttackPoint;

    // Player Attributes
    public float Speed = 3.5f;
    private bool Running;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Running = GetDirection(x, y);
        animator.SetBool("Running", Running);
        animator.SetInteger("Direction", Direction);

        rb.MovePosition(rb.position + new Vector2(x, y) * Speed * Time.fixedDeltaTime);
    }



    private bool GetDirection(float x, float y){
        switch(x){
            case >0:
                // right
                // mirror player if needed
                if(LastYDirection == LAST_LEFT){
                    transform.Rotate(0, 180, 0);
                }
                Direction = SIDE;
                LastYDirection = LAST_RIGHT;
            break;
            case <0:
                // left
                // mirror player if needed
                if(LastYDirection == LAST_RIGHT || LastYDirection==null){
                    transform.Rotate(0, 180, 0);
                }
                Direction = SIDE;
                LastYDirection = LAST_LEFT;
            break;
            default:
                switch(y){
                    case >0:
                        Direction = UP;
                        break;
                    case <0:
                        Direction = DOWN;
                        break;
                }
            break;
        }

        PositionAttackPoint(Direction);

        if(x==0&&y==0) return false;
        return true;
    }

    private void PositionAttackPoint(int Direction){
        switch(Direction){
            case UP:
                AttackPoint.position = transform.position + new Vector3(0, 0, 0);
                AttackPoint.position = transform.position + new Vector3(0, 0.7f, 0);
            break;
            case DOWN:
                AttackPoint.position = transform.position + new Vector3(0, 0, 0);
                AttackPoint.position = transform.position + new Vector3(0, -0.7f, 0);
            break;
            case SIDE:
                AttackPoint.position = transform.position + new Vector3(0, 0, 0);
                if(transform.rotation.y == -1){
                    AttackPoint.position = transform.position + new Vector3(-0.7f, 0, 0);
                }else{
                    AttackPoint.position = transform.position + new Vector3(0.7f, 0, 0);
                }
            break;
        }
    }

}
