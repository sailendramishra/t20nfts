using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeController : IController
{
    private GameModeView _gameModeView;
    private GameModeData _gameModeData;
    private ViewManager _viewManager;

    // Start is called before the first frame update
    public void Init()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _gameModeView = _viewManager.GetView<GameModeView>();
        _gameModeData = new GameModeData();
    }

    public void SelectGameMode(GameModeData.GameMode gameMode)
    {
        _gameModeData.gameMode = gameMode;
        _gameModeView.Hide();
        _viewManager.GetView<CountrySelectionView>().Show();
    }
}