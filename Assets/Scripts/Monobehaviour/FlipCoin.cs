using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class FlipCoin : MonoBehaviour
{
    public enum TOSSTYPE
    {
        HEADS,
        TAILS
    };

    private TextMeshProUGUI _coinText;
    private RectTransform _rectTransform;

    //private bool isHeads = true;

    // Start is called before the first frame update
    private void Start()
    {
        _coinText = transform.Find("Coin_Text").GetComponent<TextMeshProUGUI>();
        _rectTransform = GetComponent<RectTransform>();
    }


    public void StartFlip(Action OnFlipComplete, TOSSTYPE tossType)
    {
        _rectTransform.DOJump(transform.position, 500, 1, 2f);

        var sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOScaleY(0, 0.05f));
        sequence.OnStepComplete(() =>
        {
            //if (isHeads)
            //{
            //    isHeads = false;
            //    _coinText.text = "T";
            //}
            //else
            //{
            //    isHeads = true;
            //    _coinText.text = "H";
            //}
          
        });
        sequence.SetLoops(32, LoopType.Yoyo).SetEase(Ease.Linear);
        sequence.OnComplete(() =>
        {
            OnFlipComplete?.Invoke();
            _coinText.text = tossType == TOSSTYPE.HEADS ? "H" : "T";
        });
        //sequence.Kill();
    }

    public void Flip()
    {

    }
}
