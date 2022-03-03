using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameView : IView
{
    private ViewManager _viewManager;
    private GameController _gameController; 

    private GameObject _selfUI;
    public GameObject SelfUi => _selfUI;

    private Transform _playerCardSlot;
    private Transform _opponentCardSlot;
    private Transform _selectedCardParent;

    private Button _readyButton;
    private TextMeshProUGUI _statisticsText;
    private TextMeshProUGUI _pointText;
    private TextMeshProUGUI _roundText;

    public void Show()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _gameController = ProjectClient.Instance.GetManager<ControllerManager>().GetController<GameController>();

        _selfUI = GameObject.Instantiate((GameObject)Resources.Load("View/GameView"), _viewManager.MainCanvas.transform);

        _playerCardSlot = _selfUI.transform.Find("Table/Player/PlayerSlot");
        _opponentCardSlot = _selfUI.transform.Find("Table/Opponent/OpponentSlot");
        _selectedCardParent = _selfUI.transform.Find("SelectedCards");

        _readyButton = _selfUI.transform.Find("Ready_Button").GetComponent<Button>();
        _statisticsText = _selfUI.transform.Find("Statistics_Text").GetComponent<TextMeshProUGUI>();
        _pointText = _selfUI.transform.Find("GameStatistics/GamePoints/Point_Text").GetComponent<TextMeshProUGUI>();
        _roundText = _selfUI.transform.Find("GameStatistics/TotalGamePlayed/Round_Text").GetComponent<TextMeshProUGUI>();
        _gameController.OnViewInitialize();
    }

    public void Update()
    {

    }

    public void OnIntializeButtonsEvent()
    {
        _readyButton.onClick.AddListener(() =>
        {
            _gameController.ClickOnReadyButton();
        });
    }

    public void ShowSelectedStatistics(string statistics)
    {
        _statisticsText.text = string.Concat("Statistics : ", statistics);
    }

    public void ShowSelectedCards(List<CardObject> selectedCards)
    {
        for (int i = 0; i < selectedCards.Count; i++)
        {
            Transform cardParent = _selectedCardParent.transform.GetChild(i);
            selectedCards[i].SetUpCardPrent(cardParent);
            selectedCards[i].SetUpCardView();
            selectedCards[i].SetCardsForGamePlay();
            selectedCards[i].SetCardAsSelectedCard();
        }
    }

    public void ShowOpponentCard(CardObject card)
    {
        card.HideCard();
        card.Card.SetActive(true);
        card.Card.transform.SetParent(_opponentCardSlot, true);
        card.Card.transform.DOLocalMove(Vector3.zero, 0.5f);
    }

    public void ShowRoundResult(string message)
    {
        GameObject notification = GameObject.Instantiate((GameObject)Resources.Load("View/NotificationView"), _viewManager.MainCanvas.transform);
        notification.transform.Find("Message_Text").GetComponent<TextMeshProUGUI>().text = message;
        GameObject.Destroy(notification, 2);
    }

    public void UpdatePoints(int points)
    {
        _pointText.text = string.Concat("Points : " + points);
    }

    public void UpdateRound(int round)
    {
        _roundText.text = string.Concat("Play : " + round);
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
