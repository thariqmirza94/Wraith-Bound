using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float moveCooldown = 0.25f; // slower than player
    private float moveTimer;

    private Vector2Int currentGridPos;
    private Vector2Int direction;

    private GridManager grid;
    private CollisionManager collisionManager;

    [System.Obsolete]
    private void Start()
    {
        grid = GridManager.Instance;
        collisionManager = FindObjectOfType<CollisionManager>();

        currentGridPos = grid.WorldToGrid(transform.position);
        PickRandomDirection();
    }

    private void Update()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer < moveCooldown)
            return;

        Vector2Int targetPos = currentGridPos + direction;

        if (collisionManager.IsWalkable(targetPos))
        {
            currentGridPos = targetPos;
            transform.position = grid.GridToWorld(currentGridPos);
        }
        else
        {
            PickRandomDirection();
        }

        moveTimer = 0f;
    }

    private void PickRandomDirection() 
    {
        Vector2Int[] directions = Directions.CardinalDirections;
        direction = directions[Random.Range(0, directions.Length)];
        //for loop- goes to +1 -> goes back to spawning position -> goes to -1 
        //put a collider during the path
    }
    
    //spawn location
    //+-1 up/down or left/right

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Spell")
        {
            Die();
        }

    }
}
