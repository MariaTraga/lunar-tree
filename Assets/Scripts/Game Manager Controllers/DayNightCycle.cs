using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    const float secondsInDay = 86400f;
    const float phaseLength = 900f; //15 ingame min

    [Header("Day Night Cycle Properties")]
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] float timeScale = 600f; // 1sec real life = 10min in game
    [SerializeField] float startAtTime = 28800f; // 8 in the morning

    [Header("Needed References")]
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI dayText;
    [SerializeField] Light2D globalLight;
    


    float time;
    private int days;
    private int hh, mm;

    Volume globalVolume;
    List<TimeAgent> timeAgents;
    int oldPhase = 0;

    private void Awake()
    {
        timeAgents = new List<TimeAgent>();
        globalVolume = FindObjectOfType<GlobalVolume>().GetComponent<Volume>();
    }

    //Subscribe Time Agent to List
    public void AddTimeAgent(TimeAgent timeAgent) 
    {
        timeAgents.Add(timeAgent);
    }

    //Unsubscribe Time Agent from List
    public void RemoveTimeAgent(TimeAgent timeAgent)
    {
        timeAgents.Remove(timeAgent);
    }

    private void Start()
    {
        time = startAtTime;
    }

    public float Hours
    {
        get { return time / 3600f; }
    }

    public float Minutes
    {
        get { return time % 3600f / 60f; }
    }


    private void Update()
    {
        time += Time.deltaTime * timeScale;

        UpdateTimeText();
        ChangeLightCurve();

        if (time > secondsInDay)
        {
            NextDay();
            InvokeDayAgents();
        }

        InvokeTimeAgents();
    }

    

    private void UpdateTimeText()
    {
        hh = (int)Hours;
        mm = (int)Minutes;
        timeText.text = hh.ToString("00") + ":" + mm.ToString("00");
        dayText.text = "Day " + days;
    }

    private void ChangeLightCurve()
    {
        float evaluateTimeCurve = nightTimeCurve.Evaluate(Hours);
        globalVolume.weight = evaluateTimeCurve;   
    }

    private void NextDay()
    {
        time = 0;
        days += 1;
    }

    private void InvokeTimeAgents()
    {
        //Phases (15 ingame min) help with invoking time agent in longer intervals rather than with every update
        int currentPhase = (int) (time / phaseLength);

        if(oldPhase != currentPhase)
        {
            oldPhase = currentPhase;
            for (int i = 0; i < timeAgents.Count; i++)
            {
                timeAgents[i].InvokeTime();
            }
        }
        
    }

    private void InvokeDayAgents()
    {
        for (int i = 0; i < timeAgents.Count; i++)
        {
            timeAgents[i].InvokeDay();
        }
    }
}
