using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : IController
{
    private ViewManager _viewManager;
    private GameOverView _gameOverView;

    public void Init()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _gameOverView = _viewManager.GetView<GameOverView>();
    }

    public void OnInitializeView()
    {
        _gameOverView.InitializeButtonEvents();
    }
}
