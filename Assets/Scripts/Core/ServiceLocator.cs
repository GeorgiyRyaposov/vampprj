using System;
using System.Collections.Generic;

/// <summary>
/// Синглтон-локатор сервисов. Даёт доступ к машине состояний и другим сервисам из любой точки.
/// Доступ: ServiceLocator.Instance.Get&lt;GameStateMachine&gt;(), ServiceLocator.Instance.Get&lt;IMyService&gt;().
///
/// Как зарегистрировать новый сервис: в BootstrapState или в GameRunner.Awake вызвать
/// ServiceLocator.Instance.Register(new MyService()); затем везде получать через Get&lt;MyService&gt;().
/// </summary>
public class ServiceLocator
{
    private static ServiceLocator _instance;
    private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

    /// <summary>
    /// Единственный экземпляр локатора. При первом обращении создаётся новый инстанс.
    /// </summary>
    public static ServiceLocator Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ServiceLocator();
            return _instance;
        }
    }

    /// <summary>
    /// Регистрирует сервис по типу T. Существующий сервис того же типа будет перезаписан.
    /// </summary>
    public void Register<T>(T service) where T : class
    {
        _services[typeof(T)] = service;
    }

    /// <summary>
    /// Возвращает зарегистрированный сервис типа T. Кидает InvalidOperationException, если сервис не найден.
    /// </summary>
    public T Get<T>() where T : class
    {
        var type = typeof(T);
        if (_services.TryGetValue(type, out var service))
            return (T)service;
        throw new InvalidOperationException($"Service of type {type.Name} is not registered.");
    }

    /// <summary>
    /// Удаляет регистрацию сервиса типа T. Полезно для тестов или сброса.
    /// </summary>
    public void Unregister<T>() where T : class
    {
        _services.Remove(typeof(T));
    }

    /// <summary>
    /// Очищает все зарегистрированные сервисы. Используйте в тестах или при полной переинициализации.
    /// </summary>
    public void Clear()
    {
        _services.Clear();
    }
}
