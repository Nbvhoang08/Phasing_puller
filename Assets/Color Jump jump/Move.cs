using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Jump Physics")]
    public float jumpForce = 10f;        // Lực nhảy ban đầu
    public float gravity = 15f;         // Lực hấp dẫn
    public float bounceMultiplier = 0.5f; // Hệ số nảy khi chạm vòng ngoài

    private float _angle;                // Góc di chuyển hiện tại
    [SerializeField] private bool _isJumping = false;     // Đang nhảy?
    private float _currentRadius;        // Bán kính hiện tại
    private Vector2 _center = Vector2.zero;
    private float _radialVelocity;       // Vận tốc theo hướng bán kính
    private bool _isMovingInward = true; // Đang di chuyển vào trong?
    private int _spinDirection = 1;
    public GameObject spawPos;
    public float outerRadius;
    public float speed;
    public bool CanDraw = true;

    // Biến để theo dõi thời gian giữ nút và lực nhảy
    private float _holdTime = 0f;
    private bool _isHolding = false;
    public float _maxJumpForce = 15f;
    private float _thresholdTime = 0.2f; // Thời gian ngưỡng để đạt lực nhảy tối đa

    void Start()
    {
        _currentRadius = outerRadius;
    }

    void  Update()
    {
     
        // Xử lý input nhảy
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && !_isJumping)
        {
            _isHolding = true;
            _holdTime = 0f;
        }

        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && _isHolding)
        {
            _holdTime += Time.deltaTime;
        }

        if ((Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space)) && _isHolding)
        {
            _isHolding = false;
            if (_holdTime >= _thresholdTime)
            {
                jumpForce = _maxJumpForce;
            }
            else
            {
                jumpForce = 10f; // Lực nhảy mặc định nếu không giữ đủ lâu
            }
            StartJump();
        }

        // Cập nhật physics và chuyển động
        UpdatePhysics();
        UpdatePosition();
        UpdateRotation();

        // Debug

    }

    void StartJump()
    {
        _isJumping = true;
        _radialVelocity = -jumpForce; // Âm để nhảy vào trong
        _isMovingInward = true;
        CanDraw = false;
        StartCoroutine(Spin180(1.0f));
    }

    void UpdatePhysics()
    {
        if (!_isJumping) return;

        // Cập nhật góc di chuyển
        _angle += speed * Time.deltaTime;

        // Áp dụng lực hấp dẫn về phía vòng ngoài
        float gravityForce = _isMovingInward ? gravity : gravity * 1.5f; // Tăng gravity khi đi ra
        _radialVelocity += gravityForce * Time.deltaTime;

        // Cập nhật bán kính
        _currentRadius += _radialVelocity * Time.deltaTime;

        // Kiểm tra va chạm với vòng ngoài
        if (_currentRadius >= outerRadius)
        {
            HandleOuterCollision();
        }

        // Chuyển hướng khi đạt đến điểm gần tâm nhất
        if (_isMovingInward && _radialVelocity > 0)
        {
            _isMovingInward = false;
        }

        // Giới hạn không cho nhảy quá gần tâm
        float minRadius = outerRadius * 0.3f;
        if (_currentRadius < minRadius)
        {
            _currentRadius = minRadius;
            _radialVelocity = 0;
            _isMovingInward = false;
        }
    }

    void HandleOuterCollision()
    {
        _currentRadius = outerRadius;

        // Nếu vận tốc đủ nhỏ, dừng nhảy
        if (Mathf.Abs(_radialVelocity) < 0.5f)
        {
            _isJumping = false;
            _radialVelocity = 0;
            CanDraw = true;
        }
        else // Nảy lại với vận tốc giảm
        {
            _radialVelocity = -_radialVelocity * bounceMultiplier;
            _isMovingInward = true;
          /*  _isJumping = false;*/
        }
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
        transform.position = new Vector2(x, y);
    }

    void UpdateRotation()
    {
        // Nhân vật luôn hướng về tâm
        Vector2 direction = (_center - (Vector2)transform.position).normalized;
        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90f * _spinDirection);
    }

    System.Collections.IEnumerator Spin180(float duration)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 180);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
        _spinDirection *= -1; // Đảo hướng xoay
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log(collision.name);
        }
    }

    void OnDrawGizmos()
    {
        // Vẽ vòng ngoài
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_center, outerRadius);

        // Vẽ vòng giới hạn bên trong
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_center, outerRadius * 0.3f);
    }
}
