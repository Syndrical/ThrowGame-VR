/*--------------------------------------------
 * 
   Kevin Hoang
   7778036
   Final Project
   
   Target Hit: This script provides the behaviour of a targetter
   when hit. Specifically, it'll increase the timer remaining 
   and change color when hit.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Hit : MonoBehaviour {

    public Material defaultMat;
    public Material onHitMat;
    public TextMesh theText;
    public float hitTimer;

	// Use this for initialization
	void Start ()
    {
        hitTimer = 0;
	}

    // Change color and increase timer if enter
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            gameObject.GetComponent<MeshRenderer>().material = onHitMat;
            theText.GetComponent<Timer>().theTimer += 5f;
        } // if 
    }

    // Change color if exit
    public void OnTriggerExit(Collider other)
    {
        hitTimer = 1f;
    }

    // Update is called once per frame
    void Update ()
    {
        if (hitTimer >= 0)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0)
            {
                gameObject.GetComponent<MeshRenderer>().material = defaultMat;
            }
        }
    }
}
