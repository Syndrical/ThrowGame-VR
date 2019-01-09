/*--------------------------------------------
 * 
   Kevin Hoang
   7778036
   Final Project
   
   Target Movement: This script provides the behaviour of a targetter
   moving. Specifically, it'll provide movement beaviour at random 
   every time it does a full run (from left to right, or right to
   left.)

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Movement : MonoBehaviour {

    // Use this for initialization
    void Start ()
    {
        createBehaviour();
	}
    
    void createBehaviour()
    {
        int randomRow;
        float randomSpeed;
        int randomScript;
        float newZ = 0;

        randomRow = Random.Range(0, 3);
        randomSpeed = Random.Range(0.5f, 4f);
        randomScript = Random.Range(0, 5);

        // See which row to put it in
        switch (randomRow)
        {
            case 0: newZ = 2.5f; break;
            case 1: newZ = 3.5f; break;
            case 2: newZ = 4.5f; break;
        } // switch

        // Figure out pattern of target
        switch (randomScript)
        {
            // Left only
            case 0:
                this.GetComponent<Move_Left>().enabled = true;
                this.GetComponent<Move_Right>().enabled = false;
                this.GetComponent<Move_Up_Down>().enabled = false;
                this.GetComponent<Move_Left>().speed = -randomSpeed;
                this.transform.position = new Vector3(2.5f, this.transform.position.y, newZ);
                break;
            // Right only
            case 1:
                this.GetComponent<Move_Right>().enabled = true;
                this.GetComponent<Move_Left>().enabled = false;
                this.GetComponent<Move_Up_Down>().enabled = false;
                this.GetComponent<Move_Right>().speed = randomSpeed;
                this.transform.position = new Vector3(-2.5f, this.transform.position.y, newZ);
                break;
            // Left and height
            case 2:
                this.GetComponent<Move_Left>().enabled = true;
                this.GetComponent<Move_Up_Down>().enabled = true;
                this.GetComponent<Move_Right>().enabled = false;
                this.GetComponent<Move_Left>().speed = -randomSpeed;
                this.transform.position = new Vector3(2.5f, this.transform.position.y, newZ);
                break;
            // Right and height
            case 3:
                this.GetComponent<Move_Right>().enabled = true;
                this.GetComponent<Move_Up_Down>().enabled = true;
                this.GetComponent<Move_Left>().enabled = false;
                this.GetComponent<Move_Right>().speed = randomSpeed;
                this.transform.position = new Vector3(-2.5f, this.transform.position.y, newZ);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x <= -2.5 || this.transform.position.x >= 2.5)
        {
            createBehaviour();
        }
    } // Update
}
