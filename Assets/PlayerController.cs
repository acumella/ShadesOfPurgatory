﻿ using System.Collections;
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

    private readonly int IDLE = 0, RUNNING = 1, JUMPING = 2;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float timeJump;
    [SerializeField] private TextMeshProUGUI healthText;

    private float moveSide;

    private bool isGrounded;
    private float timeJumpCounter;
    private bool jumpReleased = true;

    private bool leftPressed = false;
    private Vector3 mouseLastPos = Mouse.current.position.ReadValue();

    private int health;

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
        health = 5;
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
        AnimState();
    }

    //====================== PLAYER INPUT ========================
    void OnMove(InputValue input)
    {
        moveSide = input.Get<Vector2>().x;
    }

    void OnJumpPressed()
    {
        if (isGrounded)
        {
            jumpReleased = false;
        }
    }

    void OnJumpReleased()
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

    public void Ground(bool g)
    {
        isGrounded = g;
    }

    private void AnimState()
    {
        if (!isGrounded) SetState(JUMPING);
        else if (rb.velocity.magnitude > 0) SetState(RUNNING);
        else SetState(IDLE);
    }

    private void SetState(int state)
    {
        anim.SetInteger("state", state);
    }

    private void CheckGround()
    {
        if (isGrounded)
        {
            timeJumpCounter = timeJump;
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
        }
        else if (rb.velocity[0] > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

}
