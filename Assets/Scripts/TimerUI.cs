
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;

    public void SetTime(float time)
    {
        // Clamp so we don't show negative time
        time = Mathf.Max(0, time);

        // Convert to minutes and seconds
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        // Update UI text (format: MM:SS)
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}