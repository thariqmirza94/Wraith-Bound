using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject ghostPrefab;
    public int numberOfGhosts = 10;

    public Vector2Int gridMin;  // bottom-left corner of spawnable area
    public Vector2Int gridMax;  // top-right corner of spawnable area
    [SerializeField] private Ghost ghostController;

    void Start()
    {
        SpawnGhosts();
    }

    void SpawnGhosts()
    {
        for (int i = 0; i < numberOfGhosts; i++)
        {
            Vector2Int spawnPos = GetRandomSpawnPosition();

            Vector3 worldPos = GridManager.Instance.GridToWorld(spawnPos);
            GameObject ghost = Instantiate(ghostPrefab, worldPos, Quaternion.identity);

            // Optional: Initialize ghost's direction
            ghostController.InitializeRandomDirection();
        }
    }

    Vector2Int GetRandomSpawnPosition()
    {
        Vector2Int pos;
        int attempts = 0;

        do
        {
            int x = Random.Range(gridMin.x, gridMax.x + 1);
            int y = Random.Range(gridMin.y, gridMax.y + 1);
            pos = new Vector2Int(x, y);
            attempts++;

            // Make sure spawn location is walkable
            if (CollisionManager.Instance.IsWalkable(pos))
                return pos;

        } while (attempts < 50);  // Prevent infinite loop if map is crowded

        return gridMin;  // fallback
    }
}
