using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : ServiceInstallerBase<ScenesManager>
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    protected override ScenesManager CreateService()
    {
        ServiceLocator.RegisterService(this);
        return this;
    }
}
