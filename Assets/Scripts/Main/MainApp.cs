using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class MainApp : MonoBehaviour
{
    public bool IS_DEMO_VERSION;

    private ProjectClient client;

    private ViewManager viewManager;

    public static MainApp Instance;

    public static Queue<Action> ExecuteOnMainThread = new Queue<Action>();

    public static string GameVersion = "V 0.1";

    private int currentScreens;

    private static bool hasStarted;

    private void ToDoOnAwake()
    {
        currentScreens = 0;
        Instance = this;
        DontDestroyOnLoad(this);
        client = ProjectClient.Instance;
        client.InitManagers();
        viewManager = client.GetManager<ViewManager>();
        InitializeProject();
    }

    private void Update()
    {
#if UNITY_EDITOR
        /*if (Input.GetButtonUp("Fire1"))
        {
            ScreenCapture.CaptureScreenshot("Screenshot"+currentScreens.ToString()+".png");
            currentScreens++;
        }*/
#endif

        if (!hasStarted)
        {
            hasStarted = true;
            ToDoOnAwake();
        }

        viewManager.SendUpdateEventToViews();
        while (ExecuteOnMainThread.Count > 0)
        {
            ExecuteOnMainThread.Dequeue()?.Invoke();
        }
    }

    private void InitializeProject()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        viewManager.GetView<CountrySelectionView>().Show();
    }

    public void UseCoroutine(IEnumerator method)
    {
        _ = StartCoroutine(method);
    }

    public void ResetMainApp()
    {
        hasStarted = false;
    }
}