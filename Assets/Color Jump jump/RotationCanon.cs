using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCanon : MonoBehaviour
{
    public float rotationSpeed = 30f; // Tốc độ xoay của khẩu pháo (độ/giây)
    public float minShootInterval = 2f; // Thời gian tối thiểu giữa các lần bắn
    public float maxShootInterval = 5f; // Thời gian tối đa giữa các lần bắn
    public GameObject bulletPrefab; // Prefab của viên đạn
    public float bulletSpeed = 10f; // Tốc độ của viên đạn
    public float bulletLifetime = 5f; // Thời gian sống của viên đạn trước khi bị hủy
    public float radius = 5f; // Bán kính của đường tròn

    private bool _isShooting = false;
    private float _nextShootTime;

    void Start()
    {
        SetNextShootTime();
    }

    void Update()
    {
        if (!_isShooting)
        {
            // Xoay khẩu pháo
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

            // Kiểm tra nếu đến thời gian bắn
            if (Time.time >= _nextShootTime)
            {
                StartCoroutine(ShootAndWait());
            }
        }
    }

    void SetNextShootTime()
    {
        _nextShootTime = Time.time + Random.Range(minShootInterval, maxShootInterval);
    }

    IEnumerator ShootAndWait()
    {
        _isShooting = true;

        // Bắn đạn
        Shoot();

        // Chờ một khoảng thời gian ngắn trước khi tiếp tục xoay
        yield return new WaitForSeconds(0.5f);

        // Thiết lập thời gian bắn tiếp theo
        SetNextShootTime();

        _isShooting = false;
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
        Destroy(bullet, bulletLifetime);
    }
}
