using UnityEngine;

public class Spell : MonoBehaviour
{
    public float fuseTime = 2f;
    public int spellRange = 3;
    public GameObject spellPrefab;

    private Vector2Int gridPos;

    void Start()
    {
        gridPos = GridManager.Instance.WorldToGrid(transform.position);
        Invoke(nameof(Explode), fuseTime);
    }

    void Explode()
    {
        CollisionManager.Instance.RemoveBomb(gridPos);
        ActivateSpellAt(gridPos);

        Vector2Int[] directions = Directions.CardinalDirections;

        foreach (Vector2Int dir in directions)
        {
            for (int i = 1; i <= spellRange; i++)
            {
                Vector2Int target = gridPos + dir * i;

                if (CollisionManager.Instance.solidWalls.Contains(target))
                    break;

                ActivateSpellAt(target);

                if (CollisionManager.Instance.breakableWalls.Contains(target))
                    break;
            }
        }

        Destroy(gameObject);
    }

    void ActivateSpellAt(Vector2Int pos)
    {
        Instantiate(spellPrefab, GridManager.Instance.GridToWorld(pos), Quaternion.identity);
        CollisionManager.Instance.AddExplosion(pos);
    }

}
