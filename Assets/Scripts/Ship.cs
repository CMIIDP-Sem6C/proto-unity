using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField] private float vesselspeed = 1;
    [SerializeField] private WaypointRoute route;
    [SerializeField] private LineRenderer lineRenderer;

    private bool movingForward = true;
    private int currentWaypointIndex = 0;
    private Transform nextWaypoint;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (sprite)
        {
            sprite.color = route.color;
        }

        if (route != null && route.waypoints.Length > 0)
        {
            nextWaypoint = route.waypoints[currentWaypointIndex];
        }
        if (lineRenderer)
        {
            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(route.color, 0f), new GradientColorKey(route.color, 1f) },
                new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) }
            );
            lineRenderer.colorGradient = gradient;

        }

    }

    void Update()
    {
        if (route == null || route.waypoints.Length == 0) return;

        if (nextWaypoint != null)
        {
            LookAtWaypoint(nextWaypoint);
            MoveTowardsWaypoint(nextWaypoint);

            if (Vector2.Distance(transform.position, nextWaypoint.transform.position) < 0.1f)
            {
                if (movingForward && currentWaypointIndex == route.waypoints.Length - 1)
                {
                    movingForward = false;
                }
                else if (!movingForward && currentWaypointIndex == 0)
                {
                    movingForward = true;
                }

                currentWaypointIndex += movingForward ? 1 : -1;
                nextWaypoint = route.waypoints[currentWaypointIndex];
            }
        }

        DrawPath();
    }

    private void LookAtWaypoint(Transform target)
    {
        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void MoveTowardsWaypoint(Transform target)
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            target.transform.position,
            vesselspeed * Time.deltaTime
        );
    }

    private void DrawPath()
    {
        if (lineRenderer == null || route == null) return;

        // Determine next 3 waypoints
        int pointCount = 0;
        Vector3[] positions = new Vector3[4]; // Ship + next 3 waypoints
        positions[0] = transform.position; // Start from ship

        int index = currentWaypointIndex;
        bool direction = movingForward;

        for (int i = 1; i <= 3; i++)
        {
            if (route.waypoints.Length <= index) break;

            positions[i] = route.waypoints[index].position;
            pointCount = i;

            // Prepare next index
            if (direction && index == route.waypoints.Length - 1)
            {
                direction = false;
            }
            else if (!direction && index == 0)
            {
                direction = true;
            }

            index += direction ? 1 : -1;
        }

        lineRenderer.positionCount = pointCount + 1;
        for (int i = 0; i <= pointCount; i++)
        {
            lineRenderer.SetPosition(i, positions[i]);
        }
    }
}