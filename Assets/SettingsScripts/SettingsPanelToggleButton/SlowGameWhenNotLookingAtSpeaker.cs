using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubtitleSystem
{
    //should be encapsulated in "slow game when not looking at tspeker
    public class SlowGameWhenNotLookingAtSpeaker : MonoBehaviour
    {
        public GameObject check;
        public GameObject main; //options to have a main other than camera without attaching everythng? seperate main blank object for this?

        void Start()
        {
            main = GameObject.Find("Main Camera");
            //to set this as true by default
            ToggleSettings();
        }

        public void ToggleSettings()
        {
            if (check.activeSelf)
            {
                //false
                main.GetComponent<Main>().nonSpeakerFacingSpeed = main.GetComponent<Main>().speakerFacingSpeed;
                check.SetActive(false);
            }
            else
            {
                //true
                main.GetComponent<Main>().nonSpeakerFacingSpeed = main.GetComponent<Main>().speakerFacingSpeed/2;
                check.SetActive(true);
            }
        }
    }
}
