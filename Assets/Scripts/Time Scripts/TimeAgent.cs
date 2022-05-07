using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAgent : MonoBehaviour
{
    public Action onTimeTick;
    public Action onDayTick;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        //TimeAgent makes sure DayNightCycle (aka timeController) knows about it
        //and timeController will inform the TimeAgent on when to Invoke the onTimeTick
        GameManager.Instance.timeController.AddTimeAgent(this);
    }

    public void InvokeTime()
    {
        onTimeTick?.Invoke();
    }

    public void InvokeDay()
    {
        onDayTick?.Invoke();
    }

    private void OnDestroy()
    {
        GameManager.Instance.timeController.RemoveTimeAgent(this);
    }
}
