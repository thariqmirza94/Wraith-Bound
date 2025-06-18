using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public float timeRemaining = 120f; // Set your timer duration
    public bool timerIsRunning = true;

    public TextMeshProUGUI timerText; // Drag your UI Text object here in Inspector

    void Start()
    {
        UpdateTimerDisplay(timeRemaining);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerIsRunning = false;
                Debug.Log("Time's up!");
                HandleTimeout();
            }

            UpdateTimerDisplay(timeRemaining);
        }
    }

    void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void HandleTimeout()
    {
        // Add logic for time-out
        // e.g., end game, disable player, show Game Over screen
        GameObject.FindWithTag("Player").SetActive(false); // Optional: disable player
    }

    public void StopTimer()
    {
        timerIsRunning = false;
    }
}
