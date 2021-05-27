using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using UnityEngine;
using TMPro;

namespace SubtitleSystem
{
    //make the sytem more adaptable for multiple speaker scnees?
    //though i think maybe the prefab shoould be one speaker at a time???
    //idk i think one speaker at a time is more understandbale for the reader
    public class fontCarousel : MonoBehaviour
    {
        //we're asdsign the ftns in the inspector
        public GameObject mainc;
        public int numFont;
        public fontID[] fontArray;
        public int index;
        public TMP_FontAsset Arimo;
        public TMP_FontAsset OpenSans;
        public TMP_FontAsset Jupiter;
        public TMP_FontAsset EBGaramond;
        public TMP_FontAsset BerkshireSwash;
        public TextMeshProUGUI fontCarouselText;

        //store the current font in main or somethign as well??
        void Start() {

            //main command: text.font = [Font object]
            //how to get Font from TMP_FOntAsset (the bigger contianer)?
            //let;s see if regular Fonts can be used in text.font for TMP text
            //set it to best fit instead and drag fints in???? idk which on is betterr....
            //the user can of course add their own fonts by adding fonts and increasing numFonts accordingly
            numFont = 5;
            //main = GameObject.Find("Main Camera");
            fontArray = new fontID[numFont]; 
            fontArray[0] = new fontID(Arimo, "Arimo");
            fontArray[1] = new fontID(OpenSans, "OpenSans");
            fontArray[2] = new fontID(Jupiter, "Jupiter");
            fontArray[2].fontSize = 90;
            fontArray[3] = new fontID(EBGaramond, "EBGaramond");
            fontArray[4] = new fontID(BerkshireSwash, "BerkshireSwash");
        }

        //is there a better way to make a ciurcular array?
        public fontID cycleLeft()
        {
            if (index > 0)
            {
                index -= 1;
            } 
            else 
            {
                index = numFont - 1;
            }
            mainc.GetComponent<Main>().currFont = fontArray[index].fontFont;
            mainc.GetComponent<Main>().subtitleFontSize = fontArray[index].fontSize;
            fontCarouselText.text = fontArray[index].fontName;
            return fontArray[index];
        }
        public fontID cycleRight()
        {
                if (index < numFont - 1)
                {
                    index += 1;
                }
                else
                {
                    index = 0;
                }
                //th call for the Main cript isnt working for some reason???
            mainc.GetComponent<Main>().currFont = fontArray[index].fontFont;
            mainc.GetComponent<Main>().subtitleFontSize = fontArray[index].fontSize;
            fontCarouselText.text = fontArray[index].fontName;
            //fontArray[index].fontName
            return fontArray[index];
            //does get component take more time than a simple varibale reference?
             //check this variable
            //im not sure of a way to make things check for updates only when this is called?
            //i mean maybe I should make a boolean that sets off when there's any change?? but that would ony help if text object had multiple tings to check...and I dont think i do...
            //name 
            //i really want to know if theres a more efficent way

        }
        //doesnt really nee to be a seperate method
        //dont have the on click functions in the same script, because they bith need to be able to reference the same variable for index
        public void OnClickRight()
        {
            cycleRight();

        }


        
        //i dont know if ill need to use this function
        //technically you could access the barrier but this seems like better form
        public fontID currFont () {
                return fontArray[index];
        }
    }
}