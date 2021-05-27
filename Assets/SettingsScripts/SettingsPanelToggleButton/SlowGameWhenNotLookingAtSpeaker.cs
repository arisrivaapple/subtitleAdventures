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
        }

        public void ToggleSettings()
        {
            if (check.activeSelf)
            {
                main.GetComponent<Main>().slowGameWhenNotLookingAtSpeaker = false;
                check.SetActive(false);
            }
            else
            {
                main.GetComponent<Main>().slowGameWhenNotLookingAtSpeaker = true;
                check.SetActive(true);
            }
        }
    }
}
