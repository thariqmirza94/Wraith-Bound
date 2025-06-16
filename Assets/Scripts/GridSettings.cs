using UnityEngine;

[CreateAssetMenu(fileName = "GridSettings", menuName = "Game/Grid Settings")]
public class GridSettings : ScriptableObject
{
    [Header("Grid Dimensions")]
    public int GridWidth = 13;
    public int GridHeight = 11;

    [Header("Cell Size")]
    public float CellSize = 1f;

    [Header("Plane Settings")]
    public bool UseXZPlane = true;  // if you're doing top-down (XZ), set to true

    // (Optional) Add any global settings for building placement or obstacles later
}