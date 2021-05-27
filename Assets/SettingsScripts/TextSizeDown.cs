using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SubtitleSystem
{
    public class TextSizeDown : MonoBehaviour
    {
        public GameObject main;
        public TextMeshProUGUI subtitlesBox;
        public TextMeshPro textMeshPro;

        public void subtractTextSize()
        {
            main.GetComponent<Main>().subtitleFontSizeDif--;
            //subtitlesBox.fontSize = main.GetComponent<Main>().subtitleFontSize;
        }
    }
}

