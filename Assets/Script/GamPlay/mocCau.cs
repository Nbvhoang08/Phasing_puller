using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mocCau : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 direction;
    public Vector2 initialDirection;
    public Player player;
    public bool pulling;
    public Transform starPos;
    private bool getdirection = true;
    [SerializeField] private  LayerMask box;
    public bool isRight;
    public bool var = true;
    public bool checkobs ;
    public bool ishorizontal;
    public float moveSpeed = 5f;
    Quaternion targetRotation = Quaternion.Euler(0, 0, 90);
    public SpriteRenderer spriteRenderer;
    private DayCau dayCau;
    void Start()
    {
        getdirection = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!dayCau)
        {
            dayCau = transform.parent.GetComponent<DayCau>();
        }
    }
    void OnEnable()
    {
        
    }
    private void Update()
    {

        isRight = player.isFacingRight;
        checkobs = dayCau.checkObs;

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (UnityEngine.Input.GetKey(KeyCode.X) && var)
        {
            pulling = true;
            if (getdirection)
            {
                getdirOT();
                if (direction.y != 0)
                {
                    ishorizontal = false;
                    transform.localScale = new Vector3(1,-direction.y,1) ;

                }
                else
                {

                    if (isRight)
                    {
                        spriteRenderer.flipY = false;
                        initialDirection = -player.transform.up;
                    }else
                    {
                        initialDirection = player.transform.up;
                        spriteRenderer.flipY = true;
                    }
                    
                    ishorizontal = true;

                    // Nếu hướng di chuyển theo trục Y, thay đổi góc xoay của transform
                    transform.rotation = targetRotation;
                }
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
      
            if (ishorizontal)
            {
                transform.Translate(initialDirection * 10 * Time.fixedDeltaTime);
            }
            else
            {
                transform.Translate(direction * 10 * Time.fixedDeltaTime);
            }
            
        }
        else
        {
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
            getdirection = true; 
            var = true;
            transform.rotation = Quaternion.identity;
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




}
