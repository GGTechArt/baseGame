using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BasicTurret : TurretBehaviorBase
{
    [SerializeField] Transform target;
    [SerializeField] public float rotationSpeed;

    public override void Update()
    {
        base.Update();

        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        }
    }

    public override void GetTargets()
    {
        base.GetTargets();

        float shortestDistance = Mathf.Infinity;

        foreach (var item in targets)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                target = item.transform;
            }
        }
    }

    public override void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoints[0].position, firePoints[0].rotation);
        BulletController bullet = bulletGO.GetComponent<BulletController>();

        if (bulletGO != null)
        {
            bullet.Seek(target);
        }
    }

    //public override void Shoot()
    //{
    //    for (int i = 0; i < targets.Length; i++)
    //    {
    //        if (i > firePoints.Count - 1)
    //        {
    //            break;
    //        }

    //        GameObject bulletGO = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
    //        BulletController bullet = bulletGO.GetComponent<BulletController>();

    //        if (bullet != null)
    //        {
    //            bullet.Seek(targets[i].transform);
    //        }
    //    }
    //}
}
