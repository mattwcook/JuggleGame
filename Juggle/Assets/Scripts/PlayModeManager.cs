using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayModeManager : MonoBehaviour
{
    [SerializeField] TMP_Text modeText;
    [SerializeField] SceneChanger sceneChanger;
    string[] modes = new string[] { "Casual", "Arcade", "Flow" };
    int currentMode = 0;
    Dictionary<int, Color> modeBackgroundColors = new Dictionary<int, Color>()
    {
        {0, new Color(49,77,121)/255.0f },
        {1, new Color(121,49,62)/255.0f },
        {2, new Color(50, 121,110)/255.0f }
    };
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(SaveKeys.lastGameMode))
        {
            currentMode = PlayerPrefs.GetInt(SaveKeys.lastGameMode);
            
        }
        UpdateDisplay();
    }
    public void StartPressed()
    {
        SaveMode();
        sceneChanger.LoadScene(currentMode + 1);
    }

    public void NextMode()
    {
        currentMode = (currentMode + 1) % modes.Length;
        UpdateDisplay();
    }
    public void PreviousMode()
    {
        currentMode = (currentMode + 1) % modes.Length;
        UpdateDisplay();
    }
    void UpdateDisplay()
    {
        modeText.text = modes[currentMode];
        Camera.main.backgroundColor = modeBackgroundColors[currentMode];
    }
    public void SaveMode()
    {
        PlayerPrefs.SetInt(SaveKeys.lastGameMode, currentMode);
    }
}
