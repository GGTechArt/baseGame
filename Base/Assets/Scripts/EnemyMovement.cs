using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 1f;

    public float health = 100f;

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

        if (Vector3.Distance(transform.position, target.position) <=0.2F )
        {
            GetNextWaypoint();
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void GetNextWaypoint()
    {
        if(waypointIndex >= Waypoints.point.Length -1) 
        {
            EndPath();
            return;
        }
        
        waypointIndex++;
        target = Waypoints.point[waypointIndex];
    }

    void EndPath()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }

}
