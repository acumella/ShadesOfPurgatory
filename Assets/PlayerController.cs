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
    private bool jumpReleased = true;

    bool leftPressed = false;

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

    void Update()
    {
        CheckGround();
        MouseHit();
    }


    void FixedUpdate()
    {
        Jump();
        Move();
        Turn();      
    }

    //====================== PLAYER INPUT ========================
    void OnMove(InputValue input)
    {
        moveSide = input.Get<Vector2>().x;
    }

    void OnJumpPress()
    {
        if (isGrounded)
        {
            jumpReleased = false;
        }
    }

    void OnJumpRelease()
    {
        jumpReleased = true;
    }

    void OnLeftPressed()
    {
        leftPressed = true;
    }

    void OnLeftReleased()
    {
        leftPressed = false;
    }

    //====================== FUNCTIONS ==============================

    private void CheckGround()
    {
        if (isGrounded)
        {
            timeJumpCounter = timeJump;
            anim.SetBool("jumping", false);
        }
    }

    private void MouseHit()
    {
        if (leftPressed)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
            }
        }
    }

    private void Jump()
    {
        if (timeJumpCounter > 0 && !jumpReleased)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetBool("jumping", true);
            timeJumpCounter -= Time.deltaTime;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveSide * speed, rb.velocity.y);
    }

    private void Turn()
    {
        if (rb.velocity[0] < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            anim.SetBool("running", true);
        }
        else if (rb.velocity[0] > 0)
        {
            transform.localScale = new Vector2(1, 1);
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

}
