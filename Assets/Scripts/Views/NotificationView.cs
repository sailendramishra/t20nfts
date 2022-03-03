using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationView : IView
{
    private ViewManager _viewManager;
    private NotificationController _notificationController;

    private GameObject _selfUI;
    public GameObject SelfUI => _selfUI;

    private TextMeshProUGUI _messageText;
    private string _message;

    public void Show()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _notificationController = ProjectClient.Instance.GetManager<ControllerManager>().GetController<NotificationController>();
        _selfUI = GameObject.Instantiate((GameObject)Resources.Load("View/NotificationView"), _viewManager.MainCanvas.transform);

        _messageText = _selfUI.transform.Find("Message_Text").GetComponent<TextMeshProUGUI>();

        _notificationController.OnViewInitialize();
    }

    public void SetMessage(string message)
    {
        _message = message;
    }

    public void SetMessageToText()
    {
        _messageText.text = _message;
    }

    public void Update()
    {

    }

    public void Hide()
    {
        if(_selfUI != null)
        {
            GameObject.Destroy(_selfUI, 2);
            _selfUI = null;
        }
    }
}