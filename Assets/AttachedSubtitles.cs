using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;

namespace SubtitleSystem
{
    public class AttachedSubtitles : MonoBehaviour
    {
        public Boolean triggeredOnce;
        public TextMeshProUGUI subtitlesBox;
        public TextAsset subtitleFile;
        public Text subtitlesBox2;
        public TextAsset subtitleFile2;
        public SubtitleBase subBase;
        //replacement encapselating class like "Basic Subtitle" used to be
        public Boolean subtitlesTriggered;
        public Boolean repeated; //mark this as true in start if you want the diolouge to be repeated on each collision
        //https://answers.unity.com/questions/1255334/drawing-a-custom-graphics-line-between-two-objects.html
        public GameObject speaker;          // Reference to the first GameObject
        public GameObject player;
        private LineRenderer line;
        public GameObject mcm;
        //i guess 3d planes, like ui canvases maybe, with a line drawn on them might ork if there isnt a more intuitive soltuion
        // Start is called before the first frame update

        //saving this functionallity for later, especially since i'm a bti confused about it
        //checks to see if the player can see the speaker
        //have silloette

        void Start()
        {
            mcm = GameObject.Find("Main Camera");
            triggeredOnce = false;
            repeated = false;
            player = GameObject.Find("player");
            subBase = new SubtitleBase(subtitlesBox, subtitleFile);
            subtitlesTriggered = false;
            // Add a Line Renderer to the GameObject
            line = this.gameObject.AddComponent<LineRenderer>();
            // Set the width of the Line Renderer
            line.startWidth = 0.05F;
            line.endWidth = 0.05F;
            // Set the number of vertex fo the Line Renderer
            line.positionCount = 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((!triggeredOnce || repeated) && !mcm.GetComponent<Main>().subtitlesOn)
            {
                subBase.assignDict();//thhis is only here bc for some reason subtitle base runs Before main??
                triggeredOnce = true;
                subtitlesTriggered = true;
                mcm.GetComponent<Main>().subtitlesOn = true;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (subtitlesTriggered)
            {
                //fix lag in speaker names and movement
                

                subBase.UpdateSubtitleBase();
                speaker = GameObject.Find(subBase.subtitleReader.getSpeaker());

                if (player != null && speaker != null)
                {
                    //i could seperate each kind of subtitles into classes, for user readability, but it seems more convent for the user to _use_ if they're in one class, where you can _pick_ the features
                    Boolean AssignCol = GameObject.Find("Main Camera").GetComponent<Main>().assignedSpeakerColors;//have a final variable so ppl can chpoose the nam of their naim object?
                    if (AssignCol)
                    {
                        //715-736 am 8-2-201`
                        //946 - 1128am out
                        //1237pm in 
                        string speakerString = subBase.subtitleReader.getSpeaker();
                        Color speakerColor = subBase.tempUselessDictionary[speakerString];
                        float alpha = 1.0f;
                        Gradient gradient = new Gradient();
                        gradient.SetKeys(
                            new GradientColorKey[] { new GradientColorKey(speakerColor, 0.0f), new GradientColorKey(speakerColor, 1.0f) },
                            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
                        );
                        line.colorGradient = gradient;
                        line.material = new Material(Shader.Find("Sprites/Default"));
                        line.endColor = speakerColor;
                        line.startColor = speakerColor;
                    }
                    line.startWidth = 0.05F;
                    line.endWidth = 0.05F;
                    // Update position of the two vertex of the Line Renderer
                    line.SetPosition(0, speaker.transform.position);
                    line.SetPosition(1, player.transform.position);
                    
                }
                else
                {
                    
                    line.startWidth = 0.0F;
                    line.endWidth = 0.0F;
                    
                }

                if (subBase.subtitleReader.shouldProgramEnd())
                {
                    speaker = null;
                    subtitlesTriggered = false;
                    line.startWidth = 0.0F;
                    line.endWidth = 0.0F;

                }
            }
            
        }
        // I referenced the line code from here: 
        //https://answers.unity.com/questions/1255334/drawing-a-custom-graphics-line-between-two-objects.html

    }
}
