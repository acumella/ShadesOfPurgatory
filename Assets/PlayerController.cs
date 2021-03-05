using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    public float timeJump;
    private float timeJumpCounter;
    public float jumpForce;

    private void Update()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-10, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            anim.SetBool("running", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(10, rb.velocity.y);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            anim.SetBool("running", true);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            anim.SetBool("running", false);
        }


        if (Input.GetKey(KeyCode.Space) && timeJumpCounter>0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            timeJumpCounter -= Time.deltaTime;
            anim.SetBool("jumping", true);
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "ground")
        {
            timeJumpCounter = timeJump;
            anim.SetBool("jumping", false);
        }
    }

}
