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
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisframe, Space.World);
    }

    void HitTarget()
    {
        //Aca se pueden asignar las particulas
        //GameObject effectIns = (GameObject)Instantiate(//crear el game object impactEffect, transform.position, transform.rotation);
        //Destroy(effectIns, 2f);

        Destroy(target.gameObject);
        Destroy(gameObject);
    }
}
