using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveCooldown = 0.15f;
    public GameObject spellPrefab;

    private float moveTimer;
    public bool hasKey = false;
    //health variable


    private void OnTriggerEnter(Collider other)
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

        if (Input.GetKeyDown(KeyCode.W)) dir = Vector2Int.up;
        if (Input.GetKeyDown(KeyCode.S)) dir = Vector2Int.down;
        if (Input.GetKeyDown(KeyCode.A)) dir = Vector2Int.left;
        if (Input.GetKeyDown(KeyCode.D)) dir = Vector2Int.right;

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
            PlaceBomb();
        }
    }

    void PlaceBomb()
    {
        Vector2Int pos = GridManager.Instance.WorldToGrid(transform.position);
        if (!CollisionManager.Instance.bombs.Contains(pos))
        {
            Instantiate(spellPrefab, GridManager.Instance.GridToWorld(pos), Quaternion.identity);
            CollisionManager.Instance.AddBomb(pos);
        }
    }
}