using UnityEngine;

[System.Serializable]
public class WaypointRoute : MonoBehaviour
{
    public Transform[] waypoints;
    public Color color;

    void Start()
    {
        ColorizeWaypoints();
    }

    // Call this if waypoints are assigned at runtime
    public void UpdateColor()
    {
        ColorizeWaypoints();
    }

    private void ColorizeWaypoints()
    {
        if (waypoints == null) return;

        foreach (Transform waypoint in waypoints)
        {
            if (waypoint != null)
            {
                var renderer = waypoint.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    renderer.color = color;
                }
            }
        }
    }
}