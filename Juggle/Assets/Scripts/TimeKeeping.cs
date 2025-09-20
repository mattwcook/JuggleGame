using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeKeeping : MonoBehaviour
{
    float time = 0;
    float bestTime = 0;
    [SerializeField] TMP_Text bestTimeText;
    [SerializeField] TMP_Text lastTime;
    [SerializeField] TMP_Text currentTimeText;
    bool timerRunning;

    private void Start()
    {
        if (PlayerPrefs.HasKey(SaveKeys.bestTimeBase + SettingsScript.maxBalls))
        {
            bestTime = PlayerPrefs.GetFloat(SaveKeys.bestTimeBase + SettingsScript.maxBalls);
        }
        bestTimeText.text = "Best Time: " + bestTime.ToString(".00");
    }
    public void StartTimer()
    {
        timerRunning = true;
    }
    public void StopTimer()
    {
        timerRunning = false;
        lastTime.text = "Last Time: " + time.ToString(".00");
        time = 0;
        currentTimeText.text = "Current Time: 0.00";
    }
    

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            time += Time.deltaTime;
            currentTimeText.text = "Current Time: " + time.ToString(".00");

            if(time > bestTime)
            {
                bestTime = time;
                PlayerPrefs.SetFloat(SaveKeys.bestTimeBase + SettingsScript.maxBalls, bestTime);
                bestTimeText.text = "Best Time: " + bestTime.ToString(".00");
            }
        }
    }
}
