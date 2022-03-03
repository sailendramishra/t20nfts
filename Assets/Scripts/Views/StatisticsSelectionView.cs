using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class StatisticsSelectionView : IView
{
    private ViewManager _viewManager;
    private StatisticsSelectionController _statisticsSelectionController;

    private Timer _timer;

    private GameObject _selfUI;
    public GameObject SelfUi => _selfUI;

    private GameObject _selectionView;

    private Transform _selectedCardParent;
    private Transform _statisticsParent;

    private Button _matchPlayedButton;
    private Button _runsScoredButton;
    private Button _centuriesButton;
    private Button _fiftiesButton;
    private Button _highestScoreButton;
    private Button _battingAvgButton;
    private Button _wicketsButton;
    private Button _catchesButton;
    private Button _nextButton;

    private Image _timerProgress;
    private TextMeshProUGUI _timerText;

    public void Show()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _statisticsSelectionController = ProjectClient.Instance.GetManager<ControllerManager>().GetController<StatisticsSelectionController>();
        _timer = new Timer(10, ShowGameView);

        _selfUI = GameObject.Instantiate((GameObject)Resources.Load("View/StatisticsSelectionView"), _viewManager.MainCanvas.transform);

        _selectedCardParent = _selfUI.transform.Find("SelectedCards");
        _statisticsParent = _selfUI.transform.Find("StatisticsCollection");

        _matchPlayedButton = _statisticsParent.Find("MatchesPlayed_Button").GetComponent<Button>();
        _runsScoredButton = _statisticsParent.Find("RunsScored_Button").GetComponent<Button>();
        _centuriesButton = _statisticsParent.Find("Centuries_Button").GetComponent<Button>();
        _fiftiesButton = _statisticsParent.Find("Fifties_Button").GetComponent<Button>();
        _highestScoreButton = _statisticsParent.Find("HighestScore_Button").GetComponent<Button>();
        _battingAvgButton = _statisticsParent.Find("BattingAverage_Button").GetComponent<Button>();
        _wicketsButton = _statisticsParent.Find("Wickets_Button").GetComponent<Button>();
        _catchesButton = _statisticsParent.Find("Catches_Button").GetComponent<Button>();

        _nextButton = _selfUI.transform.Find("Next_Button").GetComponent<Button>();

        _timerProgress = _selfUI.transform.Find("Timer/Progress").GetComponent<Image>();
        _timerText = _selfUI.transform.Find("Timer/Progress/Timer_Text").GetComponent<TextMeshProUGUI>();

        _statisticsSelectionController.OnViewInitialize();
    }

    public void Update()
    {
        if (_selfUI != null)
        {
            _timerProgress.fillAmount = _timer.UpdateTimeProgress();
            _timerText.text = ((int)_timer.ProgressTime).ToString();
        }
    }

    public void InitializeButtonsEvent()
    {
        _matchPlayedButton.onClick.AddListener(() =>
        {
            RemoveOldSelection();
            _selectionView.transform.SetParent(_matchPlayedButton.transform, false);
            _statisticsSelectionController.SelectStatistics(GameManager.PLAYERSTATISTICS.MATCHES_PLAYED);
            HighlightNewSelection();
        });

        _runsScoredButton.onClick.AddListener(() =>
        {
            RemoveOldSelection();
            _selectionView.transform.SetParent(_runsScoredButton.transform, false);
            _statisticsSelectionController.SelectStatistics(GameManager.PLAYERSTATISTICS.RUNS_SCORED);
            HighlightNewSelection();
        });

        _centuriesButton.onClick.AddListener(() =>
        {
            RemoveOldSelection();
            _selectionView.transform.SetParent(_centuriesButton.transform, false);
            _statisticsSelectionController.SelectStatistics(GameManager.PLAYERSTATISTICS.CENTURIES);
            HighlightNewSelection();
        });

        _fiftiesButton.onClick.AddListener(() =>
        {
            RemoveOldSelection();
            _selectionView.transform.SetParent(_fiftiesButton.transform, false);
            _statisticsSelectionController.SelectStatistics(GameManager.PLAYERSTATISTICS.FIFTIES);
            HighlightNewSelection();
        });

        _highestScoreButton.onClick.AddListener(() =>
        {
            RemoveOldSelection();
            _selectionView.transform.SetParent(_highestScoreButton.transform, false);
            _statisticsSelectionController.SelectStatistics(GameManager.PLAYERSTATISTICS.HIGHESTSCORE);
            HighlightNewSelection();
        });

        _battingAvgButton.onClick.AddListener(() =>
        {
            RemoveOldSelection();
            _selectionView.transform.SetParent(_battingAvgButton.transform, false);
            _statisticsSelectionController.SelectStatistics(GameManager.PLAYERSTATISTICS.BATTING_AVERAGE);
            HighlightNewSelection();
        });

        _wicketsButton.onClick.AddListener(() =>
        {
            RemoveOldSelection();
            _selectionView.transform.SetParent(_wicketsButton.transform, false);
            _statisticsSelectionController.SelectStatistics(GameManager.PLAYERSTATISTICS.WICKETS);
            HighlightNewSelection();
        });

        _catchesButton.onClick.AddListener(() =>
        {
            RemoveOldSelection();
            _selectionView.transform.SetParent(_catchesButton.transform, false);
            _statisticsSelectionController.SelectStatistics(GameManager.PLAYERSTATISTICS.CATCHES);
            HighlightNewSelection();
        });

        _nextButton.onClick.AddListener(() =>
        {
            ShowGameView();
        });
    }

    private void HighlightNewSelection()
    {
        Transform selectionParent = _selectionView.transform.parent;
        selectionParent.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f);
    }

    private void RemoveOldSelection()
    {
        Transform oldParent = _selectionView.transform.parent;
        oldParent.DOScale(Vector3.one, 0.2f);
    }

    private void ShowGameView()
    {
        Hide();
        _viewManager.GetView<GameView>().Show();
    }

    public void ShowSelectedCards(List<CardObject> selectedCards)
    {
        for (int i = 0; i < selectedCards.Count; i++)
        {           
            Transform cardParent = _selectedCardParent.transform.GetChild(i);
            selectedCards[i].SetUpCardPrent(cardParent);
            selectedCards[i].SetUpCardView();
        }
    }

    public void ShowDefaultStatisticsSelection()
    {
        GameObject selectionPrefab = (GameObject)Resources.Load("View/SelectionView");
        _selectionView = GameObject.Instantiate(selectionPrefab, _statisticsParent.GetChild(0));
        _selectionView.GetComponent<RectTransform>().sizeDelta = new Vector2(75.0f, 75.0f);
        _statisticsSelectionController.SelectStatistics(GameManager.PLAYERSTATISTICS.MATCHES_PLAYED);

        HighlightNewSelection();
    }

    private void DestroyCards()
    {

    }

    public void Hide()
    {
        if (_selfUI != null)
        {
            GameObject.Destroy(_selfUI);
            _selfUI = null;
        }
        _statisticsSelectionController.DestroyCards();
    }
}