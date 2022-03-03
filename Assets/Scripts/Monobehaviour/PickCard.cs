using UnityEngine;
using UnityEngine.EventSystems;

public class PickCard : CardMonobehaviour, IPointerDownHandler
{
    private GameController _gameController;

    // Start is called before the first frame update
    private void Start()
    {
        _gameController = ProjectClient.Instance.GetManager<ControllerManager>().GetController<GameController>();
        _dragController = new DragController
        {
            OnDrag = DragCard,
            OnDragEnd = DragEnd
        };
        InitializeMembers();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _dragController._clickOnObject = true;
        AllowToMoveCard();
    }

    private void DragEnd()
    {
        if (_triggerCard == null)
        {
            _putBack?.Invoke();
        }
        else
        {
            if (!_triggerCard.name.Equals("PlayerSlot"))
            {
                _putBack?.Invoke();
            }
            else
            {
                if (_triggerCard.transform.childCount == 0)
                {
                    _gameController.SetPickPlayerCard(cardObject);
                    _myTransform.SetParent(_triggerCard.transform, false);
                    _myTransform.localPosition = Vector3.zero;
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