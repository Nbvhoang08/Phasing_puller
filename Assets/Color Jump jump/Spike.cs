using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float radius = 5f; // Bán kính của đường tròn
    public float moveInterval = 3f; // Khoảng thời gian giữa các lần dịch chuyển
    public float minColliderDelay = 1f; // Thời gian tối thiểu để bật lại collider
    public float maxColliderDelay = 2f; // Thời gian tối đa để bật lại collider
    private Vector2 _center = Vector2.zero; // Tâm của đường tròn
    private Collider2D _collider;

    void Start()
    {
        _collider = GetComponent<Collider2D>();
        StartCoroutine(MoveObstacle());
    }

    IEnumerator MoveObstacle()
    {
        while (true)
        {
            // Dịch chuyển tới vị trí ngẫu nhiên trên đường tròn
            Vector2 newPosition = GetRandomPositionOnCircle();
            transform.position = newPosition;

            // Hướng đỉnh của tam giác về tâm của đường tròn
            Vector2 direction = (_center - newPosition).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            // Tắt collider trong khi dịch chuyển
            _collider.enabled = false;

            // Chờ một khoảng thời gian ngẫu nhiên trước khi bật lại collider
            float delay = Random.Range(minColliderDelay, maxColliderDelay);
            yield return new WaitForSeconds(delay);

            // Bật lại collider
            _collider.enabled = true;

            // Chờ đến lần dịch chuyển tiếp theo
            yield return new WaitForSeconds(moveInterval);
        }
    }

    Vector2 GetRandomPositionOnCircle()
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float x = _center.x + Mathf.Cos(angle) * radius;
        float y = _center.y + Mathf.Sin(angle) * radius;
        return new Vector2(x, y);
    }

    void OnDrawGizmos()
    {
        // Vẽ đường tròn để dễ hình dung
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_center, radius);
    }
}
