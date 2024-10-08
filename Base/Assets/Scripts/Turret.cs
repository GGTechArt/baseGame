using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]
    public float range = 10f;
    public float rotationSpeed;

    [Header("Shoot")]
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [SerializeField] LayerMask targetLayer;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void UpdateTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, range, targetLayer);
        float shortestDistance = Mathf.Infinity;

        foreach (Collider item in targets)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                target = item.transform;
            }
        }
    }

    private void Update()
    {
        if (fireCountdown > 0)
        {
            fireCountdown -= Time.deltaTime;
        }

        if (target == null)
        {
            return;
        }

        Vector3 direction = target.position - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown += fireRate;
        }
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        BulletController bullet = bulletGO.GetComponent<BulletController>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
