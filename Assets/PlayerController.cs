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
    private CapsuleCollider2D capsule;
    public static PlayerController current;

    private readonly int IDLE = 0, RUNNING = 1, JUMPING = 2;

    [SerializeField] private CursorManager cursor;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float timeJump;
    [SerializeField] private float timeHit;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TextMeshProUGUI healthText;

    private float moveSide;

    private bool isGrounded;
    
    private float timeJumpCounter;
    private bool jumpReleased = true;

    private bool leftPressed, attacking = false;
    private bool rightPressed, defending = false;
    private Vector3 mouseLastPos;
    private float timeHitCounter;

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
        capsule = GetComponent<CapsuleCollider2D>();
        health = 5;
        healthText.text = health.ToString();
        mouseLastPos = Mouse.current.position.ReadValue();
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
        attacking = true;
        defending = false;
        leftPressed = true;
    }

    void OnLeftReleased()
    {
        if (rightPressed)
        {
            defending = true;
            cursor.ChangeCursorOnRightClick();
        }
        else
        {
            cursor.ChangeCursorToDefault();
        }
        attacking = false;
        leftPressed = false;
    }

    void OnRightPressed()
    {
        cursor.ChangeCursorOnRightClick();
        attacking = false;
        defending = true;
        rightPressed = true;
    }

    void OnRightReleased()
    {
        if (leftPressed)
        {
            attacking = true;
            cursor.ChangeCursorOnLeftClick();
        }
        else
        {
            cursor.ChangeCursorToDefault();
        }
        defending = false;
        rightPressed = false;
    }


    //====================== FUNCTIONS ==============================

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
        RaycastHit2D raycastHit = Physics2D.Raycast(capsule.bounds.center, Vector2.down, capsule.bounds.extents.y + .01f, groundLayer);
        isGrounded = raycastHit.collider != null;
        if (isGrounded)
        {
            timeJumpCounter = timeJump;
        }
    }

    private void MouseHit()
    {
        if (attacking)
        {
            if (timeHitCounter <= 0) {

                cursor.ChangeCursorOnLeftClick();

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                Collider2D hit = Physics2D.OverlapCircle(mousePos2D, 0.5f);

                //Debug.Log((mousePos - mouseLastPos).magnitude);
                float distance = (mousePos - mouseLastPos).magnitude;

                if (hit != null && distance > 0.5)
                {
                    if (hit.tag == "Enemy")
                    {
                        cursor.ChangeCursorToDefault();
                        float damage = (distance * 0.3f) / 0.5f;
                        if (damage > 1) damage = 1;
                        hit.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damage);
                        timeHitCounter = timeHit;
                    }
                }
                mouseLastPos = mousePos;
            }

        } else if (defending) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            Collider2D hit = Physics2D.OverlapCircle(mousePos2D, 0.5f);

            if (hit != null)
            {
                if (hit.tag == "Bullet")
                {
                    hit.gameObject.GetComponent<BulletController>().DestroyBullet();
                }
            }
        }

        if (timeHitCounter > 0)
        {
            timeHitCounter -= Time.deltaTime;
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