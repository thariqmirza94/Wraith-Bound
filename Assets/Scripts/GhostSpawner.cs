using System.Collections.Generic;
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
        HashSet<Vector2Int> usedPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < numberOfGhosts; i++)
        {
            Vector2Int spawnPos = GetRandomSpawnPosition(usedPositions);

            Vector3 worldPos = GridManager.Instance.GridToWorld(spawnPos);
            GameObject ghost = Instantiate(ghostPrefab, worldPos, Quaternion.identity);

            Ghost ghostController = ghost.GetComponent<Ghost>();
            ghostController.InitializeRandomDirection();

            usedPositions.Add(spawnPos);
        }
    }

    Vector2Int GetRandomSpawnPosition(HashSet<Vector2Int> usedPositions)
    {
        Vector2Int pos;
        int attempts = 0;

        do
        {
            int x = Random.Range(gridMin.x, gridMax.x + 1);
            int y = Random.Range(gridMin.y, gridMax.y + 1);
            pos = new Vector2Int(x, y);
            attempts++;

            if (CollisionManager.Instance.IsWalkable(pos) && !usedPositions.Contains(pos))
                return pos;

        } while (attempts < 50);

        return gridMin;  // fallback if nothing valid found
    }
}
