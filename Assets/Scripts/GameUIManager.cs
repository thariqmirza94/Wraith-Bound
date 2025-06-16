using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public HealthBarUI healthBar;
    public TimerUI timerUI;
    public KeyCountUI keyCountUI;
    public PauseMenuUI pauseMenu;

    public void UpdateHealth(int current, int max)
    {
        healthBar.SetHealth(current, max);
    }

    //public void UpdateKeyCount(int keys)
    //{
    //    keyCountUI.SetKeys(keys);
    //}

    public void UpdateTimer(float timeRemaining)
    {
        timerUI.SetTime(timeRemaining);
    }

    public void ShowPauseMenu(bool show)
    {
        pauseMenu.gameObject.SetActive(show);
    }
}
