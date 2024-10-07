using UnityEngine;

public abstract class ServiceInstallerBase : MonoBehaviour, IServiceInstaller
{
    public abstract void InstallService();
}


public abstract class ServiceInstallerBase<T> : ServiceInstallerBase where T : class
{
    protected abstract T CreateService();

    public override void InstallService()
    {
        T service = CreateService();
        ServiceLocator.RegisterService(service);
    }
}