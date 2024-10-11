using UnityEngine;
using System.Collections.Generic;


public class ServiceInstallerManager : MonoBehaviour
{
    [SerializeField] protected ServiceInstallerBase[] installersToInstantiate;
    [SerializeField] protected ServiceInstallerBase[] installers;
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

        foreach (ServiceInstallerBase installer in installers)
        {
            installer.GetComponent<IServiceInstaller>().InstallService();
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
