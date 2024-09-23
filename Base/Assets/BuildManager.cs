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

    public bool HasQuanta { get { return PlayerStats.quanta >= turretToBuild.cost; } }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.quanta < turretToBuild.cost)
        {
            Debug.Log("No enough money to build that!");
            return;
        }

        PlayerStats.quanta -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab,node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Turret build! Money left: " + PlayerStats.quanta);
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        turretToBuild = turret;
    }
}
