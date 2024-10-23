using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 _center = Vector2.zero; // Tâm của đường tròn
    public float radius = 5f; // Bán kính của đường tròn

    void Update()
    {
        // Kiểm tra nếu viên đạn vượt quá bán kính thì hủy nó
        if (Vector2.Distance(transform.position, _center) > radius)
        {
            Destroy(gameObject);
        }
    }
}
