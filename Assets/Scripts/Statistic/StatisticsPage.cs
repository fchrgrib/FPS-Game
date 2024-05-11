using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticsPage : MonoBehaviour
{
    
    private GeneralStatistics generalStatistics;

    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI travelDistance;
    [SerializeField] private TextMeshProUGUI accuracy;
    [SerializeField] private TextMeshProUGUI kdr;
    [SerializeField] private TextMeshProUGUI kill;
    [SerializeField] private TextMeshProUGUI death;

    private void Start()
    {
        generalStatistics = GameObject.FindWithTag("GeneralStatistics").GetComponent<GeneralStatistics>();
    }

    // Update is called once per frame
    void Update()
    {
        
        int hours = Mathf.FloorToInt(generalStatistics.ElapsedTime / 3600); // calculate hours
        int minutes = Mathf.FloorToInt((generalStatistics.ElapsedTime % 3600) / 60); // calculate minutes within the hour
        int seconds = Mathf.FloorToInt(generalStatistics.ElapsedTime % 60); // calculate seconds
        time.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        travelDistance.text = generalStatistics.TravelDistance.ToString();
        accuracy.text = generalStatistics.Accuracy.ToString();
        kdr.text = generalStatistics.KillDeathRatio().ToString();
        death.text = generalStatistics.Death.ToString();
        kill.text = generalStatistics.Kill.ToString();
    }
}