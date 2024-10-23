using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Process_Text : MonoBehaviour
{
    // Start is called before the first frame update
    public Text processText; // Tham chiếu đến UI Text
    private int process = 0; // Biến lưu điểm số
    Trail trail;

    private void Start()
    {
        trail = GameObject.FindWithTag("Player").GetComponent<Trail>();
    }
    void Update()
    {
        process = Mathf.FloorToInt(trail._fillPercentage);
        processText.text = process.ToString(); // Hiển thị điểm số
    }

    // Hàm để tăng điểm và cập nhật text
    
    // Hàm cập nhật nội dung text
   
}
