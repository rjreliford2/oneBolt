using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TotalTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string temp = "";
        if (PlayerPrefs.GetInt("oneRun", 0) == 1 ? true : false)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("totalTime"));
            temp = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        }
        GetComponent<TextMeshProUGUI>().text = temp + "\n\n Thank you for playing!\r\nHead back for One Bolt Two Bolt Dead Bolt You Bolt the sequal of One Bolt at a later date";
    }

   
}
