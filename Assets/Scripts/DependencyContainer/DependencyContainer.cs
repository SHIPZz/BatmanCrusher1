using System;
using System.Collections.Generic;

public class DependencyContainer
{
    private static Dictionary<Type, object> _values = new Dictionary<Type, object>();

    public static void Register<T>(T type) =>
        _values[typeof(T)] = type;

    public static T Get<T>() =>
         (T)_values[typeof(T)];
}