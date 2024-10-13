using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : ServiceInstallerBase<ScenesManager>
{
    public void ChangeScene(string sceneName)
    {
        if (sceneName == null)
        {
            ReloadScene();
        }
        else
        {

            SceneManager.LoadScene(sceneName);
        }
    }

    public void ReloadScene()
    {
        ChangeScene(SceneManager.GetActiveScene().name);
    }

    protected override ScenesManager CreateService()
    {
        ServiceLocator.RegisterService(this);
        return this;
    }
}
