using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BasicTurret : TurretBehaviorBase
{
    [SerializeField] Transform target;
    [SerializeField] public float rotationSpeed;
    [SerializeField] Transform rotationPart;
    AudioManager audioManager;

    public override void Start()
    {
        base.Start();
        audioManager = ServiceLocator.GetService<AudioManager>();
    }

    public override void Update()
    {
        base.Update();

        if (target != null)
        {
            Vector3 direction = target.position - rotationPart.position;
            Quaternion rotation = Quaternion.RotateTowards(rotationPart.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            rotation.x = 0; rotation.z = 0;
            rotationPart.rotation = rotation;
        }
    }

    public override void GetTargets()
    {
        base.GetTargets();

        float shortestDistance = Mathf.Infinity;

        if (targets.Length > 0)
        {
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
        else
        {
            target = null;
        }
    }

    public override void Shoot()
    {
        if (target)
        {
            audioManager.PlayMainSfx("Missile Shoot");

            for (int i = 0; i < firePoints.Count; i++)
            {
                GameObject bulletGO = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
                BulletController bullet = bulletGO.GetComponent<BulletController>();

                if (bulletGO != null)
                {
                    bullet.Seek(target);
                }
            }
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
