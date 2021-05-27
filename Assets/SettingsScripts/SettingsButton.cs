using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//i think it makes the most sense to keep this as part of namespace

namespace SubtitleSystem
{
    public class SettingsButton : MonoBehaviour
    {
        public GameObject settingsPanel;
        //there doesnt seem to be an easy way to reference component images
        public GameObject gear;
        public GameObject x;

        void Start()
        {

            //have a main way to reference things withouot using the find function
            //this idnt work
            //is it cause it was a couple of layers in or something else?
            //settingsPanel = GameObject.Find("SettingsPanel");
        }

        public void OpenCloseSettingsPanel()
        {
            //txt = gameObject.GetComponent<Text>();
            //why didnt this call work? i feel like these calls might be better form then public assignents
            if (settingsPanel.activeSelf)
            {
                settingsPanel.SetActive(false);
                gear.SetActive(true);
                x.SetActive(false);
            }
            else
            {
                settingsPanel.SetActive(true);
                gear.SetActive(false);
                x.SetActive(true);
            }
        }
    }
}
