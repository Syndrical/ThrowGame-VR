/*--------------------------------------------
 * 
   Kevin Hoang
   7778036
   Final Project
   
   --NOT USED--

   Gestures: This script provides the functionallity of 
   using gestures with the interaction of an NPC. Used 
   when trigger on controller is pressed.

   The code was written from scratch. However, the
   implementation is not used as there is too much 
   to consider when doing gestures and as well, the
   current implementations to some so far are buggy.

   The following were "completed," but most likely
   contain bugs

   - SHRUG
   - POINT
   - CUTTTHROAT?
   - HEAD GESTURES

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gestures:MonoBehaviour
{

    public static bool getData;               // Determines when to retrieve data at a certain point
    public static float distance;            // Get distance

    // Testing
    public bool shrugComplete;
    public bool pointComplete;
    public bool waveComplete;
    public bool highFiveComplete;
    public bool cutthroatComplete;
    public bool shrugRotate;
    public bool silenceComplete;

    // Used for gestures
    public float xPrevRot;                    // Previous rotation (X)
    public float yPrevRot;                   // Previous rotation (Y)
    public float zPrevRot;                  // Previous rotation (Z)
    public float xRotation;                // Gets rotation input (X)
    public float yRotation;               // Get rotation input (Y)
    public float zRotation;              // Get rotation input (Z)
    public float xPrev;                 // Previous location (X)
    public float yPrev;                // Previous location (Y)
    public float zPrev;               // Previous location (Z)
    public float xCurrent;           // Current location of controller (Z)
    public float yCurrent;          // Current location of controller (Y)
    public float zCurrent;         // Current location of controller (Z)
    public int xNumRotation;      // Gets number of confirmed rotation from X axis
    public int yNumRotation;     // Gets number of confirmed rotation from Y axis
    public int zNumRotation;    // Gets number of confirmed rotation from Z axis

    // Shrug
    public const float shrugDistance = 0.25f;                // Distance needed to perform shrug gesture
    public const float shrugRotNeeded = 100;            // Controller Rotation needed to shrug gesture

    // Point
    public const float pointDistance = 0.35f;            // Distance to perform a pointing gesture

    // Wave
    public bool leftOrRight;                      // Left or right to start off wave (False = left, Right = true)
    public bool fastVelocity;
    public float waveDistance = 0.10f;           // Distance to confirm wave motion
    public int numWaveConfirm;                  // Number of confirmed waves
    public const int waveInput = 3;            // How many wave motions to produce "wave"

    // High Five
    public const float highFiveSpeed = 50;  // Speed needed to activate high five
    public float currSpeed;                // Current speed at specific frame
    public bool highFiveContact;          // Determines if collided with anything

    // Cutthroat
    public const float cutDistance = 0.6f; // Distance to perform cutthroat motion

    // Speed 
    public Vector3 previous;
    public float velocity;

    // Silence
    public const float timeSilence = 8;
    public float currTime;
    public bool stopTime;

    // Stop all motion 
    public bool stopGestures;

    private SteamVR_TrackedObject trackedObj;                 // Reference to object being tracked
    private SteamVR_Controller.Device Controller            // Controller input
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    // Get reference of tracked object that's attached to object
    public void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        numWaveConfirm = 0;
        stopGestures = false;
        previous = Vector3.zero;
        leftOrRight = false;
        getData = true;

        shrugComplete = false;
        pointComplete = false;
        waveComplete = false;
        highFiveComplete = false;
        cutthroatComplete = false;
        stopTime = true;
        leftOrRight = false;
    }

    // Update is called once per frame
    public void Update()
    {
        // Get current controller location
        xCurrent = this.gameObject.transform.position.x;
        yCurrent = this.gameObject.transform.position.y;
        zCurrent = this.gameObject.transform.position.z;

        // Get rotation 
        xRotation = this.gameObject.transform.eulerAngles.x;
        yRotation = this.gameObject.transform.eulerAngles.y;
        zRotation = this.gameObject.transform.eulerAngles.z;

        velocity = ((this.transform.position - previous).magnitude) / Time.deltaTime; // could be anything to scale frame => sec => hour
        previous = transform.position;

        // Trigger squeeze event
        if (Controller.GetHairTriggerDown())
        {
            if (getData)
            {
                // Get current controller location
                xPrev = this.gameObject.transform.position.x;
                yPrev = this.gameObject.transform.position.y;
                zPrev = this.gameObject.transform.position.z;

                // Get rotation 
                xPrevRot = this.gameObject.transform.eulerAngles.x;
                yPrevRot = this.gameObject.transform.eulerAngles.y;
                zPrevRot = this.gameObject.transform.eulerAngles.z;

                getData = false;
            } // if
            CameraActionReponses.SetInputTrue();
        } // if

        if (!getData)
            GetGesture();

        // Release trigger
        if (Controller.GetHairTriggerUp())
        {
            getData = true;
            stopGestures = false;
            shrugComplete = false;
            pointComplete = false;
            waveComplete = false;
            highFiveComplete = false;
            cutthroatComplete = false;
            CameraActionReponses.SetInputFalse();
        } // if
    } // Update

    // Tries to see if any of the gestures were made
    public void GetGesture()
    {
        // Check stuff
        HighFive();
        Point();
        Shrug();
        CutThroat();
        Wave();
    } // GetGesture

    // Point - Gets directional input stuff
    public void Point()
    {
        // Check if current controller location if they're pointing somewhere
        if (velocity > 0.7 && xCurrent - xPrev > pointDistance)
        {
            stopGestures = true;
            pointComplete = true;
        } // if

        // Direction
        if (velocity > 0.7 && xPrev - xCurrent > pointDistance)
        {
            stopGestures = true;
            pointComplete = true;
        } // if

        // Direction
        if (velocity > 0.7 && zCurrent - zPrev > pointDistance)
        {
            stopGestures = true;
            pointComplete = true;
        } // if

        // Direction 
        if (velocity > 0.7 && zPrev - zCurrent > pointDistance)
        {
            stopGestures = true;
            pointComplete = true;
        } // if 
    } // Point

    // High Five Gesture
    public void HighFive()
    {
        // Find how fast it was going and if it hits something
        if (highFiveContact && currSpeed > highFiveSpeed)
        {
            // Do High Five 
            stopGestures = true;
            highFiveComplete = true;
        } // if
    } // HighFive

    /*//  Waving gesture
    public void Wave()
    {
        // Check distance from them waving 
        if ((!leftOrRight && (xPrev - xCurrent) > waveDistance) || (leftOrRight && (xCurrent - xPrev) > waveDistance))
        {
            xPrev = xCurrent;
            leftOrRight = !leftOrRight;
            numWaveConfirm++;

            if (numWaveConfirm == waveInput)
            {
                // Do Wave input response
                stopGestures = true;
                waveComplete = true;
                numWaveConfirm = 0;
            } // if
        } // if
    } // Wave  */

    //  Waving gesture
    public void Wave()
    {
        // Check distance from them waving 
        if ((fastVelocity) || velocity > 2 && (Mathf.Abs(yPrev - yCurrent) < 1))
        {
            if (!fastVelocity)
            {
                fastVelocity = true;
            } // if
            else
            {
                if (velocity < 1)
                {
                    fastVelocity = false;
                    xPrev = xCurrent;
                    leftOrRight = !leftOrRight;
                    numWaveConfirm++;

                    if (numWaveConfirm == waveInput)
                    {
                        // Do Wave input response
                        stopGestures = true;
                        waveComplete = true;
                        numWaveConfirm = 0;
                    } // if
                } // if
            } // if
        } // if
    } // Wave 

    // Shrug gesture
    public void Shrug()
    {
        bool intoPositive = false;

        // See if it switches from negative to positive or vice versa for X
        if (zPrevRot < 60 && zRotation > 300 - shrugRotNeeded)
        {
            intoPositive = true;
        } // if
        else
        {
            intoPositive = false;
        } // else

        // Check if controller rotated a certain amount
        if ((!intoPositive && zPrevRot - zRotation > shrugRotNeeded) || (intoPositive && (zPrevRot + 360) - zRotation > shrugRotNeeded))
        {
            shrugRotate = true;
            // Check if controller moved up
            if (yCurrent - yPrev > shrugDistance)
            {
                // Shrug gesture
                stopGestures = true;
                shrugComplete = true;
            } // if
        } // if
        else
        {
            shrugRotate = false;
        } // else
    } // Shrug

    // Cut throat gesture
    public void CutThroat()
    {

        if (Mathf.Abs(xPrev - xCurrent) > cutDistance)
        {
            // Cut throat gestures
            stopGestures = true;
            cutthroatComplete = true;
        } // if
    }

    // Silence - Do Stuff
    public void Silence()
    {
        if (stopTime)
        {
            stopTime = false;
            currTime = timeSilence;
        } // if
        else
        {
            currTime -= Time.deltaTime;
            if (currTime <= 0)
            {
                stopGestures = true;
            } // if
        } // else
    } // Silence

    // OnCollisionEnter - High Five stuff
    void OnCollisionEnter(Collision coll)
    {
        highFiveContact = true;
    } // OnCollisionEnter

    // OnCollisionExit - High Five stuff
    void OnCollisionExit(Collision coll)
    {
        highFiveContact = false;
    } // OnCollisionExit
}
