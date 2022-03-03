using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeView : IView
{
    private GameObject _selfUI;
    private GameObject SelfUI => _selfUI;

    private ViewManager _viewManager;
    private GameModeController _gameModeController;

    public void Show()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _gameModeController = ProjectClient.Instance.GetManager<ControllerManager>().GetController<GameModeController>();
        _selfUI = GameObject.Instantiate((GameObject)Resources.Load("View/GameModeView"), _viewManager.MainCanvas.transform);
    }  


    public void Update()
    {

    }

    public void ClickOnTurnamentMode()
    {
        _gameModeController.SelectGameMode(GameModeData.GameMode.TOURNAMENT);
    }

    public void Hide()
    {
        if (_selfUI != null)
        {
            GameObject.Destroy(_selfUI);
            _selfUI = null;
        }
    }   
}
