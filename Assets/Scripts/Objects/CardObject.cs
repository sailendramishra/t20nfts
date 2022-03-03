using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardObject
{
    private PlayerObject _playerObject;
    public PlayerObject PlayerObject;

    private CardData _playerData;
    public CardData PlayerData => _playerData;

    private GameObject _card;
    public GameObject Card => _card;

    private GameObject _backSide;

    private Transform _cardParent;

    private Image _playerImage;

    private TextMeshProUGUI _playerNameText;
    private TextMeshProUGUI _matchesPlayedText;
    private TextMeshProUGUI _runsScoredText;
    private TextMeshProUGUI _centuriesText;
    private TextMeshProUGUI _fiftiesText;
    private TextMeshProUGUI _highestScoreText;
    private TextMeshProUGUI _battingAverageText;
    private TextMeshProUGUI _wicketsText;
    private TextMeshProUGUI _catchesText;

    private bool _isSelected;
    public bool IsSelected => _isSelected;

    public CardObject(CardData playerData, Transform parent)
    {
        _playerData = playerData;
        _cardParent = parent;

        SetUpCardView();
    }

    public void SetUpCardPrent(Transform parent)
    {
        _cardParent = parent;
    } 

    public void SetUpCardView()
    {       
        if (_card == null)
        {
            GameObject cardPrefab = (GameObject)Resources.Load("View/CardObject");
            _card = GameObject.Instantiate(cardPrefab, _cardParent);
            Init();
        }

        _backSide = _card.transform.Find("Back").gameObject;

        _playerNameText.text = PlayerData.PlayerName;
        _matchesPlayedText.text += " :" + PlayerData.MatchesPlayed;
        _runsScoredText.text += " :" + PlayerData.RunsScored;
        _centuriesText.text += " :" + PlayerData.Centuries;
        _fiftiesText.text += " :" + PlayerData.Fifties;
        _highestScoreText.text += " :" + PlayerData.HighestScore;
        _battingAverageText.text += " :" + PlayerData.BattingAverage;
        _wicketsText.text += " :" + PlayerData.Wickets;
        _catchesText.text += " :" + PlayerData.Catches;
    }

    private void Init()
    {
        _playerImage = _card.transform.Find("PlayerImage").GetComponent<Image>();
        _playerNameText = _card.transform.Find("PlayerName_Text").GetComponent<TextMeshProUGUI>();
        _matchesPlayedText = _card.transform.Find("Statistics/MatchesPlayed_Text").GetComponent<TextMeshProUGUI>();
        _runsScoredText = _card.transform.Find("Statistics/RunsScored_Text").GetComponent<TextMeshProUGUI>();
        _centuriesText = _card.transform.Find("Statistics/Centuries_Text").GetComponent<TextMeshProUGUI>();
        _fiftiesText = _card.transform.Find("Statistics/Fifties_Text").GetComponent<TextMeshProUGUI>();
        _highestScoreText = _card.transform.Find("Statistics/HighestScore_Text").GetComponent<TextMeshProUGUI>();
        _battingAverageText = _card.transform.Find("Statistics/BattingAverage_Text").GetComponent<TextMeshProUGUI>();
        _wicketsText = _card.transform.Find("Statistics/Wickets_Text").GetComponent<TextMeshProUGUI>();
        _catchesText = _card.transform.Find("Statistics/Catches_Text").GetComponent<TextMeshProUGUI>();
    }

    public void SetCardsForPlayerSelection()
    {
        if (Card != null)
        {
            Card.AddComponent<SelectCard>();
            Card.GetComponent<CardMonobehaviour>().SetCardObject(this);
        }
    }

    public void SetCardsForGamePlay()
    {
        if (Card != null)
        {
            Card.AddComponent<PickCard>();
            Card.GetComponent<CardMonobehaviour>().SetCardObject(this);
        }
    }

    public void SetPlayerObject(PlayerObject playerObject)
    {
        _playerObject = playerObject;
    }

    public void SetCardAsSelectedCard()
    {
        if (Card != null)
        {
            Card.GetComponent<CardMonobehaviour>().IsSelected = true;
        }
    }

    public void DestroyCard()
    {
        _card = null;
    }

    public void FlipCard()
    {
        _backSide.transform.SetSiblingIndex(0);
        //Card.transform.DOScale(new Vector3(0, 1, 1), 0.25f).OnComplete(() =>
        //{
        //    _backSide.transform.SetSiblingIndex(0);
        //    Card.transform.DOScale(new Vector3(-1, 1, 1), 0.25f);
        //});
    }

    public void HideCard()
    {
        _backSide.transform.SetAsLastSibling();
    }
}