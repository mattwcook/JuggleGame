using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverUi;
    [SerializeField] TMP_Text gameOverNumBallsText;
    [SerializeField] GameObject newHighScoreText;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text currentScoreText;
    [SerializeField] GameObject inGameUi;
    [SerializeField] ArcadeScoreKeeper scoreKeeper;
    [SerializeField] MusicController musicController;
    [SerializeField] TMP_Dropdown numBallsDropdown;
    bool alreadyDone = false;

    private void Awake()
    {
        //CustomEvents.onGameOver += GameOver;
        CustomEvents.onDelayedGameOver += DelayedGameOver;
    }
    public void GameOver()
    {
        if (alreadyDone == false)
        {
            PopulateGameOverUI();
            musicController.GameOver();
            alreadyDone = true;
            CustomEvents.GameOver();
        }
    }
    public void DelayedGameOver(float delayTime)
    {
        CustomEvents.GameOver();
        musicController.StopMusic();
        alreadyDone = true;
        StartCoroutine(DelayUIOpen(delayTime));
    }
    IEnumerator DelayUIOpen(float delay)
    {
        yield return new WaitForSeconds(delay);
        musicController.GameOver();
        PopulateGameOverUI();
    }
    bool CheckHighScore()
    {
        int score = scoreKeeper.GetScore();
        int highScore = scoreKeeper.GetHighScore();
        if (score > highScore)
        {
            PlayerPrefs.SetInt(SaveKeys.bestArcadeScoreBase + SettingsScript.maxBalls, score);
            return true;
        }
        return false;
    }
    void PopulateGameOverUI()
    {
        gameOverUi.SetActive(true);
        inGameUi.SetActive(false);
        newHighScoreText.SetActive(CheckHighScore());
        gameOverNumBallsText.text = SettingsScript.maxBalls + " Balls";
        highScoreText.text = "High Score: " + scoreKeeper.GetHighScore();
        currentScoreText.text = "Score: " + scoreKeeper.GetScore();
        numBallsDropdown.SetValueWithoutNotify(SettingsScript.maxBalls - 1);
    }
    private void OnDestroy()
    {
        //CustomEvents.onGameOver -= GameOver;
        CustomEvents.onDelayedGameOver -= DelayedGameOver;
    }
}
