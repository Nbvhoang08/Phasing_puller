﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;


public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    public mocCau mc;
    public bool moving;
    [SerializeField] float speed;
    [SerializeField] private float jumpForce;
    
    public Vector2 fallDirection = new Vector2(1, -1); // Hướng rơi chéo
    public float fallSpeed = 0.5f; // Tốc độ rơi chéo
    public Transform groundCheck;
    public LayerMask groundLayer;
    float hor;
    float ver;
    Vector2 dir;
 
    public bool isFacingRight = true;
    void Start()
    {
      rb = GetComponent<Rigidbody2D>(); 
  
      
    }
    private void Update()
    {
        hor = UnityEngine.Input.GetAxisRaw("Horizontal");
        ver = UnityEngine.Input.GetAxisRaw("Vertical");

        if (UnityEngine.Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
        }

        if (UnityEngine.Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        Flip();
       

    }
    void FixedUpdate()
    {

        if (UnityEngine.Input.GetKey(KeyCode.X))
        {
            moving = false;
            rb.velocity = Vector2.zero;
        }
        else
        {
            moving = true;
            if (IsGrounded())
            {
                rb.velocity = new Vector2(hor * speed, rb.velocity.y);
            }
            else
            {
                if (hor != 0) // Nếu có di chuyển ngang khi đang rơi
                {
                    rb.velocity = new Vector2(hor * speed, rb.velocity.y) + fallDirection * fallSpeed;
                }
                else // Nếu không di chuyển ngang, chỉ rơi tự do
                {
                    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                }
            }
        }
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void Flip()
    {
        if (isFacingRight && hor < 0f || !isFacingRight && hor > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    public Vector2 dirPuller()
    { 
        
        if (ver == 0)
        {
            dir = Vector2.right;
           
        }
        else
        {
            dir = new Vector2(0, ver);
          
        }

        return dir;
    }
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        }
    }

}

