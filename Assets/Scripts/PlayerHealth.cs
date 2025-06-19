using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 7;
    private int currentLives;
    public AudioSource losingSound;
    public TextMeshProUGUI healthText; // Drag your UI text object here in Inspector
    public float invincibilityDuration = 1f; // seconds
    private float lastDamageTime = -Mathf.Infinity;


    void Start()
    {
        currentLives = maxLives;
        UpdateHealthText();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ghost"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (Time.time - lastDamageTime < invincibilityDuration)
            return; // Still invincible

        lastDamageTime = Time.time;

        currentLives--;

        if (currentLives <= 0)
        {
            currentLives = 0;
            Debug.Log("Game Over");
            StartCoroutine(HandleGameOver());
        }

        UpdateHealthText();
    }


    IEnumerator HandleGameOver()
    {
        losingSound.Play();
        yield return new WaitForSeconds(losingSound.clip.length); // Wait until SFX finishes
        SceneManager.LoadScene("LoseScreen");
    }


    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Lives: " + currentLives.ToString();
        }
    }
}
