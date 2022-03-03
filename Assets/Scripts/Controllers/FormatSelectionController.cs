using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormatSelectionController : IController
{
    private GameManager _gameManager;
    private ViewManager _viewManager;
    private FormatSelectionView _formatSelectionView;

    // Start is called before the first frame update
    public void Init()
    {
        _gameManager = ProjectClient.Instance.GetManager<GameManager>();
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _formatSelectionView = _viewManager.GetView<FormatSelectionView>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void OnViewInitialize()
    {
        _formatSelectionView.InitializeButtonsEvent();
    }   

    public void SelectFormat(Country.Format format)
    {
        _gameManager.SetMatchFormat(format);
        _viewManager.GetView<PlayerSelectionView>().Show();
        _formatSelectionView.Hide();
    }
}
