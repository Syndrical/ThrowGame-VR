/*--------------------------------------------
 * 
   Kevin Hoang
   7778036
   Final Project
   
   Camera Action Responses: This script provides the functionallity of 
   gestures with the headset.

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraActionReponses:MonoBehaviour
{
    public static bool getInput;                    // Determines whether to accept input from head or not
    public static bool getData;                    // 
    public bool CRTR;                       // Complete Rotation To Right - Determines if it went from 360 -> 0 
    public bool CRTL;                      // Complete Rotation To Left - Determines if it went from 0 -> 360
    public bool NoComplete;
    public bool YesComplete;
    public bool upOrDown;
    public bool leftOrRight;
    public float xRotationInput;           // Gets rotation input for X axis
    public float yRotationInput;          // Get rotation input for Y axis
    public float xValue;
    public float yValue;
    public int xNumRotation;      // Gets number of confirmed rotation from X axis
    public int yNumRotation;     // Gets number of confirmed rotation from Y axis

    public const float degreeInputX = 10;
    public const float degreeInputY = 20;
    public const int numRotationForInput = 4;


    private SteamVR_TrackedObject trackedObj;                  // Reference to object being tracked
    private SteamVR_Controller.Device Controller            // Controller input
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }


    // Use this for initialization
    void Start()
    {
        YesComplete = false;
        NoComplete = false;
        getInput = false;
        getData = false;
        CRTR = false;
        CRTL = false;
        xNumRotation = 0;
        yNumRotation = 0;
        xRotationInput = 0;
        yRotationInput = 0;
    }

    // Calculate current and previous rotation
    void Yes()
    {
        CRTR = false;
        CRTL = false;

        xValue = this.gameObject.transform.eulerAngles.x;
        yValue = this.gameObject.transform.eulerAngles.y;

        // See if it switches from 360 -> 0 or vice versa
        if (xRotationInput >= 360 - degreeInputX && xValue <= degreeInputX)
            CRTR = true;
        if (xRotationInput <= degreeInputX && xValue >= 360 - degreeInputX)
            CRTL = true;

        // Check if head rotated a certain amount
        if ((!CRTR && !CRTL && ((upOrDown && (xRotationInput - xValue) > degreeInputX) || (!upOrDown && (xValue - xRotationInput) > degreeInputX))) || (CRTR && !upOrDown && (xValue + 360 - xRotationInput > degreeInputX)) || (CRTL && upOrDown && (xRotationInput + 360 - xValue > degreeInputX)))
        {
            xRotationInput = xValue;
            xNumRotation++;
            upOrDown = !upOrDown;

            // Check if number of completed rotations for X is reached to do action
            if (xNumRotation == numRotationForInput)
            {
                getInput = false;
                YesComplete = true;
                NoComplete = false;
                xNumRotation = 0;
                yNumRotation = 0;
            } // if
        } // if
    } // Yes

    void No()
    {
        CRTR = false;
        CRTL = false;

        // See if it switches from negative to positive or vice versa for Y
        if (yRotationInput >= 360 - degreeInputY && yValue <= degreeInputY)
            CRTR = true;
        if (yRotationInput <= degreeInputY && yValue >= 360 - degreeInputY)
            CRTL = true;

        // Check if head rotated a certain amount 
        if ((!CRTR && !CRTL && ((!leftOrRight && (yRotationInput - yValue) > degreeInputY) || (leftOrRight && (yValue - yRotationInput) > degreeInputY))) || (CRTR && leftOrRight && (yValue + 360 - yRotationInput) > degreeInputY) || (CRTL && !leftOrRight && (yRotationInput + 360 - yValue > degreeInputY)))
        {
            yRotationInput = yValue;
            yNumRotation++;
            leftOrRight = !leftOrRight;

            // Check if number of completed rotations for Y is reached to do action
            if (yNumRotation == numRotationForInput)
            {
                getInput = false;
                xNumRotation = 0;
                yNumRotation = 0;
                NoComplete = true;
                YesComplete = false;
            } // if
        } // if
    }

    // Update is called once per frame
    void Update()
    {
        // Check if we're able to get input first
        if (getInput)
        {
            if (getData)
            {
                Yes();
                No();
            }
            else
            {
                // Trigger is pressed, reset values
                getData = true;
                xNumRotation = 0;
                yNumRotation = 0;

                // Keep track of camera rotation at this point in time
                xRotationInput = this.gameObject.transform.eulerAngles.x;
                yRotationInput = this.gameObject.transform.eulerAngles.y;
            } // else
        } // if
        else
        {
            getData = false;
            NoComplete = false;
            YesComplete = false;
        }
    } // Update

    public static void SetInputTrue()
    {
        getInput = true;
    } // SetInputTrue

    public static void SetInputFalse()
    {
        getInput = false;
    } // SetInputFalse 
}