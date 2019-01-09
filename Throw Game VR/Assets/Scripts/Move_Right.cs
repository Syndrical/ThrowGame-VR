/*--------------------------------------------
 * 
   Kevin Hoang
   7778036
   Final Project
   
   Move Right: This script provides the behaviour of a targetter
   or an obstacle. Specifically, it will make it right.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Right : MonoBehaviour
{
    public float speed = 1.25f;
    // Update is called once per frame
    void Update()
    {
        Vector3 NewPos = new Vector3(speed, 0, 0);
        this.transform.position += NewPos * Time.deltaTime;

        if (this.transform.position.x >= 2.5)
        {
            this.transform.position = new Vector3(-2.5f, this.transform.position.y, this.transform.position.z);
        } // if
    } // Update
} 