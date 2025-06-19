using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public Button playButton;

    void Start()
    {
        playButton.onClick.AddListener(GoToMainMenu);
    }

   public void GoToMainMenu()
    {
        SceneManager.LoadScene("LoadingScreen");
    }
}
