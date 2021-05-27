using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubtitleSystem
{
    public class ShowSilhouetteWhenSpeakerIsBehindObstacle : MonoBehaviour
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
                main.GetComponent<Main>().silhouettes = false;
                check.SetActive(false);
            }
            else
            {
                main.GetComponent<Main>().silhouettes = true;
                check.SetActive(true);
            }
        }
    }
}