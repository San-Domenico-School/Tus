using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public float maxDistance = 10f;
    public LayerMask hitLayers = ~0; // Default to everything

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }

    void Update()
    {
        Vector3 startPosition = transform.position;
        Vector3 direction = transform.forward;

        // Perform raycast to detect hit
        if (Physics.Raycast(startPosition, direction, out RaycastHit hit, maxDistance, hitLayers))
        {
            // Hit something, set line end to hit point
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            // No hit, set line end to max distance forward
            lineRenderer.SetPosition(0, startPosition);
            lineRenderer.SetPosition(1, startPosition + direction * maxDistance);
        }
    }
}
