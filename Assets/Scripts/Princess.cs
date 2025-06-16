using UnityEngine;
using UnityEngine.SceneManagement;

public class Princess : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    public bool isFree = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (controller.hasKey)
            {
                isFree = true;
                Debug.Log("Princess is free");
                //some type of indicator saying that the player wins
                //a short wait that last a couple of secs?
                SceneManager.LoadScene("LoadingScreen");
            }
        }
    }
}
