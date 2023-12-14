using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
     public EnemyAi enemyAi;
    public void RestartGame()
    {
        // Aktif sahneyi tekrar yükleyerek oyunu başlat
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        enemyAi.deadEnemy = 0;
        PlayerPrefs.DeleteKey("deadEnemy");
        Time.timeScale = 1;
    }
}
