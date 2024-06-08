using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gate : MonoBehaviour
{
    public Transform Destination;  // Đặt vị trí đích trong Inspector

    private bool isPlayerInTrigger = false;  // Kiểm tra xem người chơi có trong vùng kích hoạt không
    private GameObject player;  // Biến lưu trữ đối tượng người chơi
    public int indexDestination;
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
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.V))  // Kiểm tra xem người chơi có nhấn phím V không
        {
            Debug.Log("Tele");
            if (player != null && Destination != null)
            {
                player.transform.position = Destination.position;  // Dịch chuyển người chơi đến vị trí đích
                MapManager.Instance.changeMap(indexDestination);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // Kiểm tra xem đối tượng vào có tag là "Player" không
        {
            isPlayerInTrigger = true;
            player = collision.gameObject;  // Lưu trữ đối tượng người chơi
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // Kiểm tra xem đối tượng ra có tag là "Player" không
        {
            isPlayerInTrigger = false;
            player = null;  // Xóa đối tượng người chơi khi họ rời khỏi vùng kích hoạt
        }
    }
}

