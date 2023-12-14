using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public Transform firePoint;         // Ateşin başlayacağı nokta
    public GameObject bulletPrefab;     // Ateş mermisi prefabı
    public float fireRate = 2f;         // Ateş etme hızı (saniyede ateş sayısı)

    private float nextFireTime = 1f;    // Bir sonraki ateş zaman



    private void OnTriggerStay(Collider other)
    {

        if (Time.time >= nextFireTime && other.gameObject.CompareTag("Enemy"))
        {
            // Ateş etme fonksiyonunu çağır
            Fire(other.transform);

            // Bir sonraki ateş zamanını güncelle (ateş aralığı kadar artır)
            nextFireTime = Time.time + 1f / fireRate;

            
        }
    }

    void Fire(Transform target)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Mermiye hız verin (örneğin, Rigidbody kullanarak)
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        bulletRb.AddForce((target.transform.position-firePoint.transform.position).normalized*1000f);
    }

}
