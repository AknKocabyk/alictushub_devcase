using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Düşmanın prefab'ı
    public float spawnInterval = 2f; // Düşman spawn aralığı (saniye)
    public float spawnDistance = 10f; // Kameranın dışındaki spawn mesafesi

    void Start()
    {
        // Belirli aralıklarla düşman spawn etme işlemini başlat
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Düşmanın spawn edileceği rastgele bir konum belirle
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Düşmanı spawn et
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomSpawnPosition()
    {
        // Kameranın dışındaki bir rastgele konum belirle
        Camera mainCamera = Camera.main;
        float cameraDistance = mainCamera.transform.position.y;

        float spawnX = Random.Range(mainCamera.ViewportToWorldPoint(new Vector3(0, 0, cameraDistance)).x - spawnDistance,
                                    mainCamera.ViewportToWorldPoint(new Vector3(1, 0, cameraDistance)).x + spawnDistance);
        float spawnZ = Random.Range(mainCamera.ViewportToWorldPoint(new Vector3(0, 0, cameraDistance)).z - spawnDistance,
                                    mainCamera.ViewportToWorldPoint(new Vector3(0, 1, cameraDistance)).z + spawnDistance);

        return new Vector3(spawnX, 0f, spawnZ);
    }
}
