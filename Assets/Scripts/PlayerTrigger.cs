using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.UI;

public class PlayerTrigger : MonoBehaviour
{
    public int coins = 0; //Topladığımız altınlar
    public TextMeshProUGUI coinText; //Altınları yazdırdığım test

    public Slider healthSlider; //Can barı değişkeni
    public GameObject losePanel; // Kaybetme Paneli
    public float maxHealth = 100f; // Karakterimizin maksimum canı
    private float currentHealth; //Karakterimizin sahip olduğu anlık can

    public Animator playerAnimator; //Karakterimizin animator objesi

    private void Start()
    {
        currentHealth = maxHealth; // Oyuna başladığımızda anlık canımızı maksimum canımıza eşitle

        coins = PlayerPrefs.GetInt(nameof(coins)); //Oyun başladığında Player Prefs ile topladığımız altını çağır oyun başladığında
        UpdateCoinText();
        UpdateHealthBar();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin") //Coin tagına girdiğimizde
        {
            //Debug.Log("Coin Collected...");
            coins++; //Topladığımız altını arttır
            Destroy(other.gameObject); // Altın objesini yok et
            PlayerPrefs.SetInt(nameof(coins), coins); //Player Prefs ile topladığımız altını tut
            UpdateCoinText();
        }

        if (other.gameObject.tag == "EnemyProjectile") //Düşman mermisine temas ettiğğimizde
        {
            currentHealth -= 10; // Canımızı 10 azaltıyorum
            currentHealth = Mathf.Max(currentHealth, 0f); //Anlık canımızı sıfır ile karşılaştırıp yüksek değeri döndür
            UpdateHealthBar();
            Destroy(other.gameObject);//Düşman mermisini yok et
            //Debug.Log("Mermi İsabet Etti...");

            if (currentHealth <= 0) //Canımız 0 veya 0 dan düşük olduğunda
            {
                losePanel.SetActive(true); // Lose paneli aktif et
                playerAnimator.SetBool("isDead", true); //Karakterimizin ölme animasyonu parametresini true yap
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Karakterimiz düşmana çarptığında
        {
            playerAnimator.SetBool("isDead", true);//Karakterimizin ölme animasyonu parametresini true yap
            losePanel.SetActive(true);// Lose paneli aktif et
        }
    }

    void UpdateCoinText()
    {
        coinText.text = coins.ToString(); //Coin Textimi güncelle
    }

    void UpdateHealthBar() 
    {
        healthSlider.value = currentHealth / maxHealth; //Can barımızın değerini değiştir
    }
}
