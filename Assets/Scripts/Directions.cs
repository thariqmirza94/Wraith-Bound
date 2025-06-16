using UnityEngine;

public static class Directions
{
    public static readonly Vector2Int[] CardinalDirections =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    //public static readonly Vector2Int[] DiagonalDirections =
    //{
    //    new Vector2Int(1, 1),
    //    new Vector2Int(1, -1),
    //    new Vector2Int(-1, 1),
    //    new Vector2Int(-1, -1)
    //};

    // Optional helper: reverse direction
    public static Vector2Int Reverse(Vector2Int dir)
    {
        return -dir;
    }
}
