using System;
using UnityEngine;

public class CardMonobehaviour : MonoBehaviour
{
    protected CardObject cardObject;
    protected ViewManager _viewManager;
    protected DragController _dragController;
    protected Transform _myTransform;
    protected Transform _oldTransform;
    protected Action _putBack;
    [HideInInspector] public bool IsSelected;
    [HideInInspector] public GameObject _triggerCard;
    protected bool _allowToTrigger;

    protected void InitializeMembers()
    {
        _viewManager = ProjectClient.Instance.GetManager<ViewManager>();
        _myTransform = transform;
        SetPutBackEvent();
    }

    private void Update()
    {
        _dragController.Update();
    }

    protected void AllowToMoveCard()
    {        
        _allowToTrigger = true;
        _dragController._allowToDrag = true;
        _myTransform.SetParent(_viewManager.MainCanvas.transform);
        _myTransform.position = Input.mousePosition;
    }

    protected void SetPutBackEvent()
    {
        _oldTransform = _myTransform.parent;
        _putBack = () =>
        {
            _myTransform.SetParent(_oldTransform, false);
            _myTransform.localPosition = Vector3.zero;
        };
    }

    public void SetCardObject(CardObject card)
    {
        cardObject = card;
    }

    protected void DragCard()
    {
        _myTransform.position = Input.mousePosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_allowToTrigger)
        {
            _triggerCard = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_triggerCard != null && _triggerCard == collision.gameObject)
        {
            _triggerCard = null;
        }
    }
}