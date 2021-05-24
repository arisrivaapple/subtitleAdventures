using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace SubtitleSystem
{
    public class Main : MonoBehaviour
    {
        //the parent track of who the speaker is is kept here--when the multi-sepaker functionallity is implemented, this will differne,t, perhas with the individual triggers keeping either speaker or null themselves
        public GameObject MainSpeaker;
        public Boolean assignedSpeakerColors;
        public Boolean speakerNamesOptional; //i want to find a way to have the toggle system activated through this class so its easier to implement
        public Boolean speakerNames;
        public Boolean showCompass;
        public Boolean sameSubtitleStyle;
        public Boolean attachSubtitles;
        public Boolean showSpeakerNames;
        public Boolean dragSubtitles;
        public Boolean highlightSpeaker;
        public Boolean showSpeakerColors;
        public Boolean speechBubbles;
        public Boolean slowGameWhenNotLookingAtSpeaker;
        public Boolean subtitleBackground;
        public Boolean silhouettes;
        public int subtitleFontSize;
        public GameObject subtitleCanvasObject;
        public Canvas subtitleDisplay;
        public Dictionary<string, Color> speakerColors;
        public TextAsset speakerColorDoc;
        public SpeakerColorParser colorParser;//since th enumber and names of the speakers are defned once the dc is processed
        //i ccant find a way that people manually assign htme\//except if ppl want t do it in the code
        // Start is called before the first frame update
        public Boolean subtitlesOn;
        void Start()
        {
            subtitleBackground = true;
            subtitleFontSize = 60;
            assignedSpeakerColors = true;
            subtitlesOn = false;
            //assigned colors speakers is a setting for the whole scene, but it can easily be set at the beuinning of a specfiic subtitle trigger (should i put the variable right in to the triggersS?)
            colorParser = new SpeakerColorParser();
            if (assignedSpeakerColors)
            {
                speakerColors = colorParser.parse(speakerColorDoc);
            }
            speakerNames = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void adjustFontSizeUp()
        {

        }

        public void adjustFontSizeDown()
        {

        }
    }
}