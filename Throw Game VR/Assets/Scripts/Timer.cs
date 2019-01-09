using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float theTimer;
    public int intTime; 
    public TextMesh theText;

	// Use this for initialization
	void Start ()
    {
        theTimer = 30;
	}
	
	// Update is called once per frame
	void Update ()
    {
        theTimer -= Time.deltaTime;
        intTime = (int)theTimer;
        theText.text = intTime.ToString();
        if (theTimer <= 0)
        {
            Time.timeScale = 0;
        }
	}
}
