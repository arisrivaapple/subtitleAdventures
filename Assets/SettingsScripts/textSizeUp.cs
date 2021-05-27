using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SubtitleSystem
{
    public class textSizeUp : MonoBehaviour
    {
        public GameObject main;
        public TextMeshProUGUI subtitlesBox;
        public TextMeshPro textMeshPro;
        

        public void addTextSize()
        {
            main.GetComponent<Main>().subtitleFontSizeDif++;
            //subtitlesBox.fontSize = main.GetComponent<Main>().subtitleFontSize;
        }
    }
}
