using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SubtitleSystem
{

    //to switch between types of subtitles, do we want a checkbox, draghgng a script, a string, or something else?
    //bc that way ppl could easily edit in the inspector, without getting into the code
    //infok: you can totally copy over a whole trigger object, with the acruipt, and if your want just change the subtitle oc
    //or have the same of course
    //iwould want to make a prefab for the triggeer object

    public class TriggerSubtitles : MonoBehaviour
    {
        public Boolean triggeredOnce;
        public TextMeshProUGUI subtitlesBox;
        public TextAsset subtitleFile;
        public GameObject mcam;
        public SubtitleBase subBase;
        //replacement encapselating class like "Basic Subtitle" used to be
        public Boolean subtitlesTriggered;
        public Boolean repeated; //mark this as true in start if you want the diolouge to be repeated on each collision
        
        // Start is called before the first frame update
        void Start()
        {
            triggeredOnce = false;
            repeated = false;
            subBase = new SubtitleBase(subtitlesBox, subtitleFile);
            subtitlesTriggered = false;
            mcam = GameObject.Find("Main Camera");
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((!triggeredOnce || repeated) && !mcam.GetComponent<Main>().subtitlesOn)
            {
                subBase.assignDict();//thhis is only here bc for some reason subtitle base runs Before main??
                triggeredOnce = true;
                subtitlesTriggered = true;
                mcam.GetComponent<Main>().subtitlesOn = true;
            }
        }
        // Update is called once per frame
        //is this the most time-efficent way to run something once its trigger
        //try to have a lot of the startup loaded on startup, so the subttiles trigger quickly when you cross them
        //should i nit seperate the functionality of update and on trigger eneter?
        void Update()
        {
            if (subtitlesTriggered)
            {
                subBase.UpdateSubtitleBase();
                if (subBase.subtitleReader.shouldProgramEnd()) {
                    subtitlesTriggered = false;
                }
            }
        }
    }
}