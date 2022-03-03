using System;
using UnityEngine;

public class DragController
{
    public enum Direction
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    
    public Action OnDrag;
    public Action OnItemSwipeUp;
    public Action OnItemSwipeDown;
    public Action OnDragEnd;
    public Action OnPointerDown;

    public bool _allowToDrag;
    public bool _clickOnObject;
    public bool _allowToGetDirection;

    public Vector3 fp;
    public Vector3 lp;

    private readonly float _minSwipeDistance;

    public DragController()
    {
        _minSwipeDistance = Screen.height * 4 / 100.0f;
    }

    public void Update()
    {
        if (!_clickOnObject)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnPointerDown?.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            if (_allowToDrag)
            {
                OnDrag?.Invoke();
            }

            if (_allowToGetDirection)
            {
                lp = Input.mousePosition;
                Direction direction = GetDirection();
                if (direction == Direction.NONE)
                {
                    return;
                }

                _allowToGetDirection = false;

                if (direction == Direction.UP)
                {
                    OnItemSwipeUp?.Invoke();
                }
                else if (direction == Direction.DOWN)
                {
                    OnItemSwipeDown?.Invoke();
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _clickOnObject = false;
            _allowToGetDirection = false;

            if (_allowToDrag)
            {
                _allowToDrag = false;
                OnDragEnd?.Invoke();
            }
        }
    }

    private Direction GetDirection()
    {
        Vector2 swipe = lp - fp;

        return swipe.magnitude < _minSwipeDistance
            ? Direction.NONE
            : Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y)
            ? swipe.x > 0 ? Direction.RIGHT : Direction.LEFT
            : swipe.y > 0 ? Direction.UP : Direction.DOWN;
    }
}