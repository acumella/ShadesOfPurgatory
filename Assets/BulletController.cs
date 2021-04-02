﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;

    public float speed;

    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = (player.transform.position - transform.position).normalized * speed;
        
        if (rb.velocity.x > 0.05f)
        {
            transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (rb.velocity.x < -0.05f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        Destroy(gameObject, 5);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Enemy")) DestroyBullet();
    }

    public void DestroyBullet()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Animator>().SetBool("destroying", true);
        Destroy(gameObject,0.25f);
    }
}
