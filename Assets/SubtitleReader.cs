    using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.Net;

namespace SubtitleSystem
{
    public class SubtitleReader
    {
        //stop two subttitles playing at once
        //also add waarning if subtitles in file might pla at th esaame time
        //we keep these as double insteadof ints to help facilitate fine tunign of the speed
        public double timer;
        public double internalTimerRate;
        public string speaker;
        public TextAsset subtitleText;
        public Queue<Subtitle> subtitleQueue;
        public Button speakerToggleButton;
        public GameObject player;
        public GameObject mainc;
        //system for keeping the color of a speaker while showing questions marks
        //maybe if they put a question mark before the speaker part in srt?
        //i dont love it because its nto standard but it should work

        int timeToClose;

        public SubtitleReader(String subtitleSetText)
        {
            mainc = GameObject.Find("Main Camera");
            player = GameObject.Find("player");
            timer = 0.0;
            internalTimerRate = 0.000001;
            //set this back to null after testing
            speaker = "";
            subtitleQueue = new Queue<Subtitle>();
            //it seems as if canvases are generally rerferences s a component of game objects
            parseSubtitles(subtitleSetText);
        }

        //this method is used to generally process subtitles for different types of subtitles and classes
        //this method is used to generally process subtitles for different types of subtitles and classes
        //it reads srt sfiles
        public void parseSubtitles(String text)
        {
            //shoudl i/can is  declare as a subtitle type queue?
            string[] seperator = { "\r\n", "\n" }; //or \r\n
            string[] lines = (text.ToString()).Split(seperator, StringSplitOptions.None);
            int i = 0;
            Regex line1 = new Regex(@"\d+");
            Regex line2 = new Regex(@"([\d]{2}:){2}(\d){2},(\d){3} --> ([\d]{2}:){2}(\d){2},(\d){3}");
            Regex line3 = new Regex(@"\[.*\].*");
            Regex blankSpace = new Regex(@"");
            while (i < lines.Length)
            {
                //could do this splitting in the subtitle class, but it seems to make mroe sense here
                //check to see if the next 4 linesmatch the correct format
                //how to deal with multiple lined subtitles?
                //would instantiating this multiple times in a for loop cause problems?
                Subtitle newSubtitleObject = new Subtitle();
                if (line1.IsMatch(lines[i]))
                {
                    //i kept this syntax because its more readbale
                    //if the code reaches here, its currentally matching the rormat
                }
                else
                {
                    throw new System.InvalidOperationException("Text not in srt format.");
                }
                if (i + 1 < lines.Length && line2.IsMatch(lines[i + 1]))
                {
                    newSubtitleObject.inputTimeInfo(lines[i + 1]);
                }
                else
                {
                    throw new System.InvalidOperationException("Text not in srt format.");
                }
                if (i + 2 < lines.Length && line3.IsMatch(lines[i + 2]))
                {
                    newSubtitleObject.inputTextInfo(lines[i + 2]);
                    //checking if this line is the last line
                    if (lines.Length - 1 == i + 2 || lines.Length - 1 == i + 3)
                    {
                        timeToClose = newSubtitleObject.getEndTimeInt();
                    }
                }
                else
                {
                    throw new System.InvalidOperationException("Text not in srt format.");
                }
                if (i + 2 < lines.Length - 1 && !(blankSpace.IsMatch(lines[i + 3])))
                {
                    {
                        throw new System.InvalidOperationException("Text not in srt format.");
                    }

                }
                i += 4;
                subtitleQueue.Enqueue(newSubtitleObject);
            }
        }


        public string readSubtitles() //better form to jsut set it in the subclass or pass it upwards?
        {
            //the times of the subtiitles should include people speaking at the same time with 
            //them bboth being incorporated into subtitle piritions
            //is there any reason I should ditcht the subtitle objects as soon as they;re read?
            Subtitle nextSubtitle = (Subtitle)subtitleQueue.Peek();
            //substitles need to be createed and sdestroyed version:
            //would it be better form dto do a range here?
            if (subtitleQueue.Count > 0)
            {
                if (nextSubtitle.getStartTimeInt() <= timer && !(nextSubtitle.getEndTimeInt() <= timer))
                {
                    //display subtitle

                    speaker = nextSubtitle.getSpeaker();
                    if (mainc.GetComponent<Main>().showSpeakerNames) 
                    {
                        return "[" + speaker + "] " + nextSubtitle.getText();
                    }
                    return nextSubtitle.getText();
                }
                if (nextSubtitle.getEndTimeInt() <= timer)
                {
                    //destroy any subtitle on screen
                    subtitleQueue.Dequeue();
                    speaker = null;
                }
                return null;
            }
            return null;
        }

        //i made a seperate method to check if the program should end
        //because null is the only non-enterablke valuable that i can return in the rea dsubtitles method
        //and im already using it not display subtitles
        public Boolean shouldProgramEnd()
        {
            if (timer <= timeToClose)
            {
                return false;
            }
            return true;
            //since the default is return false and that seems dangerous just because its mroe liekly to send your program into a loop unexpectadly
            //we should save the endtime of the last subtitle ahead of time, in the creation of the subtitles
        }

        //i made this a fucntion, since it makes more sense for the smaller class sbtitle reader
        //to not inherit monobehavior
        public void incrementTime()
        {
            timer = internalTimerRate + timer;
        }

        //change to version of increment itme
        public void incrementTime(double incrementTimeBy)
        {
            timer += incrementTimeBy;
        }

        public double getInternalTime()
        {
            return timer;
        }

        public void setInternalTimerRate(double newRate)
        {
            internalTimerRate = newRate;
        }

        //im nto sure if we'll actually need to use this method
        public double getInternalTimerRate()
        {
            return internalTimerRate;
        }

        // Update is called once per frame
        //would it be at all better to do an awake/while loop combi?
        //generally, i think it would be more legiable to the user
        //if this system used a seperate class the seperate the different fields of 
        //each line of subtitle
        //and then made a queue of themvoid

        public string getSpeaker()
        {
            return speaker;
        }
    }
}
