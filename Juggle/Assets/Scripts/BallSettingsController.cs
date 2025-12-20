using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BallSettingsController : MonoBehaviour
{
    [SerializeField] protected TMP_Dropdown numBallDropdown;
    public void SetNumberBalls(int numBallsIndex)
    {
        SettingsScript.maxBalls = int.Parse(numBallDropdown.options[numBallsIndex].text);
        PlayerPrefs.SetInt(SaveKeys.numBalls, SettingsScript.maxBalls);
    }
    public void SetNumberBalls()
    {
        SettingsScript.maxBalls = int.Parse(numBallDropdown.options[numBallDropdown.value].text);
        PlayerPrefs.SetInt(SaveKeys.numBalls, SettingsScript.maxBalls);
    }
}
