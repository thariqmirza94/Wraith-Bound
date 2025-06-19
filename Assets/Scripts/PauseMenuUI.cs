using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    public Button playButton;
    public TimerUI timer;

    void Start()
    {
        playButton.onClick.AddListener(GoToMainGame);
    }

    public void GoToMainGame()
    {
        SceneManager.LoadScene("MainGame");
        timer.timerIsRunning = true;
    }
}