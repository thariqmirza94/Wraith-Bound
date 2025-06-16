using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public static CollisionManager Instance;

    private void Awake() { Instance = this; }

    public HashSet<Vector2Int> solidWalls = new();
    public HashSet<Vector2Int> breakableWalls = new();
    public HashSet<Vector2Int> bombs = new();
    public HashSet<Vector2Int> explosions = new();
    public HashSet<Vector2Int> keys = new();
    public Vector2Int princessPosition;

    [SerializeField] private GridManager gridManager;

    public bool IsValidIndex(Vector2Int pos)
    {
        return pos.x >= 0 &&
               pos.x < gridManager.gridSettings.GridWidth &&
               pos.y >= 0 &&
               pos.y < gridManager.gridSettings.GridHeight;
    }

    public enum CellContent
    {
        Empty,
        SolidWall,
        BreakableWall,
        Bomb,
        Explosion,
        Key,
        Princess,
        Player,
        Enemy
    }


    public bool IsWalkable(Vector2Int pos)
    {
        return !(solidWalls.Contains(pos) || breakableWalls.Contains(pos) || bombs.Contains(pos));
    }

    public bool IsExploding(Vector2Int pos)
    {
        return explosions.Contains(pos);
    }

    public void AddBomb(Vector2Int pos) => bombs.Add(pos);
    public void RemoveBomb(Vector2Int pos) => bombs.Remove(pos);

    public void AddExplosion(Vector2Int pos) => explosions.Add(pos);
    public void ClearExplosions() => explosions.Clear();
}

