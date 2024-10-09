using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneContextInstaller : ServiceInstallerManager
{
    [SerializeField] protected ServiceInstallerBase[] installers;
    protected override void Awake()
    {
        foreach (var installer in installers)
        {
            installer.GetComponent<IServiceInstaller>().InstallService();
        }

        base.Awake();
    }
}
