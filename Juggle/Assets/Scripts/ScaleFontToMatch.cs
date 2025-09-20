using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScaleFontToMatch : MonoBehaviour
{
    
    [SerializeField] TMP_Text[] textsToMatch;
    string[] previousTexts;
    float minPossibleFontSize = float.PositiveInfinity;
    float maxPossibleFontSize = float.NegativeInfinity;
    float fontResolution = .05f;
    IEnumerator processingDelay;
    private void Awake()
    {
        InitializePreviousTexts();
        foreach (TMP_Text text in textsToMatch)
        {
            text.enableAutoSizing = false;
            if(text.fontSizeMin < minPossibleFontSize)
            {
                minPossibleFontSize = text.fontSizeMin;
            }
            if (text.fontSizeMax > maxPossibleFontSize)
            {
                maxPossibleFontSize = text.fontSizeMax;
            }
        }
        //TMPro_EventManager.TEXT_CHANGED_EVENT.Add(CheckTextUpdated);
        SetFontSize(GetMinFont());

    }
    
    private void OnEnable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(CheckTextUpdated);
    }
    private void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(CheckTextUpdated);
    }
    private void OnDestroy()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(CheckTextUpdated);
    }
    float GetMinFont()
    {
        float minFont = float.PositiveInfinity;
        foreach(TMP_Text text in textsToMatch)
        {
            if (text.gameObject.activeInHierarchy && text.text.Length > 0)
            {
                text.enableAutoSizing = true;
                text.ForceMeshUpdate();
                if (text.fontSize < minFont)
                {
                    minFont = text.fontSize;
                }
                text.enableAutoSizing = false;
            }
        }
        return minFont;
    }

    /*
    float GetMinFont()
    {
        float minFont = maxPossibleFontSize;
        foreach (TMP_Text text in textsToMatch)
        {
            if (text.gameObject.activeInHierarchy == false)
            {
                continue;
            }
            RectTransform rectTransform = text.GetComponent<RectTransform>();
            float fontSize = minFont;
            Debug.Log(text.transform.parent.name + " Initialize Font to: " + fontSize);
            while (fontSize >= minPossibleFontSize)
            {
                float width = rectTransform.rect.width;
                float height = rectTransform.rect.height;
                text.fontSize = fontSize;
                text.ForceMeshUpdate();
                Debug.Log("Update Size: " + fontSize);
                if (text.preferredWidth <= width && text.preferredHeight <= height)
                {
                    // Text fits within RectTransform
                    Debug.Log("Fits");
                    break;
                }
                else
                {
                    Debug.Log("Still Too Big");
                    fontSize -= fontResolution;
                    //break;
                }
            }
            Debug.Log("Largest Font for text: " + fontSize);
            if (fontSize < minFont)
            {
                Debug.Log("New Smallest");
                minFont = fontSize;
            }
        }
        return minFont;
    }
    */
    float TextWidthApproximation(string text, TMP_FontAsset fontAsset, float fontSize, FontStyles style)
    {
        // Compute scale of the target point size relative to the sampling point size of the font asset.
        float pointSizeScale = fontSize / (fontAsset.faceInfo.pointSize * fontAsset.faceInfo.scale);
        float emScale = fontSize * 0.01f;

        float styleSpacingAdjustment = (style & FontStyles.Bold) == FontStyles.Bold ? fontAsset.boldSpacing : 0;
        float normalSpacingAdjustment = fontAsset.normalSpacingOffset;

        float width = 0;

        for (int i = 0; i < text.Length; i++)
        {
            char unicode = text[i];
            TMP_Character character;
            // Make sure the given unicode exists in the font asset.
            if (fontAsset.characterLookupTable.TryGetValue(unicode, out character))
                width += character.glyph.metrics.horizontalAdvance * pointSizeScale + (styleSpacingAdjustment + normalSpacingAdjustment) * emScale;
        }

        return width;
    }
    void SetFontSize(float fontSize)
    {
        foreach (TMP_Text text in textsToMatch)
        {
            text.fontSize = fontSize;
            if (text.gameObject.activeInHierarchy == true && text.text.Length > 0)
            {
                text.ForceMeshUpdate();
            }
        }
    }
    private void InitializePreviousTexts()
    {
        if (previousTexts == null)
        {
            previousTexts = new string[textsToMatch.Length];
            for (int i = 0; i < textsToMatch.Length; i++)
            {
                previousTexts[i] = textsToMatch[i].text;
            }
        }
    }
    void CheckTextUpdated(object changedTextObj)
    {
        //Debug.Log("CheckTextUpdated: " + gameObject.name);
        bool changed = false;
        TMP_Text text;
        InitializePreviousTexts();
        for (int i = 0; i < textsToMatch.Length; i++)
        {
            text = textsToMatch[i];
            if ((object)text == changedTextObj && text.text != previousTexts[i])
            {
                changed = true;
                previousTexts[i] = text.text;
                break;
            }
        }
        if (changed == true)
        {
            //Debug.Log("Text Updated " + changedTextObj);
            TextUpdated();
        }
    }
    void TextUpdated()
    {
        
        //TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(CheckTextUpdated);
        
        if (processingDelay == null)
        {
            processingDelay = ReenableListenerDelay();
            StartCoroutine(processingDelay);
        }

        
    }
    IEnumerator ReenableListenerDelay()
    {
        yield return new WaitForEndOfFrame();
        //foreach (TMP_Text text in textsToMatch)
        //{
        //    text.ForceMeshUpdate();
        //
        //}
        //TMPro_EventManager.TEXT_CHANGED_EVENT.Add(CheckTextUpdated);
        SetFontSize(GetMinFont());
        processingDelay = null;
    }





    /*
    bool yeahIAlreadyFuckingHeard = false;
    // Start is called before the first frame update
    void Start()
    {
        //TextUpdated();
    }
    private void OnEnable()
    {
        Debug.Log("OnEnable: Add Listener");
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(CheckTextUpdated);
    }
    private void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(CheckTextUpdated);
    }
    private void OnDestroy()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(CheckTextUpdated);
    }
    public void MatchTexts()
    {
        
        StartCoroutine(Match());

    }
    IEnumerator Match()
    {
        Debug.Log("Match");
        float smallestFontSize = float.PositiveInfinity;
        foreach (TMP_Text text in textsToMatch)
        {
            if (text.gameObject.activeInHierarchy == true)
            {
                text.enableAutoSizing = true;
            }
        }
        yield return new WaitForEndOfFrame();
        foreach (TMP_Text text in textsToMatch)
        {
            Debug.Log("Look For Smallest");
            if (text.gameObject.activeInHierarchy == true && text.fontSize < smallestFontSize)
            {
                smallestFontSize = text.fontSize;
                Debug.Log("Smaller Found: " +text.gameObject.name + " - " + smallestFontSize);
            }
        }
        foreach (TMP_Text text in textsToMatch)
        {
            if (text.gameObject.activeInHierarchy == true)
            {
                text.enableAutoSizing = false;
                text.fontSize = smallestFontSize;
            }
        }
        //yeahIAlreadyFuckingHeard = false;
        yield return new WaitForEndOfFrame();

        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(CheckTextUpdated);
        Debug.Log("Finished Matching: Add Listener");
    }
    void CheckTextUpdated(object changedTextObj)
    {
        bool contains = false;
        foreach (TMP_Text text in textsToMatch)
        {
            if ((object)text == changedTextObj)
            {
                contains = true;
                break;
            }
        }
        if (contains == true)
        {
            //Debug.Log("Text Updated " + changedTextObj);
            TextUpdated();
        }
    }
    public void TextUpdated()
    {
        Debug.Log("Text Updated: Remove Listener");
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(CheckTextUpdated);
        MatchTexts();
        //if (yeahIAlreadyFuckingHeard == false)
        //{
        //    yeahIAlreadyFuckingHeard = true;
        //    StartCoroutine(GiveAllTextsAChanceToUpdate());
        //}
    }
    IEnumerator GiveAllTextsAChanceToUpdate()
    {
        yield return new WaitForEndOfFrame();
        MatchTexts();
        
    }
    */

}
