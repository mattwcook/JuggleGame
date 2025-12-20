using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArcadeScoreKeeper : MonoBehaviour
{
    public TMP_Text bestScore;
    public TMP_Text currentScore;
    public TMP_Text gameOverCurrent;
    public TMP_Text gameOverBest;
    int score = 0;
    string saveKey;
    // Start is called before the first frame update
    void Start()
    {
        
        bestScore.text = "Best Score: " + GetHighScore(); 

        CustomEvents.onAddPoints += AddScore;
        
    }
    void AddScore(int points)
    {
        score += points;
        currentScore.text = "Current Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }
    public int GetHighScore()
    {
        saveKey = SaveKeys.bestArcadeScoreBase + SettingsScript.maxBalls;
        if (PlayerPrefs.HasKey(saveKey))
        {
            return PlayerPrefs.GetInt(saveKey);
        }
        else
        {
            return 0;
        }
    }
    private void OnDestroy()
    {
        CustomEvents.onAddPoints -= AddScore;
    }




}
