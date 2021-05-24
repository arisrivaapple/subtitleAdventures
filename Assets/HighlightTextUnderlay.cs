using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighlightTextUnderlay : MonoBehaviour
{
    public GameObject thisObject;
    public TextMeshProUGUI textHere;
    public TextMeshProUGUI mainSubtitleBox;
    // Update is called once per frame
    void Start()
    {
        thisObject = gameObject;
        textHere = thisObject.GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        textHere.text = "<mark =#00000000>" + mainSubtitleBox.text + "</mark>";
        
    }
}