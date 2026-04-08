
using UnityEngine;

public class Ship : MonoBehaviour
{
    [SerializeField]
    private Waypoint[] waypoints;
    private Waypoint currentWaypoint;
    private Waypoint nextWaypoint;
    private SpriteRenderer sprite;
    [SerializeField]
    private int speed = 5;
    private bool facingWaypoint = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (sprite)
        {
            sprite.color = Color.red;
        }
        if (waypoints.Length > 0)
        {
            nextWaypoint = waypoints[0];
            if (nextWaypoint)
            {
                sprite.color = Color.green;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length > 0)
        {
            nextWaypoint = waypoints[0];
            if (nextWaypoint)
            {
                sprite.color = Color.green;
            }
        }

        if (nextWaypoint)
        {
            if (!facingWaypoint) LookAtWaypoint(nextWaypoint);

        }
    }

    private void LookAtWaypoint(Waypoint target)
    {
        Vector2 direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        facingWaypoint = true;
    }

    private void MoveTowardsWaypoint(Waypoint target)
    {

    }
}

