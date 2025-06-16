using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public int gridWidth = 13;
    public int gridHeight = 11;
    public float cellSize = 1f;
    public GridNode[,] gridNodes;
    public GridSettings gridSettings;

    private void Awake()
    {
        Instance = this;
    }

    public Vector2Int WorldToGrid(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / cellSize);
        int y = Mathf.RoundToInt(worldPos.z / cellSize);
        return new Vector2Int(x, y);
    }

    public Vector3 GridToWorld(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * cellSize, 0f, gridPos.y * cellSize);
    }

    // Converts world space to grid coordinates
    public Vector2Int WorldToGridIndex(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / gridSettings.CellSize);
        int y = Mathf.RoundToInt(worldPos.z / gridSettings.CellSize);
        return new Vector2Int(x, y);
    }

    public GridNode GetNode(Vector2Int gridPos)
    {
        return gridNodes[gridPos.x, gridPos.y];
    }

    //  New function 1: Place explosion at grid position
    public void PlaceExplosion(Vector2Int pos)
    {
        if (!IsValidIndex(pos)) return;

        GridNode node = GetNode(pos);
        node.HasExplosion = true;

        // Optional: Visual indicator (e.g. spawn explosion effect)
        // Instantiate(explosionPrefab, node.WorldPosition, Quaternion.identity);
    }

    //  New function 2: Clear explosion after duration
    public void ClearExplosion(Vector2Int pos)
    {
        if (!IsValidIndex(pos)) return;

        GridNode node = GetNode(pos);
        node.HasExplosion = false;

        // Optional: Remove explosion visuals
    }

    //  New helper 3: Walkability check for explosions
    public bool IsWalkable(Vector2Int pos)
    {
        if (!IsValidIndex(pos)) return false;

        return gridNodes[pos.x, pos.y].IsWalkable;
    }

    //public bool IsBreakableWall(Vector2Int pos)
    //{
    //    if (!IsValidIndex(pos)) return false;

    //    return gridNodes[pos.x, pos.y].IsBreakable;
    //}

    //  Index boundary check
    private bool IsValidIndex(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < gridSettings.GridWidth &&
               pos.y >= 0 && pos.y < gridSettings.GridHeight;
    }
}
