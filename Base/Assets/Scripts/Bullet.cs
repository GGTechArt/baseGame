using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;

    public void Seek (Transform _target)
    {
        target = _target;
    }


    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisframe = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisframe)
        {
            //HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisframe, Space.World);
    }

    void Damage (Transform enemy)
    {
        //Enemy e = enemy.GetComponent<Enemy>();

    }
}
