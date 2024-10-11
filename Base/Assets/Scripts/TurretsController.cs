using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretsController : MonoBehaviour, IBuildable
{
    public BuildableItemSO data => turretData;
    public TowerSO turretData;
    TurretUpdatesSO updates;
    TurretStats currentStats;
    int currentUpdate = 0;

    TurretBehaviorBase currentTurret;

    public void Configure(BuildableItemSO data)
    {
        turretData = (TowerSO)data;
        updates = (TurretUpdatesSO)turretData.Updates;
        currentStats = updates.UpdatesList[currentUpdate];

        InstantiateTurret();
    }

    public bool TryUpdate()
    {
        return currentUpdate + 1 < updates.UpdatesList.Count ? true : false;
    }

    void IBuildable.Update()
    {
        currentUpdate++;
        currentStats = updates.UpdatesList[currentUpdate];

        InstantiateTurret();
    }

    public void InstantiateTurret()
    {
        if (currentStats.Prefab != null)
        {
            GameObject newTurret = Instantiate(currentStats.Prefab, transform.position, currentTurret != null ? currentTurret.transform.rotation : Quaternion.identity);

            if (currentTurret)
            {
                Destroy(currentTurret.gameObject);
            }

            currentTurret = newTurret.GetComponent<TurretBehaviorBase>();
            currentTurret.SetStats(currentStats);
        }
    }
}
