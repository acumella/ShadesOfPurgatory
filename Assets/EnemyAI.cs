using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private Seeker seeker;
    private CapsuleCollider2D capsule;

    [SerializeField] private Transform target;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed = 800f;
    [SerializeField] private float nextWaypointDistance = 2f;
    [SerializeField] private float jumpNodeHeightRequirement = 0.8f;
    [SerializeField] private float jumpForce = 2000f;
    [SerializeField] private float activateDistance = 50f;
    [SerializeField] private float pathUpdateSeconds = 0.5f;

    [SerializeField] private bool canShoot = false;
    [SerializeField] private float shootingRange;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject muzzle;
    [SerializeField] private float startTimeShots;

    private float timeShots;

    private bool followEnabled = true;
    private bool jumpEnabled = true;
    private bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded;
    

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
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

        // Check ground
        RaycastHit2D raycastHit = Physics2D.Raycast(capsule.bounds.center, Vector2.down, capsule.bounds.extents.y + .01f, groundLayer);
        isGrounded = raycastHit.collider != null;


        // Force calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        // Jump
        if (jumpEnabled && isGrounded)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * jumpForce);
            }
        }
        
        //Shoot
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if(distance <= shootingRange && lineOfSight() && canShoot)
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
        else if ((distance < activateDistance))
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
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
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
            Debug.Log("Rigidbody collider is: " + sightTest.collider.tag);
            return false;
        }
        return true;

    }

}
