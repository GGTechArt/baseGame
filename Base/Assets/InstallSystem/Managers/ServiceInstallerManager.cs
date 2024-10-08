using UnityEngine;
using System.Collections.Generic;


public class ServiceInstallerManager : MonoBehaviour
{
    [SerializeField] protected ServiceInstallerBase[] installers;
    [SerializeField] protected ServiceInstallerBase[] installersToInstantiate;
    protected List<GameObject> installerGO = new List<GameObject>();

    protected virtual void Awake()
    {
        foreach (ServiceInstallerBase installer in installers)
        {
            GameObject installerInstance = Instantiate(installer).gameObject;
            installerInstance.transform.SetParent(transform);
            installerInstance.GetComponent<IServiceInstaller>().InstallService();
            installerGO.Add(installerInstance);
        }

        foreach (ServiceInstallerBase installer in installersToInstantiate)
        {
            installer.transform.SetParent(transform);
            installer.GetComponent<IServiceInstaller>().InstallService();
            installerGO.Add(installer.gameObject);
        }

        if (!FindFirstObjectByType<ProjectContextInstaller>())
        {
            ProjectContextInstaller[] installers = Resources.FindObjectsOfTypeAll<ProjectContextInstaller>();

            if (installers.Length > 0)
            {
                Instantiate(installers[0]);
            }
        }
    }
}
