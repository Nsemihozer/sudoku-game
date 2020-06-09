using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    int minute = 0, sec = 0, hour = 0;
    private Text clock;
    float delta;
    private bool stop_clock=false;
    public static Clock instance;

    private void Awake()
    {
        if (instance)
            Destroy(instance);
        instance = this;
        clock = GetComponent<Text>();
        if (levelSettings.instance.getContinuePrevious())
            delta = Config.ReadGameTime();
        else
            delta = 0;
    }
    private void Start()
    {
        stop_clock = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (levelSettings.instance.getPause()==false && stop_clock == false)
        {
            delta += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(delta);
            string min = leadingZero(span.Minutes+ (span.Hours * 60));
            string sec = leadingZero(span.Seconds);
            clock.text =  min + ":" + sec;
        }
        
    }
    public Text GetCurrentTimeText()
    {
        return clock;
    }
    public void onGameOver()
    {
        stop_clock = true;
    }
    public void onBoardCompleted()
    {
        stop_clock = true;
    }
    public void onGameOverContinue()
    {
        stop_clock = false;
    }
    string leadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }

    private void OnEnable()
    {
        GameEvents.onGameOver += onGameOver;
        GameEvents.onBoardCompleted += onBoardCompleted;
    }
    private void OnDisable()
    {
        GameEvents.onGameOver -= onGameOver;
        GameEvents.onBoardCompleted -= onBoardCompleted;
    }
    public static string getCurrentTime()
    {
        return instance.delta.ToString();

    }
    public Text CurrentTime()
    {
        return clock;
    }
}
