using UnityEngine;
using System.Collections.Generic;


public class ServiceInstallerManager : MonoBehaviour
{
    [SerializeField] protected ServiceInstallerBase[] installersToInstantiate;
    protected List<GameObject> installerGO = new List<GameObject>();

    protected virtual void Awake()
    {
        foreach (ServiceInstallerBase installer in installersToInstantiate)
        {
            GameObject installerInstance = Instantiate(installer).gameObject;
            installerInstance.transform.SetParent(transform);
            installerInstance.GetComponent<IServiceInstaller>().InstallService();
            installerGO.Add(installerInstance);
        }

        if (!FindFirstObjectByType<ProjectContextInstaller>())
        {
            Instantiate(Resources.Load<ProjectContextInstaller>("ProjectInstallerManager"));
        }
    }
}
