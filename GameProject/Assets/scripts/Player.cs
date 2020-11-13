using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Animator anim;

    //Movement
    private float MoveInput;
    [SerializeField] float MoveVelocity = 200f;
    private float LastInput;

    //Jump
    [SerializeField] LayerMask GroundMask;
    [SerializeField] float Jumpvelocity = 10f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        MoveInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, Jumpvelocity);
        }


        //Anim
        if(!IsGrounded())
        {
            //Jump Animation
        }
        else if(MoveInput != 0)
        {
            //Run Animation
            anim.Play("Player-Run");
        }
        else
        {
            //Idle Animation
            anim.Play("Player-Idle");

        }
    }
    void FixedUpdate() 
    {
        rb.velocity = new Vector2(MoveInput * MoveVelocity * Time.fixedDeltaTime , rb.velocity.y);
        CheckDir();
    }
    void CheckDir()
    {
        if(LastInput != MoveInput)
        {
            if(MoveInput == 0)return;

            transform.localScale = new Vector3(MoveInput ,transform.localScale.y ,1f);
        }
        LastInput = MoveInput;
    }
    bool IsGrounded()
    {
        RaycastHit2D rch = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.1f, GroundMask);

        return rch.collider != null;
    }
}
