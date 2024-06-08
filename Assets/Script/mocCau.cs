using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mocCau : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 direction;
    public Player player;
    public bool pulling;
    public Transform starPos;
    private bool getdirection = true;
    [SerializeField] private  LayerMask box;
    public bool isRight;
    public bool var = true;
    public float moveSpeed = 5f;


    void Start()
    {
        getdirection = true;
        
    }
    void OnEnable()
    {
        
    }
    private void Update()
    {
       
        isRight = player.isFacingRight;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (UnityEngine.Input.GetKey(KeyCode.X)&& var)
        {
            pulling = true;
            if (getdirection)
            {
                getdirOT();
            }
        }
        else
        {
            pulling = false;
        }
        pull();
        
    }



    void pull()
    {
        if (pulling)
        {
            transform.Translate(direction * 10 * Time.fixedDeltaTime);
        }
        else
        {
            /*transform.Translate(direction * -10 * Time.fixedDeltaTime);*/
            returnSP();
            transform.position= player.transform.position;
        }
    }
    void returnSP()
    {
        // Quay lại điểm bắt đầu
        if (Vector2.Distance(starPos.position, this.transform.position) <= 0.5f)
        {
            this.transform.position = starPos.position;
            direction = Vector2.zero;
            getdirection = true; // Cho phép gán lại direction
            var = true;
        }
    }

    void getdirOT()
    {
        if (getdirection)
        {
            direction = player.dirPuller();
            getdirection = false;
        }
    }

   
    public bool isbox()
    {
        return Physics2D.OverlapCircle(this.transform.position, 0.5f, box);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("box"))
        {
            var = true;
            col.gameObject.GetComponent<boxPull>().pulled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("box"))
        {
            var = false;
            pulling = false;
        }
    }

    private void OnDrawGizmos()
    {
       
    }



}
