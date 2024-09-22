using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void StandarShopTurret()
    {
        buildManager.SetTurretTobuild(buildManager.standardTurretPrefab);
    }

    public void OtherShopTurret()
    {
        buildManager.SetTurretTobuild(buildManager.anotherTurretPrefab);
    }
}
