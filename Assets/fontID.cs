using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;

namespace SubtitleSystem
{
    public class fontID
    {

        public int fontSize;
        public TMP_FontAsset fontFont;
        public string fontName;
        // Start is called before the first frame update
        public fontID(TMP_FontAsset font, string name)
        {
            fontSize = 60; //size isnt automatically defined bec its igonna take some fiddling, have it in a second line to make this more obvious to users?
            fontFont = font;
            fontName = name;
        }
    }
}