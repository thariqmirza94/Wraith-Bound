using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Assign your player here

    public Vector2 pixelResolution = new Vector2(160, 192);  // Your desired screen resolution
    public int pixelsPerUnit = 16;  // Match your sprite import setting

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();

        // Calculate orthographic size automatically
        cam.orthographic = true;
        cam.orthographicSize = (pixelResolution.y / (pixelsPerUnit * 2f));
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        // Follow target while keeping Z the same
        Vector3 newPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = newPos;
    }
}
