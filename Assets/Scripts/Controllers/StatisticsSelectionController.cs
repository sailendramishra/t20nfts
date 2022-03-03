using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StatisticsSelectionController : IController
{
    private ViewManager _viewManager;
    private GameManager _gameManager;
    private StatisticsSelectionView _statisticsSelectionView;

    public void Init()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _gameManager = ProjectClient.Instance.GetManager<GameManager>();
        _statisticsSelectionView = _viewManager.GetView<StatisticsSelectionView>();
    }

    public void OnViewInitialize()
    {
        _statisticsSelectionView.ShowSelectedCards(_gameManager.LocalPlayer.Cards);
        _statisticsSelectionView.ShowDefaultStatisticsSelection();
        _statisticsSelectionView.InitializeButtonsEvent();
    }

    public void SelectStatistics(GameManager.PLAYERSTATISTICS statistics)
    {
        _gameManager.SetPlayerStatistics(statistics);
    }

    public void DestroyCards()
    {
        for (int i = 0; i < _gameManager.LocalPlayer.Cards.Count; i++)
        {
            _gameManager.LocalPlayer.Cards[i].DestroyCard();
        }
    }
}
