using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseScreen : MonoBehaviour
{
    public Button playButton;

    void Start()
    {
        playButton.onClick.AddListener(GoToMainMenu);
    }

    void GoToMainMenu()
    {
        SceneManager.LoadScene("LoadingScreen");
    }
}
