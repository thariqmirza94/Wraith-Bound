using UnityEngine;
using UnityEngine.SceneManagement;

public class Princess : MonoBehaviour
{
    [SerializeField] private PlayerController controller;
    public bool isFree = false;
    public Animator animator;
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
                animator.SetBool("IsWalking", true);
                Debug.Log("Princess is free");
                SceneManager.LoadScene("LoadingScreen");
            }
        }
    }
}
