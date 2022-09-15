using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

    public static void RegisterService<T>(T service, bool overwrite = false) where T : class, IService
    {
        Type type = typeof(T);
        if (_services.ContainsKey(type))
        {
            if (overwrite)
            {
                UnregisterService<T>();
            }
            else
            {
                UnityEngine.Debug.LogError("Tried to add an already existing service to the service locator: " +
                                           type.Name);
                return;
            }
        }

        _services.Add(type, service);
    }

    public static void RegisterService<T>() where T : class, IService, new()
    {
        RegisterService(new T());
    }

    public static bool HasService<T>() where T : class, IService
    {
        return _services.ContainsKey(typeof(T));
    }

    public static T GetService<T>() where T : class, IService
    {
        if (!_services.TryGetValue(typeof(T), out IService service))
        {
            UnityEngine.Debug.LogError("Tried to get a non registered service from the service locator: " +
                                       typeof(T).Name);
            return default;
        }

        return (T)service;
    }

    public static void UnregisterService<T>()
    {
        Type type = typeof(T);
        if (_services.ContainsKey(type))
        {
            _services[type].Clear();
            _services.Remove(type);
        }
    }

    public static void UnregisterAll()
    {
        foreach (KeyValuePair<Type, IService> service in _services)
        {
            try
            {
                service.Value.Clear();
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }

        _services.Clear();
    }
}
