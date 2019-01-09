/*--------------------------------------------
 * 
   Kevin Hoang
   7778036
   Final Project
   
   Count Up: This script will act as a timer, counting up.
   This script is used for people to see how long they have 
   actually lasted in the game.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountUp : MonoBehaviour
{

    public float theTimer;
    public int intTime;
    public TextMesh theText;

    // Use this for initialization
    void Start()
    {
        theTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        theTimer += Time.deltaTime;
        intTime = (int)theTimer;
        theText.text = intTime.ToString();
    }
}