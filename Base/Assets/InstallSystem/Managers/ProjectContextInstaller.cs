using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectContextInstaller : ServiceInstallerManager
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
