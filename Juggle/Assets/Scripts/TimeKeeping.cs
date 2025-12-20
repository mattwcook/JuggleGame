using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeKeeping : MonoBehaviour
{
    float time = 0;
    float lastTime = 0;
    float bestTime = 0;
    [SerializeField] TMP_Text bestTimeText;
    [SerializeField] TMP_Text lastTimeText;
    [SerializeField] TMP_Text currentTimeText;
    bool timerRunning;

    private void Start()
    {
        if (PlayerPrefs.HasKey(SaveKeys.bestCasualTimeBase + SettingsScript.maxBalls))
        {
            bestTime = PlayerPrefs.GetFloat(SaveKeys.bestCasualTimeBase + SettingsScript.maxBalls);
        }
        if (bestTimeText != null)
        {
            bestTimeText.text = "Best Time: " + bestTime.ToString(".00");
        }
    }
    public void StartTimer()
    {
        timerRunning = true;
    }
    public void StopTimer()
    {
        lastTime = time;
        if (timerRunning == true)
        {
            lastTimeText.text = "Last Time: " + lastTime.ToString(".00");
        }
        timerRunning = false;
        time = 0;
        currentTimeText.text = "Current Time: 0.00";
    }
    public float GetTime()
    {
        return time;
    }
    public float GetLastTime()
    {
        return lastTime;
    }
    public bool IsTimeRunning()
    {
        return timerRunning;
    }
    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            time += Time.deltaTime;
            currentTimeText.text = "Current Time: " + time.ToString(".00");

            if(time > bestTime && bestTimeText != null)
            {
                bestTime = time;
                PlayerPrefs.SetFloat(SaveKeys.bestCasualTimeBase + SettingsScript.maxBalls, bestTime);
                bestTimeText.text = "Best Time: " + bestTime.ToString(".00");
            }
        }
    }
}
