using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void RegisterService<T>(T service)
    {
        var type = typeof(T);
        if (!_services.ContainsKey(type))
        {
            _services[type] = service;
        }
    }

    public static T GetService<T>()
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service))
        {
            return (T)service;
        }
        throw new Exception($"Service of type {type} not found.");
    }

    public static void ClearServices()
    {
        _services.Clear();
    }
}
