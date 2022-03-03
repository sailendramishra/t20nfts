using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerObject
{
    private List<CardObject> _cards;
    public List<CardObject> Cards => _cards;

    private List<CardObject> _playedCards;
    public List<CardObject> PlayedCards => _playedCards;

    private bool _isLocalPlayer;

    private int _points;
    public int Points => _points;

    public PlayerObject(List<CardObject> cards, List<CardObject> playedCards, bool isLocalplayer)
    {
        _cards = cards;
        _playedCards = playedCards;
        _isLocalPlayer = isLocalplayer;

    }

    public CardObject PickCard(GameManager.PLAYERSTATISTICS statistics)
    {
        switch (statistics)
        {
            case GameManager.PLAYERSTATISTICS.MATCHES_PLAYED:
                return Cards.Aggregate((i1, i2) => i1.PlayerData.MatchesPlayed > i2.PlayerData.MatchesPlayed ? i1 : i2);

            case GameManager.PLAYERSTATISTICS.BATTING_AVERAGE:
                return Cards.Aggregate((i1, i2) => i1.PlayerData.BattingAverage > i2.PlayerData.BattingAverage ? i1 : i2);

            case GameManager.PLAYERSTATISTICS.CATCHES:
                return Cards.Aggregate((i1, i2) => i1.PlayerData.Catches > i2.PlayerData.Catches ? i1 : i2);

            case GameManager.PLAYERSTATISTICS.CENTURIES:
                return Cards.Aggregate((i1, i2) => i1.PlayerData.Centuries > i2.PlayerData.Centuries ? i1 : i2);

            case GameManager.PLAYERSTATISTICS.FIFTIES:
                return Cards.Aggregate((i1, i2) => i1.PlayerData.Fifties > i2.PlayerData.Fifties ? i1 : i2);

            case GameManager.PLAYERSTATISTICS.HIGHESTSCORE:
                return Cards.Aggregate((i1, i2) => i1.PlayerData.HighestScore > i2.PlayerData.HighestScore ? i1 : i2);

            case GameManager.PLAYERSTATISTICS.RUNS_SCORED:
                return Cards.Aggregate((i1, i2) => i1.PlayerData.RunsScored > i2.PlayerData.RunsScored ? i1 : i2);

            case GameManager.PLAYERSTATISTICS.WICKETS:
                return Cards.Aggregate((i1, i2) => i1.PlayerData.Wickets > i2.PlayerData.Wickets ? i1 : i2);

            default:
                return default;
        }
    }

    public void AddPoints(int points)
    {
        _points += points;
    }

    public void ResetPlayer()
    {
        for (int i = 0; i < PlayedCards.Count; i++)
        {
            Cards.Add(PlayedCards[i]);
        }
        PlayedCards.Clear();
    }
}
