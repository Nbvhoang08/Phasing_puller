using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMove : MonoBehaviour
{
    [Header("Jump Physics")]
    public float jumpForce = 8f;        // Lực nhảy ban đầu
    public float gravity = 15f;         // Lực hấp dẫn
    public float bounceMultiplier = 0.5f; // Hệ số nảy khi chạm vòng ngoài

    private float _angle;                // Góc di chuyển hiện tại
    private bool _isJumping = false;     // Đang nhảy?
    private float _currentRadius;        // Bán kính hiện tại
    private Vector2 _center = Vector2.zero;   
    public float outerRadius;
    public float speed;
    public bool CanDraw = true;
    void Start()
    {
        _currentRadius = outerRadius;
    }

    void Update()
    {


        // Cập nhật physics và chuyển động

        UpdatePosition();
        UpdateRotation();

        // Debug

    }


    void UpdatePosition()
    {
        if (!_isJumping)
        {
            // Di chuyển bình thường trên vòng ngoài
            _angle += speed * Time.deltaTime;
            _currentRadius = outerRadius;
        }

        // Cập nhật vị trí
        float x = _center.x + Mathf.Cos(_angle) * _currentRadius;
        float y = _center.y + Mathf.Sin(_angle) * _currentRadius;
        transform.position = new Vector2(x, -y);
    }

    void UpdateRotation()
    {
        // Nhân vật luôn hướng về tâm
        Vector2 direction = (_center - (Vector2)transform.position).normalized;
        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90f);
    }

} 
