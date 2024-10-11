using TMPro;
using Unity.Burst.CompilerServices;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1f;
    public float rotationSpeed = 1f;

    private Transform target;
    private Vector3 targetPosition;
    private int waypointIndex = -1;

    NavMeshAgent agent;

    [SerializeField] float avoidDistance;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = Waypoints.point[0];

        GetNextWaypoint();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= 1.5f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.point.Length - 1)
        {
            //Destroy(gameObject);
            return;
        }

        waypointIndex++;
        target = Waypoints.point[waypointIndex];
        NavMeshHit hit;
        targetPosition = new Vector3(Random.Range(target.position.x - avoidDistance, target.position.x + avoidDistance), transform.position.y,
            Random.Range(target.position.z - avoidDistance, target.position.z + avoidDistance));
        NavMesh.SamplePosition(targetPosition, out hit, Mathf.Infinity, NavMesh.AllAreas);
        targetPosition = hit.position;
        agent.SetDestination(targetPosition);
    }
}
