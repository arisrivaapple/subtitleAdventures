using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubtitleSystem
{
    public class speechBubbles : MonoBehaviour
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
                main.GetComponent<Main>().showSpeechBubble = false;
                check.SetActive(false);
            }
            else
            {
                main.GetComponent<Main>().showSpeechBubble = true;
                check.SetActive(true);
            }
        }
    }
}


//maybe ill originally have all in the settings panel for experiment (to test out), but maybe some should be changed to be just for the developer?

/*
public Image speechBubble;
// Start is called before the first frame update
void Start()
{
    ///the person originally instantiates it here but that seems maybe a lil silly? idk
}

// Update is called once per frame
void Update()
{
    speechBubkke.transform.position = Camera.main.WorldToScreenPoint(speaker.transform.position);
}*/
