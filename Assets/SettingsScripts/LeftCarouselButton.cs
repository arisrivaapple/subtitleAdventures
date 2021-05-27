using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SubtitleSystem
{
    public class LeftCarouselButton : MonoBehaviour
    {

        public GameObject fontCarousel;
        //correct spelling: carousel
        // Start is called before the first frame update
       //having trouble assigning from sript
            //fontCarousel = GameObject.Find("Font Carousel Button");
            //fontCarousel = gameObject.transform.parent.parent.gameObject;


        public void OnClickLeft()
        {
            //do later
            fontCarousel.GetComponent<fontCarousel>().fontCarouselText.text = fontCarousel.GetComponent<fontCarousel>().cycleLeft().fontName;
        }
    }
}