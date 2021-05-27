using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighlightTextUnderlay : MonoBehaviour
{
    public GameObject thisObject;
    public TextMeshProUGUI textHere;
    public TextMeshProUGUI mainSubtitleBox;
    public GameObject main;
    // Update is called once per frame
    void Start()
    {
        main = GameObject.Find("Main Camera");
        thisObject = gameObject;
        textHere = thisObject.GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
        textHere.text = "<mark =#00000000>" + mainSubtitleBox.text + "</mark>";
        
    }
}