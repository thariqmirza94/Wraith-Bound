using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveCooldown = 0.15f;
    public GameObject spellPrefab;
    [SerializeField] private Animator animator;
    private float moveTimer;
    public bool hasKey = false;
    //health variable


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
            Debug.Log("Key collected!");
        }
        if (other.CompareTag("Princess"))
        {
            if (hasKey)
            {
                Debug.Log("You saved the princess!");
                // trigger win screen or victory state
            }
            else
            {
                Debug.Log("You need a key to save the princess.");
            }
        }
    }

    void Update()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer < moveCooldown) return;

        Vector2Int dir = Vector2Int.zero;

        if (Input.GetKeyDown(KeyCode.W)) 
        {
            dir = Vector2Int.up; //why moving z axis and not y?
        }

        if (Input.GetKeyDown(KeyCode.S)) 
        {
            dir = Vector2Int.down; //why moving z axis and not y?
        }

        if (Input.GetKeyDown(KeyCode.A)) 
        { 
            dir = Vector2Int.left; 
        }

        if (Input.GetKeyDown(KeyCode.D)) 
        { 
            dir = Vector2Int.right; 
        }

        if (dir != Vector2Int.zero)
        {
            Vector2Int currentPos = GridManager.Instance.WorldToGrid(transform.position);
            Vector2Int targetPos = currentPos + dir;

            if (CollisionManager.Instance.IsWalkable(targetPos))
            {
                transform.position = GridManager.Instance.GridToWorld(targetPos);
                moveTimer = 0f;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetAttack();
            PlaceSpell();
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
       Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            SetWalkingTrue();
        }
        else
        {
            SetWalkingFalse();
        }
    }

    void PlaceSpell()
    {
        Vector2Int pos = GridManager.Instance.WorldToGrid(transform.position);
        if (!CollisionManager.Instance.bombs.Contains(pos))
        {
            Instantiate(spellPrefab, GridManager.Instance.GridToWorld(pos), Quaternion.identity);
            CollisionManager.Instance.AddBomb(pos);
        }
    }

    void SetWalkingTrue()
    {
        animator.SetBool("IsWalking", true);
    }

    void SetWalkingFalse()
    {
        animator.SetBool("IsWalking", false);
    }

    void SetAttack()
    {
        animator.SetTrigger("Attack");
    }

}