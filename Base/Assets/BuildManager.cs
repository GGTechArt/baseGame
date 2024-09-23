using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
        }

        instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject anotherTurretPrefab;

    private TurretBlueprint turretToBuild;

    public bool CanBuild {  get {return turretToBuild != null;} }

    public void BuildTurretOn(Node node)
    {
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab,node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
