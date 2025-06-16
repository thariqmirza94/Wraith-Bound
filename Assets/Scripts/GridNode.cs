using UnityEngine;

public class GridNode
{
    public Vector2Int GridPosition;  // grid coordinates (x, y)
    public Vector3 WorldPosition;    // world position for rendering/placement

    public bool IsWalkable = true;   // walkable by player or not
    public bool HasBuilding = false; // is there a building occupying this cell?
    public bool HasExplosion = false; // temporarily true when explosion happens


    // Optional: Could track what object is occupying the tile
    public GameObject OccupyingObject;

    public GridNode(Vector2Int gridPos, Vector3 worldPos)
    {
        GridPosition = gridPos;
        WorldPosition = worldPos;
    }
}
