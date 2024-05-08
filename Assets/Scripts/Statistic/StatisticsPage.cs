using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticsPage : MonoBehaviour
{
    private GeneralStatistics generalStatistics;


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
        GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        GetComponent<TextMeshProUGUI>().text = generalStatistics.TravelDistance.ToString();
        GetComponent<TextMeshProUGUI>().text = generalStatistics.Accuracy.ToString();
        GetComponent<TextMeshProUGUI>().text = generalStatistics.KillDeath.ToString();
        GetComponent<TextMeshProUGUI>().text = generalStatistics.Death.ToString();
        GetComponent<TextMeshProUGUI>().text = generalStatistics.Kill.ToString();
    }
}