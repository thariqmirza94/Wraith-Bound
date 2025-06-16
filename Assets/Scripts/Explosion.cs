using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int explosionRange = 2;
    public float explosionDuration = 0.3f;
    public Vector2Int origin;

    private GridManager gridManager;
    private CollisionManager collisionManager;

    public void Initialize(Vector2Int startPos)
    {
        origin = startPos;
        gridManager = GridManager.Instance;
        collisionManager = FindObjectOfType<CollisionManager>();

        Explode();
    }

    private void Explode()
    {
        // Explode in 4 directions (center + cardinal)
        ExplodeAt(origin); // center first

        Vector2Int[] directions = Directions.CardinalDirections;

        foreach (var dir in directions)
        {
            for (int i = 1; i <= explosionRange; i++)
            {
                Vector2Int targetPos = origin + dir * i;

                if (!collisionManager.IsValidIndex(targetPos))
                    break;

                // If solid wall ? stop explosion
                if (!collisionManager.IsWalkable(targetPos))
                    break;

                ExplodeAt(targetPos);

                // If breakable wall ? destroy and stop propagation
                //if (gridManager.IsBreakableWall(targetPos))
                //{
                //    gridManager.DestroyBreakableWall(targetPos);
                //    break;
                //}
            }
        }

        Destroy(gameObject, explosionDuration);
    }

    private void ExplodeAt(Vector2Int gridPos)
    {
        // Spawn visual explosion prefab here (optional):
        // Instantiate(explosionEffectPrefab, gridManager.GridToWorld(gridPos), Quaternion.identity);

        // Damage enemy/ghost
        Ghost ghost = FindGhostAt(gridPos);
        if (ghost != null)
        {
            ghost.Die();
        }
    }

    private Ghost FindGhostAt(Vector2Int gridPos)
    {
        Vector3 worldPos = gridManager.GridToWorld(gridPos);
        Collider2D[] hits = Physics2D.OverlapCircleAll(worldPos, 0.2f);

        foreach (var hit in hits)
        {
            Ghost ghost = hit.GetComponent<Ghost>();
            if (ghost != null)
                return ghost;
        }
        return null;
    }
}
