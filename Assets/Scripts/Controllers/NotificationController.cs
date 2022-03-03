using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationController : IController
{
    private ViewManager _viewManager;
    private NotificationView _notificationView;

    public void Init()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _notificationView = _viewManager.GetView<NotificationView>();
    }

    public void OnViewInitialize()
    {
        _notificationView.SetMessageToText();
        _notificationView.Hide();
    }
}
