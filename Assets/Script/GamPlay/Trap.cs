using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform Destination;  // Đặt vị trí đích trong Inspector

    void Start()
    {
        // Đảm bảo vị trí đích được thiết lập
        if (Destination == null)
        {
            Debug.LogError("Destination is not set for the Gate.");
        }
    }

    void Update()
    {
        
    }

 

   
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // Kiểm tra xem đối tượng ra có tag là "Player" không
        {
            collision.gameObject.transform.position = Destination.position;
            SoundManager.Instance.ActionSound(3);
        }
    }
}
