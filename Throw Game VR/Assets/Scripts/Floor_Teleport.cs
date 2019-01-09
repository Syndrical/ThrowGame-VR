/*--------------------------------------------
 * 
   Kevin Hoang
   7778036
   Final Project
   
   Floor Telpoert: This script will make the ball
   teleport to the front if it falls on the ground.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Teleport : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}

    // Set up as potential grab target
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            other.gameObject.transform.position = new Vector3(0, 2.1f, 0.95f);
        } // if 
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
