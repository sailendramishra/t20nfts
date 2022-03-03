//using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : IManager
{
    private DataManager _dataManager;
    private ViewManager _viewManager;
    public enum PLAYERSTATISTICS
    {
        MATCHES_PLAYED,
        RUNS_SCORED,
        CENTURIES,
        FIFTIES,
        HIGHESTSCORE,
        BATTING_AVERAGE,
        WICKETS,
        CATCHES
    }

    private PLAYERSTATISTICS _playerStatisticsType;
    public PLAYERSTATISTICS PlayerStatisctics => _playerStatisticsType;

    private Country _country;
    public Country Country => _country;

    private bool _tossWon;
    public bool IsTossWon => _tossWon;

    private int _gameRound;
    public int GameRound => _gameRound;

    private PlayerObject _localPlayer;
    public PlayerObject LocalPlayer => _localPlayer;

    private PlayerObject _opponentPlayer;
    public PlayerObject OpponentPlayer => _opponentPlayer;

    public void Init()
    {
        _country = new Country();

        _dataManager = ProjectClient.Instance.GetManager<DataManager>();
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        SetUpPlayers();
    }

    public void SetUpPlayers()
    {
        _localPlayer = new PlayerObject(new List<CardObject>(), new List<CardObject>(), true);
        _opponentPlayer = new PlayerObject(new List<CardObject>(), new List<CardObject>(), false);
    }

    public void SetMatchFormat(Country.Format format)
    {
        _country.MatchFormat = format;
    }

    public void SetCountry(string countryName)
    {
        _country.CountryName = countryName;
    }

    public void SetSelectedPlayer(List<CardObject> cards)
    {
        LocalPlayer.Cards.Clear();
        LocalPlayer.PlayedCards.Clear();

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetPlayerObject(LocalPlayer);
            LocalPlayer.Cards.Add(cards[i]);
        }
    }

    public void SetTossResult(bool result)
    {
        _tossWon = result;
    }

    public void SetPlayerStatistics(PLAYERSTATISTICS statistics)
    {
        _playerStatisticsType = statistics;
    }

    public void SetUpOpponentPlayer()
    {
        ControllerManager controllerManager = ProjectClient.Instance.GetManager<ControllerManager>();
        Country randomCountry = controllerManager.GetController<CountrySelectionController>().PickRandomCountry();

        List<CardData> cardDatas = _dataManager.LoadPlayersData(randomCountry.CountryName, Country.MatchFormat.ToString());
        List<CardData> selectRandomCards = new List<CardData>();

        for (int i = 0; i < cardDatas.Count; i++)
        {
            int randomIndex = Random.Range(0, cardDatas.Count);
            selectRandomCards.Add(cardDatas[randomIndex]);
            cardDatas.RemoveAt(randomIndex);
        }
        _opponentPlayer.Cards.Clear();
        _opponentPlayer.PlayedCards.Clear();

        for (int i = 0; i < selectRandomCards.Count; i++)
        {
            CardObject cardObject = new CardObject(selectRandomCards[i], _viewManager.MainCanvas.transform);
            cardObject.SetPlayerObject(_opponentPlayer);
            cardObject.Card.SetActive(false);
            _opponentPlayer.Cards.Add(cardObject);
        }
    }

    public CardObject PickRandomOpponentCard()
    {
        if (OpponentPlayer.Cards.Count == 0)
        {
            return null;
        }
        CardObject card = OpponentPlayer.PickCard(PlayerStatisctics);
        OpponentPlayer.Cards.Remove(card);
        OpponentPlayer.PlayedCards.Add(card);
        return card;
    }

    public void StartNextRound()
    {
        _gameRound++;
    }

    public CardObject CalculateResult(CardObject playerCard, CardObject opponetCard)
    {
        switch (PlayerStatisctics)
        {
            case PLAYERSTATISTICS.MATCHES_PLAYED:
                return playerCard.PlayerData.MatchesPlayed > opponetCard.PlayerData.MatchesPlayed ? playerCard : opponetCard;

            case PLAYERSTATISTICS.BATTING_AVERAGE:
                return playerCard.PlayerData.BattingAverage > opponetCard.PlayerData.BattingAverage ? playerCard : opponetCard;

            case PLAYERSTATISTICS.CATCHES:
                return playerCard.PlayerData.Catches > opponetCard.PlayerData.Catches ? playerCard : opponetCard;

            case PLAYERSTATISTICS.CENTURIES:
                return playerCard.PlayerData.Centuries > opponetCard.PlayerData.Centuries ? playerCard : opponetCard;

            case PLAYERSTATISTICS.FIFTIES:
                return playerCard.PlayerData.Fifties > opponetCard.PlayerData.Fifties ? playerCard : opponetCard;

            case PLAYERSTATISTICS.HIGHESTSCORE:
                return playerCard.PlayerData.HighestScore > opponetCard.PlayerData.HighestScore ? playerCard : opponetCard;

            case PLAYERSTATISTICS.RUNS_SCORED:
                return playerCard.PlayerData.RunsScored > opponetCard.PlayerData.RunsScored ? playerCard : opponetCard;

            case PLAYERSTATISTICS.WICKETS:
                return playerCard.PlayerData.Wickets > opponetCard.PlayerData.Wickets ? playerCard : opponetCard;

            default:
                return default;
        }
    }

    public void PickRandomGameStatistics()
    {
        _playerStatisticsType = (PLAYERSTATISTICS)Random.Range(0, System.Enum.GetValues(typeof(PLAYERSTATISTICS)).Length);
    }

    public void ResetGame()
    {
        _gameRound = 0;
        _localPlayer.ResetPlayer();
        _opponentPlayer.ResetPlayer();
    }
}
