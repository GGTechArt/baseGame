using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissileTurret : TurretBehaviorBase
{
    List<Transform> nearestTargets = new List<Transform>();
    [SerializeField] List<Transform> piecesToRotate = new List<Transform>();
    [SerializeField] float rotationSpeed;

    public override void Update()
    {
        base.Update();

        for (int i = 0; i < nearestTargets.Count; i++)
        {
            if (nearestTargets[i])
            {
                Vector3 direction = nearestTargets[i].position - piecesToRotate[i].position;
                Quaternion rotation = Quaternion.RotateTowards(piecesToRotate[i].rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
                rotation.x = 0; rotation.z = 0;
                piecesToRotate[i].rotation = rotation;
            }
        }
    }

    public override void GetTargets()
    {
        base.GetTargets();
        nearestTargets = GetNearestTargets(targets);
    }

    public override void Shoot()
    {
        for (int i = 0; i < nearestTargets.Count; i++)
        {
            if (nearestTargets[i] != null)
            {
                GameObject bulletGO = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
                BulletController bullet = bulletGO.GetComponent<BulletController>();

                if (bullet != null)
                {
                    bullet.Seek(nearestTargets[i]);
                }
            }
        }
    }

    public List<Transform> GetNearestTargets(Collider[] colliders)
    {
        List<Collider> orderedColliders = colliders
            .OrderBy(collider => Vector3.Distance(collider.transform.position, transform.position))
            .ToList();

        List<Transform> closestTransforms = new List<Transform>();

        for (int i = 0; i < firePoints.Count; i++)
        {
            if (i < orderedColliders.Count)
            {
                closestTransforms.Add(orderedColliders[i].transform);
            }
            else
            {
                closestTransforms.Add(orderedColliders.Count > 0 ? orderedColliders[0].transform : null);
            }
        }

        return closestTransforms;
    }

    //public override void Update()
    //{
    //    base.Update();

    //    for (int i = 0; i < nearestTargets.Count; i++)
    //    {
    //        if (nearestTargets[i])
    //        {
    //            Vector3 direction = nearestTargets[i].position - piecesToRotate[i].position;
    //            Quaternion rotation = Quaternion.RotateTowards(piecesToRotate[i].rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
    //            rotation.x = 0; rotation.z = 0;
    //            piecesToRotate[i].rotation = rotation;
    //        }
    //    }
    //}
    //public override void GetTargets()
    //{
    //    base.GetTargets();
    //    nearestTargets = GetNearestTargets(targets);
    //}
    //public override void Shoot()
    //{
    //    for (int i = 0; i < nearestTargets.Count; i++)
    //    {
    //        GameObject bulletGO = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation);
    //        BulletController bullet = bulletGO.GetComponent<BulletController>();

    //        if (bulletGO != null)
    //        {
    //            bullet.Seek(nearestTargets[i]);
    //        }
    //    }
    //}

    //public List<Transform> GetNearestTargets(Collider[] colliders)
    //{
    //    List<Collider> orderedColliders = colliders
    //        .OrderBy(collider => Vector3.Distance(collider.transform.position, transform.position))
    //        .ToList();

    //    List<Transform> closestTransforms = orderedColliders
    //        .Take(firePoints.Count)
    //        .Select(collider => collider.transform)
    //        .ToList();

    //    return closestTransforms;
    //}


}
