using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using System.Diagnostics;
using TMPro;

//im unsure whether its better form to keep the scope of the variables smaller and incorporate the like arrows and compass in here, 
//or if i sshould have that stuff in seperate scripts
//i think im supposed to create a function that will share the necessasry info with y other scripts
//but i should check


//to add: be able to move subtitles around (for blind spots and stuff)
//change text
//speed down speech too long with subtitles, without making soeech incomprehensivble???

//maybeb a should include having sound come from the assigned gameobject speaker spoemwherre in here, though it's not my first priority
namespace SubtitleSystem
{
    public class AngleSubtitles : MonoBehaviour //hopefully i can
    {
        //i probably shouldnt have all my variables public, but in unity its useful for debugging
        //what should i change in the release version?
        //ive always had a bit of toruble with scope
        //shouof check the scope of everythign to seee that nothing is brpader scope than it needs to be so it doesnt interfere t=with the user's rpogram
        private LineRenderer line;
        public TextMeshProUGUI speakerAngleText, playerYAngleText, totalFinalAngleText, fillCircleConvertAngleText;
        public GameObject leftArrow;
        public GameObject rightArrow;
        public Boolean speakerArrows;
        public double internalTime;
        public Boolean triggeredOnce;
        public TextMeshProUGUI subtitlesBox;
        public GameObject Compass;
        public Vector3 compassCurrentEulerAngles;
        public Quaternion compassCurrentRotation;
        public GameObject Compass2;
        public Vector3 compassCurrentEulerAngles2;
        public Quaternion compassCurrentRotation2;
        public GameObject Compass3;
        public Vector3 compassCurrentEulerAngles3;
        public Quaternion compassCurrentRotation3;//can get a similar arrow thing by getting directions of raycast..
        public TextAsset subtitleFile;
        public SubtitleBase subBase;
        public float justSpeakerAngle;
        public float defaultFloat;
        //replacement encapselating class like "Basic Subtitle" used to be
        public Boolean subtitlesTriggered;
        public Boolean repeated; //mark this as true in start if you want the diolouge to be repeated on each collision
        //there could also be an option where ppl didnt actuly move their speech until you foud them. t could be kinda a cool thing if you had super powers or something
        //a big question is what to do if more than one person is speaking
        //should the text speed wait 'till you've looked at both of them?
        public Quaternion speakerCurrentRotation;
        public Vector3 speakerCurrentEulerAngles;
        public GameObject player;
        public GameObject Speaker;
        public float playerAngle;
        public Boolean speakerFaced;
        public float speakerAngle;
        public float speakerZ;
        public float speakerX;
        public float playerZ;
        public GameObject mostRecentSpeakerLight;
        public GameObject speechBubble;
        public float playerX;
        public GameObject textBackground;
        public GameObject mostRecentSpeaker;
        public Boolean needToSeeSpeaker;
        public Material silhouetteMaterial;
        public Material nonSilhouetteMaterial;
        //measured in degrees away from facing the speaker straight on are considered "facing the speaker"
        //it would be possible for the player's view to snap towards the speaker
        //also, maybe it would be beneficial for the speaaker to glow
        //(it would be helpful for location)
        //but also once you were facing them, you could be sure you knew who was talking
        public double internalTimeRate;
        //the name o fthe person talking
        //null is used to show that no one is talking
        //if you want to show the speaker's name in the subititles, you write theyre name twice
        public string speakerTag;
        public GameObject mainCamr;
        //I definately want to see if there's a better way to do this, but for now we're tracking
        //the angles of the player n a seperate field
        //so we can see what direction the player is facing
        //for finding if the speaker is withing the allowance
        //not that this is in this part of the code--im just noting that we need to do that
        //bc yeah there might be an eaasier way to do this
        //but for now ill use trig to calcualte the angle a speaker is from the plarer
        //using the difference in positions between the player and speaker
        /// will we be okay instantiating one of these for every set of subtitles?
        
        public float allowance;
        public Collider speakerCollider;
        public RaycastHit targetHit;
        public Vector3 playerForward;
        public float x;
        public float y;

        //again it seems there should be a bteeer why t locate ui elemetns than dputting in  a game object
        public GameObject subtitleCanvasObject;
        public Canvas subtitleDisplay;

        //get rid of lingering text

        void Start()
        {
            Speaker = player;
            subtitleDisplay = GameObject.Find("Main Camera").GetComponent<Main>().subtitleDisplay;
            //way to get arrow without assigning in inspector?
            speakerArrows = true;
            mainCamr = GameObject.Find("Main Camera");
            triggeredOnce = false;
            repeated = false;
            subtitlesTriggered = false;
            player = GameObject.Find("player");
            Speaker = player;
            //HERE THE CODE SHOULD BE EDITED DEPENDING ON WHAT PLAYER MOVEMENT YOU USE
            //"plsyerAngle" SHOULD CONTAIN THE PLAYERS ROTATION AORUND THE Y AXIS IN DEGREES
            //for our functionality: dont forget to write the code to keep our playerRotstion w=under 360 degrees and correctlky formatted
            playerAngle = GameObject.Find("player").GetComponent<MoveRobot>().playerYAngle;//will this update as the variable updates?
            //i dont think so
            //decide if we want another class for the basic sbtitle displaer stuff
            ///we assume that initially the player isnt facing the speaker
            mostRecentSpeaker = player; //still feels like weird "fix"
            speakerFaced = false;
            speakerTag = "";
            allowance = 35.0f;

            line = this.gameObject.AddComponent<LineRenderer>();
            // Set the width of the Line Renderer
            line.startWidth = 0.05F;
            line.endWidth = 0.05F;
            // Set the number of vertex fo the Line Renderer
            line.positionCount = 2;
        }

        void OnTriggerEnter(Collider other)
        {
            //wont except just a declaration in the main part of the class for some reason
            GameObject mainCamr = GameObject.Find("Main Camera");
            if (mainCamr != null)
            {
                //it keeps saying that main isnt declared for some reason
                //so im going to assign the thing to a boolean instead
                Boolean subs = !(mainCamr.GetComponent<Main>().subtitlesOn);
                if ((!triggeredOnce || repeated) && subs)
                {

                    subBase = new SubtitleBase(subtitlesBox, subtitleFile);
                    subBase.assignDict();
                    subBase.subtitleReader.setInternalTimerRate(mainCamr.GetComponent<Main>().nonSpeakerFacingSpeed);
                    subBase.assignDict();//thhis is only here bc for some reason subtitle base runs Before main??
                    triggeredOnce = true;
                    subtitlesTriggered = true;
                    mainCamr.GetComponent<Main>().subtitlesOn = true;
                }
            }
        }

        void Update()
        {
            getSpeakerAngle();
            if (mostRecentSpeaker != Speaker) 
            {
                showSilhouette(false);
            }
            if (subBase != null) //order of checking most recent and assigneing speaker conflicts?
            {
                subBase.subtitleReader.incrementTime();

                internalTimeRate = subBase.subtitleReader.getInternalTimerRate();
                internalTime = subBase.subtitleReader.getInternalTime();
                playerAngle = processAngle(GameObject.Find("player").GetComponent<MoveRobot>().playerYAngle);
                String speakerPos = playerViewContains(speakerTag, playerAngle);
                if (subtitlesTriggered)
                {
                    if (justSpeakerAngle != defaultFloat)
                    {
                        getSpeakerAngle();
                        x = Compass.transform.rotation.x;
                        y = Compass.transform.rotation.y;
                        //negative speaker agles should have negative compass angle too
                        //modifying the Vector3, based on input multiplied by speed and time
                        compassCurrentEulerAngles = new Vector3(x, y, justSpeakerAngle - player.GetComponent<MoveRobot>().playerYAngle);
                        //moving the value of the Vector3 into Quanternion.eulerAngle format
                        compassCurrentRotation.eulerAngles = compassCurrentEulerAngles; //threw an error?
                        //apply the Quaternion.eulerAngles change to the gameObject
                        Compass.transform.rotation = compassCurrentRotation;
                    }
                    if (mainCamr.GetComponent<Main>().attachSubtitles)
                    {
                        attachSubtitles();
                    }
                    subBase.UpdateSubtitleBase();
                    //do our counters and time adjustments match up?

                    speakerTag = subBase.subtitleReader.getSpeaker();//hmmmm
                    Speaker = GameObject.Find(speakerTag);
                    
                    if (mostRecentSpeaker != Speaker)
                    {
                        if (Speaker != null && (mainCamr.GetComponent<Main>().showSpeechBubble))
                        {
                            float x = Speaker.transform.position.x;
                            float y = Speaker.transform.position.y;
                            float z = Speaker.transform.position.z;
                            speechBubble.SetActive(true);
                            speechBubble.transform.position = new Vector3(x, y + 1.5f, z);

                            Vector3 currentEulerAngles;
                            Quaternion currentRotation;
                            x = Speaker.transform.rotation.x;
                            y = Speaker.transform.rotation.y;
                            z = Speaker.transform.rotation.z;

                            //modifying the Vector3, based on input multiplied by speed and time
                            speakerCurrentEulerAngles = new Vector3(0, 0, Compass.transform.rotation.z);//heck if theres initital rotation

                            //moving the value of the Vector3 into Quanternion.eulerAngle format
                            speakerCurrentRotation.eulerAngles = speakerCurrentEulerAngles;

                            //apply the Quaternion.eulerAngles change to the gameObject
                            speechBubble.transform.rotation = speakerCurrentRotation;
                        }
                        
                        showSilhouette(false);
                    }
                    if (Speaker != null)
                    {

                        if (mainCamr.GetComponent<Main>().showCompass)
                        {
                            Compass.SetActive(true);

                            x = Compass.transform.rotation.x;
                            y = Compass.transform.rotation.y;

                            //modifying the Vector3, based on input multiplied by speed and time
                            compassCurrentEulerAngles = new Vector3(x, y, -speakerAngle);
                            //moving the value of the Vector3 into Quanternion.eulerAngle format
                            compassCurrentRotation.eulerAngles = compassCurrentEulerAngles;

                            //apply the Quaternion.eulerAngles change to the gameObject
                            Compass.transform.rotation = compassCurrentRotation;

                            y = Compass2.transform.rotation.y;
                            compassCurrentEulerAngles2 = new Vector3(x, y, justSpeakerAngle);
                            //moving the value of the Vector3 into Quanternion.eulerAngle format
                            compassCurrentRotation2.eulerAngles = compassCurrentEulerAngles2;
                            //apply the Quaternion.eulerAngles change to the gameObject
                            Compass2.transform.rotation = compassCurrentRotation2;

                            y = Compass3.transform.rotation.y;
                            compassCurrentEulerAngles3 = new Vector3(x, y, player.GetComponent<MoveRobot>().playerYAngle);
                            //moving the value of the Vector3 into Quanternion.eulerAngle format
                            compassCurrentRotation3.eulerAngles = compassCurrentEulerAngles3;
                            //apply the Quaternion.eulerAngles change to the gameObject
                            Compass3.transform.rotation = compassCurrentRotation3;

                        }
                        mostRecentSpeaker = Speaker;
                    } //does it need to go after too?
                    if (Speaker == null)
                    {
                        speechBubble.SetActive(false);
                        rightArrow.SetActive(false);
                        leftArrow.SetActive(false);
                        speakerFaced = false;
                        //when there's a break in the subtittlwa, thew siubtitle speed speeds up to speakerFacingSpeed
                        subBase.subtitleReader.setInternalTimerRate(mainCamr.GetComponent<Main>().speakerFacingSpeed);
                        lightSpeaker(false);
                        changeText(false);
                        //Compass.SetActive(false);
                    }
                    else
                    {
                        //tmeo change to test raycast
                        //find a way to toggle the existence of th eif clause so this function can be turned on and off
                        //could use nested if statwements but think clauses are cleeaner'
                        //wpuld it make f=more sense just to have like seperate if clauses or a switch box for eahc setting?
                        if (mainCamr.GetComponent<Main>().silhouettes)
                        {
                            showSilhouette(true);
                        }
                        speakerPos = playerViewContains(speakerTag, playerAngle);
                        if (Equals("true", speakerPos) && (checkVisualSpeaker()))
                        {
                            speakerFaced = true;
                            double tempor = mainCamr.GetComponent<Main>().speakerFacingSpeed;
                            subBase.subtitleReader.setInternalTimerRate(mainCamr.GetComponent<Main>().speakerFacingSpeed);
                            rightArrow.SetActive(false);
                            leftArrow.SetActive(false);

                            lightSpeaker(true);
                            if (mainCamr.GetComponent<Main>().subtitleBackground) //maybe  change to baackground all the time when the setting is on?
                            {
                                changeText(true);
                            }
                            //this gives a void error sometimes???

                        }
                        else if (checkVisualSpeaker()) //im wondeing if you should only swith to spekwerFacingSpeed if you can see a significent percentage of the speaker
                                                       //i think that would be harder to code though
                        {
                            if (Equals("left", speakerPos))
                            {
                                rightArrow.SetActive(false);
                                leftArrow.SetActive(true);
                            }
                            else if (Equals("right", speakerPos))
                            {
                                rightArrow.SetActive(true);
                                leftArrow.SetActive(false);
                            }
                        }
                        else
                        {
                            if (!Equals("true", speakerPos))
                            {
                                speakerFaced = false;
                                //could just have a fiunction that did all the updates related to speakerFaced?
                                double tempor = mainCamr.GetComponent<Main>().nonSpeakerFacingSpeed;
                                subBase.subtitleReader.setInternalTimerRate(mainCamr.GetComponent<Main>().nonSpeakerFacingSpeed);
                                if (speakerArrows)
                                {
                                    if (Equals("left", speakerPos))
                                    {
                                        rightArrow.SetActive(false);
                                        leftArrow.SetActive(true);
                                    }
                                    else
                                    {
                                        rightArrow.SetActive(true);
                                        leftArrow.SetActive(false);
                                    }
                                }
                                lightSpeaker(false);
                                changeText(false);
                            }
                        }
                    }

                    if (subBase.subtitleReader.shouldProgramEnd())
                    {
                            //Compass.SetActive(false);
                            speakerFaced = false;
                        //could just have a fiunction that did all the updates related to speakerFaced?
                        double tempor = mainCamr.GetComponent<Main>().speakerFacingSpeed;
                        subBase.subtitleReader.setInternalTimerRate(mainCamr.GetComponent<Main>().speakerFacingSpeed);
                        Speaker = null;
                        speakerTag = "";
                        subtitlesTriggered = false;
                        lightSpeaker(false);
                        changeText(false);
                        subBase.subBox.text = "";
                        //i dont think i can set the whole script inactive...especially if its repeatable?
                    }
                }
            }
        }

        public void attachSubtitles()
        {
            if (player != null && Speaker != null)
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
                line.SetPosition(0, Speaker.transform.position);
                line.SetPosition(1, player.transform.position);

            }
            else
            {

                line.startWidth = 0.0F;
                line.endWidth = 0.0F;

            }

            if (subBase.subtitleReader.shouldProgramEnd())
            {
                line.startWidth = 0.0F;
                line.endWidth = 0.0F;

            }
        }

        //check spelling
        public void showSilhouette(Boolean setting)
        {
            if (setting) 
            {
                mostRecentSpeaker.GetComponent<Renderer>().material = silhouetteMaterial;
            } 
            else
            {
                mostRecentSpeaker.GetComponent<Renderer>().material = nonSilhouetteMaterial;
            }

        }

        public Boolean checkVisualSpeaker()
        {
            //i think givent he functionality i want, i can just replace playeranglecontains with raycast, plus add show soluette and i wont need to amake any tother changes
            if (needToSeeSpeaker)
            {
                if (doesRaycastHitSpeaker())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public float processAngle(float prevAngle)
        {
            float temp = (prevAngle + 90) % 360;
            if (temp >= 0)
            {
                return temp;
            }
            else
            {
                return 360 + temp;
            }
        }


        public float getSpeakerAngle()
        {
            if (Speaker != null) //it entered this if satemnt when it wasnt supposed to??!!
            {
                speakerZ = Speaker.transform.position.z;
                speakerX = Speaker.transform.position.x;
                playerZ = player.transform.position.z;
                playerX = player.transform.position.x;
                UnityEngine.Debug.Log(speakerAngle);
                //just walk through the math agnle
                //speaker angle is the nearest angle in this case
                justSpeakerAngle = -(180.0f / Mathf.PI) * (Mathf.Atan((speakerX - playerX) / (speakerZ - playerZ)));
                speakerAngleText.text = "og speaker angle: " + justSpeakerAngle;
                speakerAngle = -justSpeakerAngle + player.GetComponent<MoveRobot>().playerYAngle;
                playerYAngleText.text = ("play Y angle: " + player.GetComponent<MoveRobot>().playerYAngle);
                totalFinalAngleText.text = "speaker minus play Y angle: " + speakerAngle;
                return fullCircleConvert(speakerAngle);
            }
            return 0.0f;
        }

        //this seems to run before some of the update before this is called?
        public String playerViewContains(string speakerName, float playerAngle)
        {
            ///ugh!! i could include both of the answers in the stirng to return
            ///ibut i think the most elegant would be to include the compass shit in HERE
            ///GRar
            ///i mean what if we like seeprate this class into angle subtitles main setting class and helper classes...
            ///in an attemot to do this, i exported the version _Before_ attempting this on 1/21/2021, labeled and COmplexSubtitleSystem with said date
            if (Speaker != null)
            {
                //if theres no distance, it counts as looking at the speaker--implement
                speakerZ = Speaker.transform.position.z;
                speakerX = Speaker.transform.position.x;
                playerZ = player.transform.position.z;
                playerX = player.transform.position.x;
                speakerAngle = (180.0f / Mathf.PI) * (Mathf.Atan(Math.Abs(speakerZ - playerZ) / Math.Abs(speakerX - playerX)));
                speakerAngle = fullCircleConvert(speakerAngle);
                playerAngle += player.transform.rotation.z;
                //the coefficent converst it to degrees, which i think are easier to sdeall with in grid form
                //perhaps try to trouble shoot by puttingn aline at the player angle angle??
                float temp1 = (playerAngle - allowance);
                float temp2 = (playerAngle + allowance);
                float test1 = temp1 % (360.0f);
                float test2 = temp2 % (360.0f);
                if (speakerAngle >= (float)test1)
                {
                    if (speakerAngle <= (float)test2)
                    {
                        return "true";
                    }
                    else
                    {
                        return "left";
                    }
                }
            }
            return "right";
            //use raycast? so its clear that the player has a direct line of site to the speaker?
            //though, an alternative would be to ust have player.foward +- allowance == angle between player and speaker
            //which would be useful if speakers were behind objects...
            //alternativelyy, if the speakers were behind objects, there could be a silloette of the speaker
            //perhaps that could be toggled on and of*/ 
        }

        /*Boolean isSpeakerOnLeft()
        {
        }*/

        public float fullCircleConvert(float origAngle)
        {
            if (((speakerZ - playerZ) >= 0) && ((speakerX - playerX) > 0))
            {
                //no change
            }
            if (((speakerZ - playerZ) > 0) && ((speakerX - playerX) <= 0))
            {
                return 180.0f - speakerAngle;
            }
            if (((speakerZ - playerZ) <= 0) && ((speakerX - playerX) < 0))
            {
                return 180.0f + speakerAngle;
            }
            if (((speakerZ - playerZ) < 0) && ((speakerX - playerX) >= 0))
            {
                return 360.0f - speakerAngle;
            }
            return speakerAngle;
        }

        public void lightSpeaker(Boolean setting)
        {
            if (Speaker != null)
            {
                mostRecentSpeakerLight = Speaker.transform.GetChild(0).gameObject;
                if (mostRecentSpeakerLight != null)
                {
                    if (setting)
                    {

                        mostRecentSpeakerLight.SetActive(true);
                        // sets the public variable emissionSourceMaterial's emission property active. Use DisableKeyword to disable emission.
                    }
                    else
                    {
                        mostRecentSpeakerLight.SetActive(false);
                    }
                }
                else
                {
                    //this system defaults to jsut not doing anything if 
                    //maybe edit it to throw an exception if the light component of a speaker is null?
                    //what about if the speaker doesnt have a light component???
                    UnityEngine.Debug.Log("speakerLight null");
                }
            }
            else if (mostRecentSpeakerLight != null)
            {
                mostRecentSpeakerLight.SetActive(false);
            }
        }

        public void changeText(Boolean textSetting)
        {
            //color text when you face speaker??
            //like because you know who's talking sometimes when you see them??
            if (textSetting)
            {
                ///TO DO: change to havetext backgorund not havr to be assigned for every subtitle trigger 
                textBackground.SetActive(true);
            }
            else
            {
                textBackground.SetActive(false);
            }
        }

        //is there a clear line between the player's forward direction and the center of the speaker
        //using some code from/referencing : https://answers.unity.com/questions/294285/casting-ray-forward-from-transform-position.html
        public Boolean doesRaycastHitSpeaker()
        {
            targetHit = new RaycastHit();
            //do i neeed to initilize targetHit?
            //targetHit is a RaycastHit declared in the beguinning of this class
            //how to get this to check specifically to see if it hits the speaker?
            playerForward = player.transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(player.transform.position, player.transform.forward, out targetHit, 100))
            {
                //UnityEngine.Debug.DrawRay(player.transform.position, player.transform.TransformDirection(Vector3.forward) * targetHit.distance, Color.yellow);
                //UnityEngine.Debug.Log("hit");
                //UnityEngine.Debug.Log("hit: " + targetHit.transform.name);
                if (targetHit.transform.name.Equals(speakerTag))
                {

                    return true;
                }
            }
            //UnityEngine.Debug.DrawRay(player.transform.position, player.transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //UnityEngine.Debug.Log("Did not Hit");
            return false;
        }
    }
}