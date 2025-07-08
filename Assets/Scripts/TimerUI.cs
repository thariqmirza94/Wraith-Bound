using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    public float totalTime = 120f; // Total countdown time
    private float remainingTime;
    public bool timerIsRunning = true;

    public TextMeshProUGUI timerText;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        remainingTime = totalTime;
        UpdateTimerText();
    }

    void Update()
    {
        if (timerIsRunning && remainingTime > 0f)
        {
            remainingTime -= Time.unscaledDeltaTime; // Use unscaled time so it updates even when timeScale = 0
            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        int seconds = Mathf.Max(0, Mathf.FloorToInt(remainingTime));
        timerText.text = "Time: " + seconds.ToString();
    }

    public void StopTimer()
    {
        timerIsRunning = false;
    }

    public void ResumeTimer()
    {
        timerIsRunning = true;
    }

    public void ResetTimer()
    {
        remainingTime = totalTime;
        UpdateTimerText();
        timerIsRunning = true;
    }

    public float GetRemainingTime()
    {
        return remainingTime;
    }
}
