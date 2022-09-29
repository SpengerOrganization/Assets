using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Directions (Animation stuff)
    private readonly int UP = 3;
    private readonly int DOWN = 1;
    private readonly int SIDE = 2;
    private int Direction;
    private int? LastYDirection = null; 
    private readonly int LAST_RIGHT = 1;
    private readonly int LAST_LEFT = 2;

    // Component References
    private Animator animator;
    private Rigidbody2D rb;

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

        if(x==0&&y==0) return false;
        return true;
    }

}
