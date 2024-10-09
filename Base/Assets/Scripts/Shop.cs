using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;

    BuildManager buildManager;
    Node node;

    private void Start()
    {
        buildManager = BuildManager.instance;
        node = Node.instance;
    }
    public void SelectStandarTurret()
    {
        Debug.Log("Standar turret");
        buildManager.SelectTurretToBuild(standardTurret);
    }

    public void SelectMissileLauncher()
    {
        Debug.Log("Missile turret");
        buildManager.SelectTurretToBuild(missileLauncher);
    }
}
