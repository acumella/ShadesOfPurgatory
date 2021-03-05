 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public static PlayerController current;

    public float speed;
    public bool isGrounded;
    public float jumpForce;

    public float timeJump;
    private float timeJumpCounter;

    private float moveSide;
    private bool jumped = false;
    private bool jumpReleased = true;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void OnMove(InputValue input)
    {
        moveSide = input.Get<Vector2>().x;
    }

    void OnJumpPress()
    {
        if (isGrounded) { 
            jumpReleased = false;
            jumped = true;
        }
    }

    void OnJumpRelease()
    {
        jumpReleased = true;
        jumped = false;        
    }

    void Update()
    {
        if(isGrounded)
        {
            timeJumpCounter = timeJump;
            anim.SetBool("jumping", false);
        }
    }


    void FixedUpdate()
    {
        if (timeJumpCounter > 0 && !jumpReleased)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool("jumping", true);
            timeJumpCounter -= Time.deltaTime;
            jumped = false;
        }

        rb.velocity = new Vector2(moveSide * speed, rb.velocity.y);

        if (rb.velocity[0] < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            anim.SetBool("running", true);
        } else if(rb.velocity[0] > 0)
        {
            transform.localScale = new Vector2(1, 1);
            anim.SetBool("running", true);
        } else {
            anim.SetBool("running", false);
        }
    }
    
}
