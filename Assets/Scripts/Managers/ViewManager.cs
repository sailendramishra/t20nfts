using System.Collections.Generic;
using UnityEngine;
using System;

public class ViewManager : IManager
{
    public GameObject MainCanvas;
    public GameObject MainWorld;

    private List<IView> viewsList;

    public void Init()
    {
        viewsList = new List<IView>
        {
            new CountrySelectionView(),
            new FormatSelectionView(),
            new PlayerSelectionView(),
            new TossView(),
            new StatisticsSelectionView(),
            new GameView(),
            new NotificationView(),
            new GameOverView()
        };

        MainCanvas = GameObject.Find("Canvas");
        MainWorld = GameObject.Find("World");
    }

    public T GetView<T>() where T : IView
    {
        for (int i = 0; i < viewsList.Count; i++)
        {
            if (viewsList[i] is T view)
            {
                return view;
            }
        }

        return default;
    }

    public void SendUpdateEventToViews()
    {
        for (int i = 0; i < viewsList.Count; i++)
        {
            viewsList[i].Update();
        }
    }

    public void HideAllViews()
    {
        for (int i = 0; i < viewsList.Count; i++)
        {
            viewsList[i].Hide();
        }
    }
}