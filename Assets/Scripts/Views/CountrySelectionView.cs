using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CountrySelectionView : IView
{
    private GameObject _selfUI;
    private GameObject SelfUI => _selfUI;

    private ViewManager _viewManager;
    private CountrySelectionController _countrySelectionController;
    private Transform _countryParent;

    private GameObject _selectionView;
    private Button _nextbutton;

    public void Show()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _countrySelectionController = ProjectClient.Instance.GetManager<ControllerManager>().GetController<CountrySelectionController>();
        _selfUI = GameObject.Instantiate((GameObject)Resources.Load("View/CountrySelectionView"), _viewManager.MainCanvas.transform);
        _countryParent = _selfUI.transform.Find("Scroll View/Viewport/Content");
        _nextbutton = _selfUI.transform.Find("Next_Button").GetComponent<Button>();
        _countrySelectionController.OnViewInitialize();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void InitializeButtonsEvent()
    {
        _nextbutton.onClick.AddListener(_countrySelectionController.ClickOnNextButton);
    }

    public void ShowCountries(List<Country> countries)
    {
        GameObject countryPrefab = (GameObject)Resources.Load("View/Country_Button");

        for (int i = 0; i < countries.Count; i++)
        {
            Country countryData = countries[i];

            GameObject country = GameObject.Instantiate(countryPrefab, _countryParent);
            Texture2D country_Texture = (Texture2D)Resources.Load("Sprites/Countries/" + countries[i].CountryName);
            Sprite country_Sprite = Sprite.Create(country_Texture, new Rect(0, 0, country_Texture.width, country_Texture.height), new Vector2(0.5f, 0.5f));
            country.GetComponent<Image>().sprite = country_Sprite;

            country.GetComponent<Button>().onClick.AddListener(() =>
            {
                Transform oldParent = _selectionView.transform.parent;
                oldParent.DOScale(Vector3.one, 0.2f);

                _selectionView.transform.SetParent(country.transform, false);
                _countrySelectionController.ClickOnCountry(countryData.CountryName);

                Transform selectionParent = _selectionView.transform.parent;
                selectionParent.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
            });
        }
    }

    public void ShowDefaultCountrySelection()
    {
        GameObject selectionPrefab = (GameObject)Resources.Load("View/SelectionView");
        _selectionView = GameObject.Instantiate(selectionPrefab, _countryParent.GetChild(0));
        Transform parent = _countryParent.GetChild(0);
        parent.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
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