/*--------------------------------------------
 * 
   Kevin Hoang
   7778036
   Final Project
   
   Move Up / Down: This script provides the behaviour of a targetter
   or an obstacle. Specifically, it will make it move up and down.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move_Up_Down : MonoBehaviour {

    public bool moveUp = true;
    // Update is called once per frame
    void Update()
    {
        if (moveUp)
        {
            Vector3 NewPos = new Vector3(0, 0.5f, 0);
            this.transform.position += NewPos * Time.deltaTime;

            if (this.transform.position.y >= 1.65)
            {
                moveUp = false;
            } // if
        } // moveUp
        else
        {
            Vector3 NewPos = new Vector3(0, -0.5f, 0);
            this.transform.position += NewPos * Time.deltaTime;

            if (this.transform.position.y <= 0.9)
            {
                moveUp = true;
            } // if
        }
    } // Update
} 
