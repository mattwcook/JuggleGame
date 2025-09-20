using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SetFontsToMatch : MonoBehaviour
{
    [SerializeField] TMP_Text longestStringText;
    [SerializeField] TMP_Text[] textsToMatchLongest;
    [SerializeField] [Range(0, 1.0f)] float fractionOfLargestSize = .9f;

    // Start is called before the first frame update
    void Start()
    {
        longestStringText.enableAutoSizing = true;
        longestStringText.ForceMeshUpdate();
        float fontSize = longestStringText.fontSize * fractionOfLargestSize;
        longestStringText.enableAutoSizing = false;
        longestStringText.fontSize = fontSize;
        foreach (TMP_Text t in textsToMatchLongest)
        {
            t.enableAutoSizing = false;
            t.fontSize = fontSize;
        }
    }


}
