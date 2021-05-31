using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Diagnostics;

namespace SubtitleSystem
{
    public class SpeakerColorParser
    {
        //i 
        // Start is called before the first frame update
        //the c=goal is to be able to let the user update  the colors from the inspector
        //if they didnt assign them in the text file
        void Start()
        {
        }

        public Dictionary<string, Color> parse(TextAsset txt)
        {
            Dictionary<string, Color> speakersColors = new Dictionary<string, Color>();
            string text = txt.text;
            string[] seperator = { "\r\n", "\n" };
            string[] lines = (text.ToString()).Split(seperator, StringSplitOptions.None);
            Regex speakerWithColorAssigned = new Regex(@".* = .*");
            for (int i = 0; i < lines.Length; i++) { // is this flow proper? feels a li weird
                int equalsSignIndex = -1;

                if (!speakerWithColorAssigned.IsMatch(lines[i])) 
                {
                    throw new System.InvalidOperationException("Text not in correct format for speaker color assignments. Please refer to API chapter Speaker Color.");
                } //should i just have the brackets around a speaker so i can help ppl check if they have the correct format?
            
                for (int j = 0; j < lines[i].Length; j++)
                {
                    //reminder to add = to chracters that need a \ to be read regularly
                    if (lines[i][j] == '=')
                    {
                        equalsSignIndex = j;
                    }
                }
                    
                string sideSpeaker = lines[i].Substring(0, equalsSignIndex - 1);
                string[] seperator2 = { ", " };
                string[] colors = (lines[i].ToString()).Split(seperator2, StringSplitOptions.None);
                if (equalsSignIndex == -1)
                {
                    throw new System.InvalidOperationException("Text not in correct format.");
                }
                float color1 = float.Parse(colors[0].Substring(equalsSignIndex + 3, colors[0].Length - (equalsSignIndex + 3)));
                float color2 = float.Parse(colors[1].Substring(0, colors[1].Length));
                float color3 = float.Parse(colors[2].Substring(0, colors[2].Length - 1));
                Color color = new Color(color1, color2, color3);
                speakersColors.Add(sideSpeaker, color);
             
            }
            return speakersColors;
        }

    }
}
