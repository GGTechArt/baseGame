using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1f;

    private Transform target;
    private int waypointIndex = 0;

    void Start()
    {
        target = Waypoints.point[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2F)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.point.Length - 1)
        {
            Destroy(gameObject);
            return;
        }

        waypointIndex++;
        target = Waypoints.point[waypointIndex];
    }
}
