using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubtitleSystem
{
    public class DragSubtitleButton : MonoBehaviour
    {
        //should i make this more precise for just the subtitles?
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
                main.GetComponent<Main>().dragSubtitles = false;
                check.SetActive(false);
            }
            else
            {
                main.GetComponent<Main>().dragSubtitles = true;
                check.SetActive(true);
            }
        }
    }
}
