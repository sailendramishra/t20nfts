using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultView : IView
{
    private ViewManager _viewManager;

    private GameObject _selfUI;
    public GameObject SelfUI => _selfUI;

    private TextMeshProUGUI _playerStatisticText;
    private TextMeshProUGUI _aIStatisticText;

    public void Show()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _selfUI = GameObject.Instantiate((GameObject)Resources.Load("Views/ResultView"), _viewManager.MainCanvas.transform);

        _playerStatisticText = _selfUI.transform.Find("ResultView").GetComponent<TextMeshProUGUI>();
    }

    public void Update()
    {

    }

    public void Hide()
    {

    }
}