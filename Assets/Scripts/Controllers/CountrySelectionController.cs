using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountrySelectionController : IController
{
    private GameManager _gameManager;
    private DataManager _dataManager;
    private CountrySelectionView _countrySelectionView;
    private ViewManager _viewManager;
    // Start is called before the first frame update
    private List<Country> _countries;

    public void Init()
    {
        _dataManager = ProjectClient.Instance.GetManager<DataManager>();
        _gameManager = ProjectClient.Instance.GetManager<GameManager>();
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _countrySelectionView = _viewManager.GetView<CountrySelectionView>();
    }

    public void OnViewInitialize()
    {
        _countrySelectionView.InitializeButtonsEvent();
        _countries = _dataManager.LoadCountriesData();
        _countrySelectionView.ShowCountries(_countries);
        _countrySelectionView.ShowDefaultCountrySelection();
        _gameManager.SetCountry(_countries[0].CountryName);
    }

    public void ClickOnCountry(string name)
    {
        _gameManager.SetCountry(name);
    }

    public void ClickOnNextButton()
    {
        _viewManager.GetView<FormatSelectionView>().Show();
        _countrySelectionView.Hide();
    }

    public Country PickRandomCountry()
    {
        return _countries[Random.Range(0, _countries.Count)];
    }
}