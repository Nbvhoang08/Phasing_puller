using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZIgZagTrrap : MonoBehaviour
{
    public float speed = 5f; // Tốc độ di chuyển của đối tượng
    public float radius = 5f; // Bán kính của đường tròn
    public float zigzagAngle = 30f; // Góc chuyển hướng ziczac
    public float minDistanceFromCenter = 1f; // Khoảng cách tối thiểu từ tâm

    private Vector2 _center = Vector2.zero; // Tâm của đường tròn
    private Vector2 _direction; // Hướng di chuyển hiện tại

    void Start()
    {
        // Khởi tạo hướng di chuyển ban đầu
        _direction = Quaternion.Euler(0, 0, zigzagAngle) * Vector2.right;
    }

    void Update()
    {
        // Di chuyển đối tượng theo hướng cục bộ
        transform.Translate(_direction * speed * Time.deltaTime, Space.World);

        // Kiểm tra nếu đối tượng chạm vào biên của đường tròn hoặc quá gần tâm
        float distanceToCenter = Vector2.Distance(transform.position, _center);
        if (distanceToCenter >= radius || distanceToCenter <= minDistanceFromCenter)
        {
            // Chuyển hướng ziczac
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        // Đảo ngược hướng di chuyển
        _direction = Quaternion.Euler(0, 0, -2 * zigzagAngle) * _direction;

        // Đảm bảo đối tượng không vượt quá bán kính và không quá gần tâm
        Vector2 directionToCenter = (_center - (Vector2)transform.position).normalized;
        float distanceToCenter = Vector2.Distance(transform.position, _center);

        if (distanceToCenter > radius)
        {
            Vector2 newPosition = _center + directionToCenter * radius;
            Vector2 adjustment = newPosition - (Vector2)transform.position;
            transform.Translate(adjustment, Space.World);
        }
        else if (distanceToCenter < minDistanceFromCenter)
        {
            Vector2 newPosition = _center + directionToCenter * minDistanceFromCenter;
            Vector2 adjustment = newPosition - (Vector2)transform.position;
            transform.Translate(adjustment, Space.World);
        }
    }

    void OnDrawGizmos()
    {
        // Vẽ đường tròn để dễ hình dung
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_center, radius);

        // Vẽ vòng giới hạn bên trong
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_center, minDistanceFromCenter);
    }
}
