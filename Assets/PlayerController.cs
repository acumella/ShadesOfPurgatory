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
    private CapsuleCollider2D capsule;
    public static PlayerController current;

    private readonly int IDLE = 0, RUNNING = 1, JUMPING = 2, DEATH = 9;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask bulletLayer;
    [SerializeField] private CursorManager cursor;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameOver gameOver;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float timeJump;
    [SerializeField] private float timeHit;
    [SerializeField] private float timeInvulnerability;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool invencible = false;

    private float moveSide;

    private bool isGrounded;
    
    private float timeJumpCounter;
    public bool jumpReleased = true;

    private bool leftPressed, attacking = false;
    private bool rightPressed, defending = false;
    private Vector3 mouseLastPos;
    private float timeHitCounter;

    public int health;
    private float invulnerabilityCounter;

    private GameObject[] players;
    private int previousLevel = 0;

    public bool isDying = false;

    

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
        healthBar.SetHealth(health);
        mouseLastPos = Mouse.current.position.ReadValue();
    }

    void Update()
    {
        if (!PauseMenu.isPaused && !isDying)
        {
            MouseHit();
        }   
    }


    void FixedUpdate()
    {
        CheckGround();
        if (!isDying)
        {
            Jump();
            Move();
            Turn();
        }
        AnimState();
        ResetInvulnerability();
    }

    //====================== PLAYER INPUT ========================
    void OnMove(InputValue input)
    {
        moveSide = input.Get<Vector2>().x;
    }

    void OnJumpPressed()
    {
        if (!PauseMenu.isPaused)
        {
            if (isGrounded) {
                jumpReleased = false;
            }
        }
        
    }

    void OnJumpReleased()
    {
        if (!PauseMenu.isPaused)
        {
            jumpReleased = true;
        }
    }

    void OnLeftPressed()
    {
        if (!PauseMenu.isPaused)
        {
            attacking = true;
            defending = false;
            leftPressed = true;
        }
    }

    void OnLeftReleased()
    {
        if (!PauseMenu.isPaused)
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
    }

    void OnRightPressed()
    {
        if (!PauseMenu.isPaused)
        {
            cursor.ChangeCursorOnRightClick();
            attacking = false;
            defending = true;
            rightPressed = true;
        }
    }

    void OnRightReleased()
    {
        if (!PauseMenu.isPaused)
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
    }


    //====================== FUNCTIONS ==============================

    private void AnimState()
    {
        if (isDying) SetState(DEATH);
        else if (!isGrounded) SetState(JUMPING);
        else if (rb.velocity.magnitude > 0) SetState(RUNNING);
        else SetState(IDLE);
    }

    private void SetState(int state)
    {
        anim.SetInteger("state", state);
    }

    private void CheckGround()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(capsule.bounds.center, Vector2.down, capsule.bounds.extents.y + .15f, groundLayer);
        isGrounded = raycastHit.collider != null;
        if (isGrounded)
        {
            timeJumpCounter = timeJump;
        }
    }

    private void MouseHit()
    {
        if (timeHitCounter <= 0) {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            Collider2D hit = Physics2D.OverlapCircle(mousePos2D, 0.5f, enemyLayer);

            //Debug.Log((mousePos - mouseLastPos).magnitude);
            float distance = (mousePos - mouseLastPos).magnitude;

            if (attacking)
            {
                cursor.ChangeCursorOnLeftClick();
                if (hit != null && distance > 0.5 && attacking)
                {
                    //Debug.Log((mousePos - mouseLastPos).magnitude);
                    if (hit.tag == "Enemy")
                    {
                        //Debug.Log(hit.tag);
                        cursor.ChangeCursorToDefault();
                        float damage = (distance * 0.4f) / 0.5f;
                        Debug.Log(damage);
                        if (damage > 1.25) damage = 1.25f;
                        hit.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damage);
                        timeHitCounter = timeHit;
                    }
                }
            }

            mouseLastPos = mousePos;
        } else {
            timeHitCounter -= Time.deltaTime;
        }

        if (defending) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            Collider2D hit = Physics2D.OverlapCircle(mousePos2D, 0.5f, bulletLayer);

            if (hit != null)
            {
                if (hit.tag == "Bullet")
                {
                    hit.gameObject.GetComponent<BulletController>().DestroyBullet();
                }
            }
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

    public void Damage()
    {
        if (invulnerabilityCounter <= 0 && !isDying && !invencible)
        {
            health -= 1;
            healthBar.SetHealth(health);
            invulnerabilityCounter = timeInvulnerability;
            GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.playerHealth = health;
            GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.Save();
            SoundManager.PlaySound("playerDamaged");

            if(health <= 0)
            {
                Die();
            }
        }
    }

    public void Reset()
    {
        health = 5;
        isDying = false;
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.playerHealth = health;
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.Save();
    }

    private void ResetInvulnerability()
    {
        if (invulnerabilityCounter > 0)
        {
            invulnerabilityCounter -= Time.deltaTime;
        }
    }

    public void SetHealth(int hp)
    {
        health = hp;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDying = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void Respawn()
    {
        if (health == 0) gameOver.Pause();
        else GameMaster.Spike();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Bullet")
        {
            Damage();
        } else if (collision.gameObject.tag == "Spikes")
        {
            Damage();
            Die();
        }
          

    }

}
