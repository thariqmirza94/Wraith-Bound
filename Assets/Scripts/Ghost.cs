using System.Collections;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public float moveCooldown = 0.25f; // slower than player
    private float moveTimer;

    private Vector2Int currentGridPos;
    private Vector2Int direction;

    private GridManager grid;
    private CollisionManager collisionManager;

    public float moveDuration = 0.2f;

    private Vector2Int currentPos;
    private bool isMoving = false;
    private Vector2Int initialDirection = Vector2Int.zero;

    void Start()
    {
        currentPos = GridManager.Instance.WorldToGrid(transform.position);
        transform.position = GridManager.Instance.GridToWorld(currentPos);

        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (!isMoving)
            {
                Vector2Int dir = (initialDirection == Vector2Int.zero) ? GetRandomDirection() : initialDirection;
                initialDirection = Vector2Int.zero;
                Vector2Int targetPos = currentPos + dir;

                if (CollisionManager.Instance.IsWalkable(targetPos))
                {
                    yield return StartCoroutine(MoveTo(targetPos));
                }
            }

            yield return new WaitForSeconds(moveCooldown);
        }
    }

    Vector2Int GetRandomDirection()
    {
        Vector2Int[] directions = Directions.CardinalDirections;
        int index = Random.Range(0, directions.Length);
        return directions[index];
    }

    public void InitializeRandomDirection()
    {
        Vector2Int[] directions = Directions.CardinalDirections;
        int index = Random.Range(0, directions.Length);
        initialDirection = directions[index];
    }

    IEnumerator MoveTo(Vector2Int targetPos)
    {
        isMoving = true;
        Vector3 start = transform.position;
        Vector3 end = GridManager.Instance.GridToWorld(targetPos);
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / moveDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
        currentPos = targetPos;
        isMoving = false;
    }

    //spawn location
    //+-1 up/down or left/right

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Spell")
        {
            Die();
        }

    }
}
