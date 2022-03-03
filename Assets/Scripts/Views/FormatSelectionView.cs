using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormatSelectionView : IView
{
    private GameObject _selfUI;
    private GameObject SelfUI => _selfUI;

    private ViewManager _viewManager;
    private FormatSelectionController _formatSelectionController;

    private Button _odiButton;
    private Button _testButton;
    private Button _t20Button;

    public void Show()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _formatSelectionController = ProjectClient.Instance.GetManager<ControllerManager>().GetController<FormatSelectionController>();

        _selfUI = GameObject.Instantiate((GameObject)Resources.Load("View/FormatSelectionView"), _viewManager.MainCanvas.transform);
        _odiButton = _selfUI.transform.Find("ODI_Button").GetComponent<Button>();
        _testButton = _selfUI.transform.Find("Test_Button").GetComponent<Button>();
        _t20Button = _selfUI.transform.Find("T20_Button").GetComponent<Button>();
        _formatSelectionController.OnViewInitialize();
    }

    public void InitializeButtonsEvent()
    {
        _odiButton.onClick.AddListener(() =>
        {
            _formatSelectionController.SelectFormat(Country.Format.ODI);
        });
        _testButton.onClick.AddListener(() =>
        {
            _formatSelectionController.SelectFormat(Country.Format.TEST);
        });
        _t20Button.onClick.AddListener(() =>
        {
            _formatSelectionController.SelectFormat(Country.Format.T20);
        });
    }

    public void Update()
    {
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
