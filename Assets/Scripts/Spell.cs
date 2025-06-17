using UnityEngine;

public class Spell : MonoBehaviour
{
    public float fuseTime = 2f;                  // Time before explosion
    public int spellRange = 2;                   // How far flames reach
    public GameObject explosionPrefab;           // Visual-only explosion prefab (NOT the Spell itself)

    private Vector2Int gridPos;

    void Start()
    {
        if (explosionPrefab == null)
        {
            Debug.LogError("Explosion prefab not assigned on: " + gameObject.name);
            return;
        }
        // Convert world position to grid coordinates
        gridPos = GridManager.Instance.WorldToGrid(transform.position);

        // Wait fuseTime seconds, then explode
        Invoke(nameof(Explode), fuseTime);
    }

    void Explode()
    {
        // Clear the bomb from the collision map
        CollisionManager.Instance.RemoveBomb(gridPos);

        // Spawn center explosion
        ActivateExplosionAt(gridPos);

        // Spread in all 4 cardinal directions
        Vector2Int[] directions = Directions.CardinalDirections;

        foreach (Vector2Int dir in directions)
        {
            for (int i = 1; i <= spellRange; i++)
            {
                Vector2Int target = gridPos + dir * i;

                // Stop if blocked by solid wall
                if (CollisionManager.Instance.solidWalls.Contains(target))
                    break;

                ActivateExplosionAt(target);

                // Stop flame if hitting breakable wall
                if (CollisionManager.Instance.breakableWalls.Contains(target))
                    break;
            }
        }

        // Destroy this bomb object
        Destroy(gameObject);
    }

    void ActivateExplosionAt(Vector2Int pos)
    {
        Vector3 worldPos = GridManager.Instance.GridToWorld(pos);

        // Spawn visual
        Instantiate(explosionPrefab, worldPos, Quaternion.identity);

        // Logic for collision system
        CollisionManager.Instance.AddExplosion(pos);

        // Check for ghosts in explosion radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(worldPos, 1.5f); // 1.5 = radius (tweak as needed)

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Ghost"))
            {
                Destroy(hit.gameObject); // Destroy ghost!
                Debug.Log("Ghost dead");
            }
        }
    }

}

