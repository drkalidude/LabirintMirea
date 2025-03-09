using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapShooter : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint; 
    [SerializeField] private Transform targetPoint; 
    [SerializeField] private float fireRate = 2.0f; 
    [SerializeField] private float bulletSpeed = 0.5f; 

    private void Start()
    {
        StartCoroutine(ShootBullets());
    }

    private IEnumerator ShootBullets()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null || targetPoint == null) return;

        Vector3 direction = (targetPoint.position - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed;
            rb.angularVelocity = Vector3.zero;
        }

        Destroy(bullet, 5f);
    }
}
