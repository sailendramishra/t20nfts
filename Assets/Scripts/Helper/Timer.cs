using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private readonly float _maxTimer;
    private float _time;
    public float ProgressTime => _time;

    private readonly Action _onTimeFinished;

    public Timer(float time, Action onTimeFinished)
    {
        _time = time;
        _onTimeFinished = onTimeFinished;
    }

    public float UpdateTimeProgress()
    {
        if (_time < 0)
        {
            _onTimeFinished?.Invoke();
            return 0;
        }

        _time -= Time.deltaTime;
        return _time / _maxTimer;
    }
}
