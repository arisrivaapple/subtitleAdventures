using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SubtitleSystem
{
    public class TextSize : MonoBehaviour
    {   
        //ive been having toruble assign main through find object
        public GameObject main;
        public GameObject thisObject;
        public TextMeshProUGUI textHere;
        // Update is called once per frame
        void Update()
        {
            main = GameObject.Find("Main Camera");
            thisObject = gameObject;
            textHere = thisObject.GetComponent<TextMeshProUGUI>();
            textHere.fontSize = main.GetComponent<Main>().subtitleFontSize;
        }
    }
}
