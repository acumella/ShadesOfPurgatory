using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private Seeker seeker;
    private CapsuleCollider2D capsule;
    private Animator anim;
    private Transform target;
    private GameData gd;

    private readonly int IDLE = 0, RUNNING = 1, JUMPING = 2, FIRING = 3, ATTACKING = 4, DEATH = 9;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float speed = 800f;
    [SerializeField] private float nextWaypointDistance = 2f;
    [SerializeField] private float jumpNodeHeightRequirement = 0.8f;
    [SerializeField] private float jumpForce = 2000f;
    [SerializeField] private float activateDistance = 50f;
    [SerializeField] private float pathUpdateSeconds = 0.5f;

    //[SerializeField] private float attackRange;
    [SerializeField] private float attackDelay;

    [SerializeField] private bool canAttack = false;
    [SerializeField] private bool canFly = false;
    [SerializeField] private bool canShoot = false;

    [SerializeField] private float shootingRange;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private float startTimeShots;

    [SerializeField] private GameObject slash;

    private float timeShots;

    private bool followEnabled = true;
    private bool jumpEnabled = true;
    private bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded;

    private bool isAttacking = false;
    private float lastAttackTime;
    public bool playerInRange = false;

    private bool isDead = false;
    private bool activated = false;


    public void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        gd = GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd;

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if (Time.time > (lastAttackTime + attackDelay) && isAttacking)
        {
            //Debug.Log(Time.time);
            isAttacking = false;
        }

        if (!isAttacking)
        {
            if (playerInRange)
            {
                Attack();
            }
            else if (TargetInDistance() && followEnabled && !isDead)
            {
                PathFollow();
            }
        }
        
        GroundCheck();
        AnimState();
    }

    //====================== FUNCTIONS ========================

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // Force calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        // Jump
        if (jumpEnabled && isGrounded && !canFly && !isAttacking)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }        

        // Attack
        //Vector2 posForward = transform.position + transform.forward;
        //float distanceToPlayer = Vector3.Distance(transform.position, target.position);

        float distance = Vector3.Distance(transform.position, target.transform.position);

        /*
        if (distanceToPlayer < attackRange && canAttack)
        {
            Collider2D hit = Physics2D.OverlapCircle(posForward, attackRange, playerLayer);
            if (Time.time > lastAttackTime + attackDelay && hit!=null)
            {
                if(hit.tag == "Player")
                {
                    lastAttackTime = Time.time;
                    isAttacking = true;
                    StartCoroutine(Attack());
                }
            }
        }
        */

        // Shoot
        if (distance <= shootingRange && lineOfSight() && canShoot && !isAttacking)
        {
            if (timeShots <= 0)
            {
                Instantiate(bullet, muzzle.transform.position, Quaternion.identity);
                timeShots = startTimeShots;
            }
            else
            {
                timeShots -= Time.deltaTime;
            }
        }

        // Move
        else if ((distance < activateDistance) && !isAttacking)
        {
            rb.AddForce(force);
        }

        // Next Waypoint
        distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Flip sprite
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }
    private bool TargetInDistance()
    {
        if ((Vector2.Distance(transform.position, target.transform.position) < activateDistance) && (lineOfSight() || activated))
        {
            activated = true;
            return true;
        }
        return false;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private bool lineOfSight()
    {
        Vector3 start = transform.position;
        Vector3 direction = (target.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, target.transform.position);


        RaycastHit2D sightTest = Physics2D.Raycast(start, direction, distance, groundLayer);
        if (sightTest.collider != null)
        {
            //Debug.Log("Rigidbody collider is: " + sightTest.collider.tag);
            return false;
        }
        return true;

    }

    private void GroundCheck()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(capsule.bounds.center, Vector2.down, capsule.bounds.extents.y + .01f, groundLayer);
        isGrounded = raycastHit.collider != null;
    }

    private void AnimState()
    {
        if (isDead) SetState(DEATH);
        else if (isAttacking) SetState(ATTACKING);
        else if (!isGrounded) SetState(JUMPING);
        else if (Vector3.Distance(transform.position, target.transform.position) <= shootingRange && lineOfSight() && canShoot) SetState(FIRING);
        else if (rb.velocity.magnitude > 0.5) SetState(RUNNING);
        else SetState(IDLE);
    }

    private void SetState(int state)
    {
        anim.SetInteger("state", state);
    }

    public void Attack()
    {
        if (!isAttacking && canAttack)
        {
            isAttacking = true;
            lastAttackTime = Time.time;
        }
    }

    public void DamagePlayer()
    {
        SoundManager.PlaySound("enemyAttack");
        if (playerInRange)
        {
            target.gameObject.GetComponent<PlayerController>().Damage();
            Debug.Log("esto esta mal");
        }
    }

    public void Die()
    {
        isDead = true;
        gd.addEnemyDestroyed(SceneManager.GetActiveScene().buildIndex, this.gameObject);
        gd.Save();
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    public void Slash()
    {
        Instantiate(slash, transform.position, Quaternion.identity);
        SoundManager.PlaySound("playerSlash");
    }

}
