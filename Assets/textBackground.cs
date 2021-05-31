using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//there are definately ways to hgihglight, but not any simple ones I can find
//like are we goign to use html, c# tags system, or textMeshPro sytstem?
//ive bookmarked these all in y makerlabfolder
//i honestly want to ask what the most understand approah would be to other people, so for now ill just color in the panel behind the text
//my first instinct is textmeshpro approach, but im not sure
//for soemreason i think the whole panel turning color opposite of the text would be disconceritng, so i think ill leave that feature for hwen figuring out the highlights?
namespace SubtitleSystem
{

    public class textBackground : MonoBehaviour
    {
        public GameObject thisObject;
        public TextMeshProUGUI textHere;
        public GameObject main;
        public Image panel;
        public Color tempColor;
        public Color clear;

        // Update is called once per frame
        void Start()
        {
            //start runs twice here for some reason???
            main = GameObject.Find("Main Camera");
            main.GetComponent<Main>().textBackground = thisObject;
            thisObject = gameObject;
            textHere = thisObject.GetComponent<TextMeshProUGUI>();
            panel = GetComponent<Image>();

            tempColor = Color.black;
            tempColor.a = 0.5f;

            clear = Color.black;
            clear.a = 0f;
        }

        void Update()
        {
            //should i add a transparency slider that the user can use as well?
            if (main.GetComponent<Main>().subtitleBackground)
            {
                panel.color = tempColor;

            }
            //textHere.text = <span style="background-color:#FFFF00"> + textHere.text + </span>;
            else
            {
                panel.color = clear;
            }//panel turns null between updates?????
            //but doesnt seem to do anything to justify that???????
        }
    }
}