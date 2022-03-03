using DG.Tweening;
using UnityEngine;

public class GameController : IController
{
    private ViewManager _viewManager;
    private GameManager _gameManager;
    private GameView _gameView;

    private CardObject _playerCard;
    private CardObject _opponentCard;

    public void Init()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _gameManager = ProjectClient.Instance.GetManager<GameManager>();
        _gameView = _viewManager.GetView<GameView>();       
    }

    public void OnViewInitialize()
    {
        _gameManager.ResetGame();
        _gameManager.SetUpOpponentPlayer();
        _gameManager.StartNextRound();
        _gameView.ShowSelectedCards(_gameManager.LocalPlayer.Cards);
        _gameView.ShowSelectedStatistics(_gameManager.PlayerStatisctics.ToString().ToLower());
        _gameView.OnIntializeButtonsEvent();
        _gameView.UpdateRound(_gameManager.GameRound);
        _gameView.UpdatePoints(_gameManager.LocalPlayer.Points);
        PickOpponentCard();
    }   

    public void PickOpponentCard()
    {
        _opponentCard = _gameManager.PickRandomOpponentCard();
        if (_opponentCard == null)
        {
            _viewManager.GetView<GameOverView>().Show();
            _gameView.Hide();
            return;
        }
        _gameView.ShowOpponentCard(_opponentCard);
    }

    public void SetPickPlayerCard(CardObject cardObject)
    {
        _playerCard = cardObject;
    }

    public void ClickOnReadyButton()
    {
        if (_playerCard != null && _opponentCard != null)
        {
            GameObject.Destroy(_playerCard.Card.GetComponent<PickCard>());
            _gameManager.LocalPlayer.Cards.Remove(_playerCard);
            _gameManager.LocalPlayer.PlayedCards.Add(_playerCard);
            _playerCard.FlipCard();
            _opponentCard.FlipCard();

            CardObject winCardObject = _gameManager.CalculateResult(_playerCard, _opponentCard);

            if (winCardObject == _playerCard)
            {
                _gameView.ShowRoundResult("YOU WON");
                _gameManager.LocalPlayer.AddPoints(500);
                _gameView.UpdatePoints(_gameManager.LocalPlayer.Points);
            }
            else
            {
                _gameView.ShowRoundResult("YOU LOSE");
            }            

            float waitTime = 2;
            DOTween.To(() => waitTime, x => waitTime = x, 0, 2).OnComplete(() =>
            {
                GameObject.Destroy(_playerCard.Card);
                GameObject.Destroy(_opponentCard.Card);
                _playerCard.DestroyCard();
                _opponentCard.DestroyCard();
                _gameManager.StartNextRound();
                _gameView.UpdateRound(_gameManager.GameRound);
                PickOpponentCard();
            });
        }
    }
}
