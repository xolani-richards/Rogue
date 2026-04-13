using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    public static Dictionary<Type, object> services = new();
    public static bool Register<T> (T service) { 
        if(services.ContainsKey(typeof(T))) return false;
        services[typeof(T)] = service;
        return true;
    }
    public static T Get<T>() => services.TryGetValue(typeof(T), out var service) ? (T) service: default;
}