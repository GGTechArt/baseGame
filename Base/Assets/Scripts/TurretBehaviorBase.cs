using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretBehaviorBase : MonoBehaviour
{
    protected TurretStats currentStats;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected List<Transform> firePoints;
    [SerializeField] protected Collider[] targets;
    [SerializeField] protected LayerMask targetLayer;
    [SerializeField] protected float range;
    protected float fireCountdown = 0f;

    public virtual void Start()
    {
        InvokeRepeating("GetTargets", 0f, 0.5f);
    }

    public virtual void Update()
    {
        if (fireCountdown > 0)
        {
            fireCountdown -= Time.deltaTime;
        }

        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown += currentStats.FireRate;
        }
    }

    public virtual void GetTargets()
    {
        targets = Physics.OverlapSphere(transform.position, range, targetLayer);
    }
    public abstract void Shoot();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void SetStats(TurretStats stats)
    {
        currentStats = stats;
        fireCountdown = 0;
    }
}
