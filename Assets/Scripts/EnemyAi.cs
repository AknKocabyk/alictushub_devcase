using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;

public class EnemyAi : MonoBehaviour
{
    private Animator Enemyanimator;

    public NavMeshAgent meshAgentEnemy;
    private Transform playerTransform;

    private TextMeshProUGUI deadEnemyText; //Öldürülen düşmanların tutulduğu text

    public float detectionRange = 10f; // Düşmanın sınırı tespit etme menzili
    public float fireRate = 1f; // Mermi atma hızı (saniyede kaç kere atacak)

    private float nextFireTime = 0f; // Bir sonraki mermi atma zamanı

    public GameObject bulletPrefab; // Mermi prefab'ı
    private Transform firePoint; // Mermi spawn noktası

    public int deadEnemy=0;
 

    private void Start()
    {
        deadEnemyText = GameObject.Find("EnemyText").GetComponent<TextMeshProUGUI>();

        GameObject playerObject = GameObject.FindWithTag("Player");//Player objemizi bul
        Enemyanimator = GetComponent<Animator>(); //Düşman animatorunu bul

        firePoint = this.gameObject.transform; //Ateş edilecek noktayı düşman transformuna eşitle
        Vector3 newPosition = new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z); //Merminin atılacağı pozisyonu Vector3 değerine ata
        firePoint.position = newPosition; //Atılacak pozisyonu oluşturduğumuz vector3 değerine eşitle

        if (playerObject != null) //Karakterimizi bulduğumuda boş dönüp döndürmedğini kontro et
        {
            playerTransform = playerObject.transform; //Bulduğumuz playerObjesini transforma eşitle
        }
        else
        {
            Debug.LogError("Player not found with the tag 'Player'."); // Player objesini bulamaazsa hata çıktısı ver
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);//Karakterimizin uzaklığını hesapla

        if (distanceToPlayer <= detectionRange && Time.time >= nextFireTime) //Uzaklık yeterli ve mermi atma sıklığı gelirse
        {
            // Mermi atma fonksiyonunu çağır
            EnemyShoot();
            meshAgentEnemy.Stop();

            // Bir sonraki mermi atma zamanını belirle
            nextFireTime = Time.time + 1f / fireRate;
        }
        else if(distanceToPlayer > detectionRange)
        {
            meshAgentEnemy.Resume();
            meshAgentEnemy.SetDestination(playerTransform.position);
        }

    }

    private void FixedUpdate()
    {
        //meshAgentEnemy.SetDestination(playerTransform.position);
        deadEnemyText.text = PlayerPrefs.GetInt(nameof(deadEnemy)).ToString();
    }

    private void EnemyShoot()
    {
        
        //Enemyanimator.SetBool("ThrowingEnemy",true);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);//Mermi prefabını oluşturduğunuz pozisyon ve rotasyon değerine göre yarat
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boomerang"))
        {
            deadEnemy = PlayerPrefs.GetInt(nameof(deadEnemy));
            deadEnemy++;
            PlayerPrefs.SetInt(nameof(deadEnemy), deadEnemy);
            Destroy(this.gameObject);
        }
    }
}
