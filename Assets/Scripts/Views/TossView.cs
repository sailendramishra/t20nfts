using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class TossView : IView
{
    private GameObject _selfUI;
    public GameObject SelfUI => _selfUI;

    private ViewManager _viewManager;
    private TossViewController _tossViewController;

    private Button _headsButton;
    private Button _tailsButton;
    private TextMeshProUGUI _resultText;

    private FlipCoin _flipCoin;

    public void Show()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        
        _tossViewController = ProjectClient.Instance.GetManager<ControllerManager>().GetController<TossViewController>();
        _selfUI = GameObject.Instantiate((GameObject)Resources.Load("View/TossView"), _viewManager.MainCanvas.transform);

        _headsButton = _selfUI.transform.Find("TossTable/Heads_Button").GetComponent<Button>();
        _tailsButton = _selfUI.transform.Find("TossTable/Tails_Button").GetComponent<Button>();
        _flipCoin = _selfUI.transform.Find("TossTable/Coin_Image").GetComponent<FlipCoin>();

        _resultText = _selfUI.transform.Find("TossTable/Result_Text").GetComponent<TextMeshProUGUI>();

        _tossViewController.OnViewInitialize();
    }

    public void InitializeButtonsEvent()
    {
        _headsButton.onClick.AddListener(() =>
        {
            FlipCoin(global::FlipCoin.TOSSTYPE.HEADS);
        });
        _tailsButton.onClick.AddListener(() =>
        {
            FlipCoin(global::FlipCoin.TOSSTYPE.TAILS);
        });
    }

    private void FlipCoin(FlipCoin.TOSSTYPE selectType)
    {
        FlipCoin.TOSSTYPE resultType = UnityEngine.Random.Range(0, 100) < 50 ? global::FlipCoin.TOSSTYPE.HEADS : global::FlipCoin.TOSSTYPE.TAILS;

        _headsButton.interactable = false;
        _tailsButton.interactable = false;

        _flipCoin.StartFlip(() =>
        {
            bool isTossWon = selectType == resultType;

            if (isTossWon)
            {
                _tossViewController.SetTossResult(true);
                _resultText.text = "YOU WON";
            }
            else
            {
                _tossViewController.SetTossResult(false);
                _resultText.text = "YOU LOSS";
            }

            PlayActionWithDelay(2, () =>
            {               
                if (isTossWon)
                {
                    Hide();
                    _viewManager.GetView<StatisticsSelectionView>().Show();
                }
                else
                {
                    _tossViewController.PickOpponentStatistics();
                    _tossViewController.ShowNotificationOfStatistics();
                    PlayActionWithDelay(2, () =>
                    {
                        Hide();
                        _viewManager.GetView<GameView>().Show();
                    });
                }
            });
        },resultType);
    }

    private void PlayActionWithDelay(float delayTime, Action cb)
    {
        float waitTime = delayTime;
        DOTween.To(() => waitTime, x => waitTime = x, 0, 2).OnComplete(() =>
        {
            cb?.Invoke();
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