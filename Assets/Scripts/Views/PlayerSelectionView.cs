using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PlayerSelectionView : IView
{
    private GameObject _selfUI;
    private GameObject SelfUI => _selfUI;

    private ViewManager _viewManager;
    private Transform _selectedCardParent;
    private Transform _replaceCardParent;
    private PlayerSelectionController _playerSelectionController;
    private ScrollRect _replaceCardsScroll;
    private List<CardObject> cards = new List<CardObject>();

    private Button _nextButton;

    public void Show()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _playerSelectionController = ProjectClient.Instance.GetManager<ControllerManager>().GetController<PlayerSelectionController>();

        _selfUI = GameObject.Instantiate((GameObject)Resources.Load("View/PlayerSelectionView"), _viewManager.MainCanvas.transform);

        _selectedCardParent = _selfUI.transform.Find("SelectedCards");
        _replaceCardParent = _selfUI.transform.Find("ReplaceCard/Scroll View/Viewport/Content");
        _replaceCardsScroll = _selfUI.transform.Find("ReplaceCard/Scroll View").GetComponent<ScrollRect>();
        _nextButton = _selfUI.transform.Find("Next_Button").GetComponent<Button>();

        _playerSelectionController.OnViewInitialize();
    }

    public void InitializeButtonsEvent()
    {
        _nextButton.onClick.AddListener(() =>
        {
            _playerSelectionController.ClickOnNextButton();
        });
    }

    // Update is called once per frame
    public void Update()
    {
        
    }   

    public void ShowSelectedCards(List<CardData> selectedPlayer)
    {
        cards.Clear();
        for (int i = 0; i < selectedPlayer.Count; i++)
        {
            Transform cardParent = _selectedCardParent.transform.GetChild(i);
            CardObject cardObject = new CardObject(selectedPlayer[i], cardParent);
            cardObject.SetCardsForPlayerSelection();
            cardObject.Card.GetComponent<CardMonobehaviour>().IsSelected = true;
            cards.Add(cardObject);
        }
    }

    public void ShowReplaceCards(List<CardData> replacePlayers)
    {
        for (int i = 0; i < replacePlayers.Count; i++)
        {
            CardObject cardObject = new CardObject(replacePlayers[i], _replaceCardParent);
            cardObject.SetCardsForPlayerSelection();
            cardObject.Card.GetComponent<CardMonobehaviour>().IsSelected = false;
            cards.Add(cardObject);
        }
    }

    public void HandleScroll(bool isEnable)
    {
        _replaceCardsScroll.horizontal = isEnable;
    }

    public List<CardObject> GetSelectedPlayerData()
    {
        List<CardObject> selectedCards = cards.Where(x => x.Card.GetComponent<CardMonobehaviour>().IsSelected).ToList();

        return selectedCards;
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