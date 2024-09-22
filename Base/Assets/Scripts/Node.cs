using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;

    public RadialMenu radialMenu;
    public bool activateMenu;

    private GameObject turret;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;


    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    private void OnMouseDown()
    {
        if(activateMenu == false)
        {
            radialMenu.Open();
            activateMenu = true;
        }

        if(turret != null) 
        {
            Debug.Log("");
            return;
        }

        if (buildManager.GetTurretToBuild() == null)
            return;
 
        //Build a turret
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);

    }
    //Estudiar y diseñar el input de VR para este llamado.
    void OnMouseEnter()
    {
        if (buildManager.GetTurretToBuild() == null)
            return;

        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
