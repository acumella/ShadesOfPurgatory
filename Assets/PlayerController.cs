 using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public static PlayerController current;

    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    public bool isGrounded;

    [SerializeField] private float timeJump;
    private float timeJumpCounter;

    private float moveSide;
    private bool jumpReleased = true;

    private bool leftPressed = false;
    Vector3 mouseLastPos = Mouse.current.position.ReadValue();

    private int health = 5;
    [SerializeField] private TextMeshProUGUI healthText; 

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
        healthText.text = health.ToString();
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

            Debug.Log((mousePos - mouseLastPos).magnitude);
            float distance = (mousePos - mouseLastPos).magnitude;

            if (hit.collider != null && distance>0.4)
            {
                if(hit.collider.tag == "Player")
                {
                    health-=1;
                    healthText.text = health.ToString();
                }
            }

            mouseLastPos = mousePos;
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
