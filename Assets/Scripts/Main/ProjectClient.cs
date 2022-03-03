using System;
using System.Collections.Generic;

public class ProjectClient
{
    private static IDictionary<Type, IManager> managers;
    private static IDictionary<Type, IController> controllers;

    private static ProjectClient instance;

    public static ProjectClient Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ProjectClient();
            }

            return instance;
        }
    }

    internal ProjectClient()
    {
        managers = new Dictionary<Type, IManager>();
        AddManager<ViewManager>(new ViewManager());
        AddManager<ControllerManager>(new ControllerManager());
        AddManager<GameManager>(new GameManager());
        AddManager<DataManager>(new DataManager());
    }

    private void AddManager<T>(IManager manager)
    {
        managers.Add(typeof(T), manager);
    }

    public void InitManagers()
    {
        foreach (IManager manager in managers.Values)
        {
            manager.Init();
        }
    }

    public T GetManager<T>()
    {
        return (T) managers[typeof(T)];
    }
}
