using System.Collections.Generic;
using UnityEngine;

public class Prediction : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform arrowSpawnPoint;    
    public float arrowSpeed = 10f;
    public int maxReflectionCount = 5;
    public float maxStepDistance = 200f;
    public LayerMask collisionLayers;

    private void Update()
    {
        DrawTrajectory();
    }

    private void DrawTrajectory()
    {
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, arrowSpawnPoint.position);

        Vector3 direction = arrowSpawnPoint.right;
        Vector3 currentPosition = arrowSpawnPoint.position;

        List<Vector3> positions = new List<Vector3> { currentPosition };

        for (int i = 0; i < maxReflectionCount; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, maxStepDistance, collisionLayers);

            if (hit.collider != null)
            {
                currentPosition = hit.point;
                positions.Add(currentPosition);
                break; // Stop at the first collision
            }
            else
            {
                currentPosition += direction * maxStepDistance;
                positions.Add(currentPosition);
            }
        }

        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }
}
