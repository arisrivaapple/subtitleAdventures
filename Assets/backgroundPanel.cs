using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundPanel : MonoBehaviour
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
        
    }
}
