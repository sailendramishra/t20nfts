using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : IView
{
    private ViewManager _viewManager;
    private GameOverController _gameOverController;

    private GameObject _selfUI;
    public GameObject SelfUi => _selfUI;

    private Button _continueButton;
    private Button _exitButton;

    public void Show()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _gameOverController = ProjectClient.Instance.GetManager<ControllerManager>().GetController<GameOverController>();
        _selfUI = GameObject.Instantiate((GameObject)Resources.Load("View/GameOverView"), _viewManager.MainCanvas.transform);

        _continueButton = _selfUI.transform.Find("Continue_Button").GetComponent<Button>();
        _exitButton = _selfUI.transform.Find("Exit_Button").GetComponent<Button>();

        _gameOverController.OnInitializeView();
    }

    public void Update()
    {

    }

    public void InitializeButtonEvents()
    {
        _continueButton.onClick.AddListener(() =>
        {
            _viewManager.GetView<GameView>().Show();
            Hide();
        });

        _exitButton.onClick.AddListener(() =>
        {
            _viewManager.GetView<CountrySelectionView>().Show();
            Hide();
        });
    }

    public void Hide()
    {
        if(_selfUI != null)
        {
            GameObject.Destroy(_selfUI);
            _selfUI = null;
        }
    }
}
