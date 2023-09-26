using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeText;

    [SerializeField]
    private TextMeshProUGUI dayText;

    // Update is called once per frame
    void Update()
    {
        UpdateTimeOfDay();
    }

    private void UpdateTimeOfDay()
    {
        if (timeText != null)
        {
            float timeInHours = LightingManager.TimeOfDay;
            int hours = Mathf.FloorToInt(timeInHours);
            int minutes = Mathf.FloorToInt((timeInHours - hours) * 60);

            string formattedTime = hours.ToString("00") + ":" + minutes.ToString("00");
            string formattedDay = LightingManager.Day.ToString(); // Fixed this line

            timeText.text = formattedTime;
            dayText.text = "Day " + formattedDay; // Set the day text
        }
    }
}

