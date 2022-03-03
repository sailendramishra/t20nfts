using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectionController : IController
{
    private ViewManager _viewManager;
    private DataManager _dataManager;
    private GameManager _gameManager;
    private PlayerSelectionView _playerSelectionView;
    private List<CardData> _playerDatas;

    public void Init()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _dataManager = ProjectClient.Instance.GetManager<DataManager>();
        _gameManager = ProjectClient.Instance.GetManager<GameManager>();
        _playerSelectionView = _viewManager.GetView<PlayerSelectionView>();
    }

    public void OnViewInitialize()
    {
        _playerSelectionView.InitializeButtonsEvent();

        _playerDatas = _dataManager.LoadPlayersData(_gameManager.Country.CountryName, _gameManager.Country.MatchFormat.ToString());
        List<CardData> _selectedPlayer = new List<CardData>();

        for (int i = 0; i < 5; i++)
        {
            int index = Random.Range(0, _playerDatas.Count);
            _selectedPlayer.Add(_playerDatas[index]);
            _playerDatas.RemoveAt(index);
        }

        _playerSelectionView.ShowSelectedCards(_selectedPlayer);
        _playerSelectionView.ShowReplaceCards(_playerDatas);
    }

    public void ClickOnNextButton()
    {
        _gameManager.SetSelectedPlayer(_playerSelectionView.GetSelectedPlayerData());
        _viewManager.GetView<TossView>().Show();
        _playerSelectionView.Hide();
    }
}