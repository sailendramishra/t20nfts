using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TossViewController : IController
{
    private ViewManager _viewManager;
    private TossView _tossView;
    private GameManager _gameManager;

    public void Init()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _tossView = _viewManager.GetView<TossView>();
        _gameManager = ProjectClient.Instance.GetManager<GameManager>();
    }

    public void OnViewInitialize()
    {
        _tossView.InitializeButtonsEvent();
    }

    public void SetTossResult(bool result)
    {
        _gameManager.SetTossResult(result);
    }

    public void PickOpponentStatistics()
    {
        _gameManager.PickRandomGameStatistics();
    }

    public void ShowNotificationOfStatistics()
    {
        NotificationView notificationView = _viewManager.GetView<NotificationView>();
        notificationView.SetMessage(_gameManager.PlayerStatisctics.ToString().ToLower() + " is picked by opponent");
        notificationView.Show();
    }
}
