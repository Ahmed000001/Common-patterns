using System;
using System.Collections.Generic;

public class ServiceLocator : Singleton<ServiceLocator>
{
    private IDictionary<object, object> _services;

    public override void Awake()
    {
        base.Awake();
        _services = new Dictionary<object, object>();

    }

    public void RegisterService(AService aService)
    {
        _services.Add(aService.GetType(),aService);
    }

    public T GetService<T>()
    {
        try
        {
            return (T)_services[typeof(T)];
        }
        catch
        {
            throw new ApplicationException("The requested service is not found.");
        }
    }
}