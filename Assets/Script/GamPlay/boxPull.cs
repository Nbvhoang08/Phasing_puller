using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxPull : MonoBehaviour
{
    public bool pulled;
    public Vector2 dirPull;
    public float speedPull;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (pulled)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;  
            rb.MovePosition((Vector2)transform.position + dirPull * -10 * Time.fixedDeltaTime);
        }
        else
        {
            dirPull = Vector2.zero;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("moc"))
        {
            pulled = true;
            if (col.gameObject.GetComponent<mocCau>().isRight)
            {
                dirPull = col.gameObject.GetComponent<mocCau>().direction;
            }
            else
            {
                dirPull = col.gameObject.GetComponent<mocCau>().direction * -1;
            }

        }
        else if (col.gameObject.CompareTag("moc") && col.gameObject.CompareTag("Line"))
        {
            pulled = false;
        }
       

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obs") || collision.gameObject.CompareTag("box"))
        {
            pulled = false;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }
    

}
