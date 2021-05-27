using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SubtitleSystem
{
    public class TextSizeDemo : MonoBehaviour
    {
        public GameObject main;
        // Start is called before the first frame update
        void Start()
        {
            main = GameObject.Find("Main Camera");
        }

        // Update is called once per frame
        void Update()
        {
            //this line doesn;t work below
            //GetComponent<TextMesh>().fontSize = main.GetComponent<Main>().subtitleFontSize;
            //

            //so im going to make a function in main I can call hopefully
        }
    }
}
