using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TMP_Text))]
public class FontMatch : MonoBehaviour
{
    [SerializeField] TMP_Text fontToMatch;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TMP_Text>().fontSize = fontToMatch.fontSize;
    }

}
