using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectCard : CardMonobehaviour, IPointerDownHandler
{   
    // Start is called before the first frame update
    private void Start()
    {
        _dragController = new DragController
        {
            OnItemSwipeUp = OnCardSwipeUp,
            OnDrag = DragCard,
            OnDragEnd = DragEnd
        };
        InitializeMembers();
    }

    private void OnCardSwipeUp()
    {
        AllowToMoveCard();
        DisableScroll();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _dragController._clickOnObject = true;

        if (IsSelected)
        {
            SetPutBackEvent();
            AllowToMoveCard();
            return;
        }

        //_viewManager.GetView<PlayerSelectionView>().HandleScroll(false);
        _dragController._allowToGetDirection = true;
        _dragController.fp = Input.mousePosition;
        _dragController.lp = Input.mousePosition;
    }

    private void DisableScroll()
    {
        _viewManager.GetView<PlayerSelectionView>().HandleScroll(false);
    }

    private void DragEnd()
    {
        _viewManager.GetView<PlayerSelectionView>().HandleScroll(true);
        if (_triggerCard == null)
        {
            _putBack?.Invoke();
        }
        else
        {
            if (IsSelected)
            {
                if (_triggerCard.GetComponent<CardMonobehaviour>().IsSelected)
                {
                    Transform triggerParent = _triggerCard.transform.parent;

                    _myTransform.SetParent(triggerParent, false);
                    _triggerCard.transform.SetParent(_oldTransform, false);

                    _myTransform.localPosition = Vector3.zero;
                    _triggerCard.transform.localPosition = Vector3.zero;
                }
                else
                {
                    _putBack?.Invoke();
                }
            }
            else
            {
                if (_triggerCard.GetComponent<CardMonobehaviour>().IsSelected)
                {
                    Transform triggerParent = _triggerCard.transform.parent;

                    _myTransform.SetParent(triggerParent, false);
                    _triggerCard.transform.SetParent(_oldTransform, false);

                    _myTransform.localPosition = Vector3.zero;
                    _triggerCard.transform.localPosition = Vector3.zero;

                    IsSelected = true;
                    _triggerCard.GetComponent<CardMonobehaviour>().IsSelected = false;
                }
                else
                {
                    _putBack?.Invoke();
                }
            }
        }
        _allowToTrigger = false;
        _triggerCard = null;
    }
}
