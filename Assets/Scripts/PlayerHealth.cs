using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 5;
    private int currentLives;

    public TextMeshProUGUI healthText; // Drag your UI text object here in Inspector

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
        currentLives--;

        if (currentLives <= 0)
        {
            currentLives = 0;
            Debug.Log("Game Over");
            gameObject.SetActive(false);
            SceneManager.LoadScene("LoseScreen");
        }

        UpdateHealthText();
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Lives: " + currentLives.ToString();
        }
    }
}
