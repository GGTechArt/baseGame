using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BasicTurret : TurretBehaviorBase
{
    [SerializeField] Transform lastTarget;
    [SerializeField] Transform target;
    [SerializeField] public float rotationSpeed;
    [SerializeField] Transform rotationPart;
    AudioManager audioManager;
    [SerializeField] float returnCooldown;
    [SerializeField] float returnCooldownTime;
    public override void Start()
    {
        base.Start();
        audioManager = ServiceLocator.GetService<AudioManager>();
    }

    public override void Update()
    {
        base.Update();

        // Dirección hacia la izquierda en el espacio mundial
        Vector3 leftDirection = transform.position - Vector3.right * 5;

        if (target)
        {
            RotateTowards(target.position);
            returnCooldownTime = 0;
            // Reiniciar el cooldown si hay un target
        }
        else
        {
            // Manejar el cooldown solo si se ha perdido un target
            if (target != lastTarget && !target)
            {
                // Solo reinicia el cooldown si ha cambiado el objetivo
                returnCooldownTime = returnCooldown; // Reiniciar el cooldown
            }

            // Si no hay target, manejar el cooldown
            if (returnCooldownTime > 0)
            {
                returnCooldownTime -= Time.deltaTime; // Reducir el cooldown
            }
            else
            {
                // Al llegar a 0 el cooldown, rotar hacia la izquierda
                RotateTowards(leftDirection);
            }
        }

        lastTarget = target; // Actualizar el último target
    }

    // Método para manejar la rotación
    private void RotateTowards(Vector3 direction)
    {
        // Calcular la dirección hacia la que se debe rotar
        Quaternion targetDirection = Quaternion.LookRotation(direction - rotationPart.position);

        // Rotar hacia la dirección objetivo con la velocidad especificada
        Quaternion rotation = Quaternion.RotateTowards(rotationPart.rotation, targetDirection, rotationSpeed * Time.deltaTime);

        rotation.x = 0; // Mantener en el eje Y
        rotation.z = 0; // Mantener en el eje Y
        rotationPart.rotation = rotation;
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
