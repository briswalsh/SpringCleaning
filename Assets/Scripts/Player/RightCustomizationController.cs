﻿using System.Collections;
using UnityEngine;

public class RightCustomizationController : MonoBehaviour {
    public GameObject eye;
    public float eyeHeight;

    public float lgHeight;
    public float midHeight;

    public bool isActive = true;
    public bool rightDominant;

    private bool userInitialized = false;
    private bool initialSetup = true;
    private bool menuActive = true;

    private int currentRightMallet = 0;
    private int currentLeftMallet = 0;

    private bool handSet = false;
    private bool lenSet = false;
    private bool rotSet = false;

    private bool rightFingerPressed = false;
    private bool rightFingerDown = false;
    private bool leftFingerPressed = false;
    private bool leftFingerDown = false;

    private bool rightPalmPressed = false;
    private bool rightPalmDown = false;
    private bool leftPalmPressed = false;
    private bool leftPalmDown = false;

	public GameObject soundManager;
	private SoundsController sfx;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(SetEyeHeight());
        sfx = soundManager.GetComponent<SoundsController>();
    }

    // Update is called once per frame
    void Update ()
    {
        TriggerChecks();

		if (!userInitialized && menuActive)
        {
            if (CheckForDominantHand()) // prompt the user to choose their dominant hand
            {
                if (CheckForMalletLength()) // prompt user to choose the length of their mallet
                {
                    if (CheckForMalletRotation()) // prompt user to choose rotation of their mallet
                    {
                        if (Confirm()) // prompt the user to confirm their choices
                        {
                            userInitialized = true;
                            menuActive = false;
                            initialSetup = false;
                        }
                    }
                }
            }
        }
        else if (userInitialized && menuActive)
        {
            // robot must approach them
            // then user is asked what it wants to customize
            print("menu is active again");
        }
        else // user can activate the customization menu with their non-dominant hand
        {
            if (rightDominant)
            {
                if (OVRInput.GetDown(OVRInput.Button.Three, OVRInput.Controller.Touch) || OVRInput.GetDown(OVRInput.Button.Four, OVRInput.Controller.Touch))
                {
                    menuActive = true;
                }
            }
            else
            {
                if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two))
                {
                    menuActive = true;
                }
            }
        }
	}

    void TriggerChecks()
    {
        float leftPalmClench = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch);
        float leftFingerClench = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.Touch);

        float rightPalmClench = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch);
        float rightFingerClench = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch);

        if (rightPalmClench > 0.2f)
        {
            if (!rightPalmPressed)
            {
                rightPalmPressed = true;
                rightPalmDown = true;
            }
        }
        else
        {
            rightPalmPressed = false;
        }
        if (rightFingerClench > 0.2f)
        {
            if (!rightFingerPressed)
            {
                rightFingerPressed = true;
                rightFingerDown = true;
            }
        }
        else
        {
            rightFingerPressed = false;
        }

        if (leftPalmClench > 0.2f)
        {
            if (!leftPalmPressed)
            {
                leftPalmPressed = true;
                leftPalmDown = true;
            }
        }
        else
        {
            leftPalmPressed = false;
        }
        if (leftFingerClench > 0.2f)
        {
            if (!leftFingerPressed)
            {
                leftFingerPressed = true;
                leftFingerDown = true;
            }
        }
        else
        {
            leftFingerPressed = false;
        }
    }

    bool CheckForDominantHand()
    {
        if (handSet)
        {
            return true;
        }

        float leftPalmClench = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch);
        float leftFingerClench = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.Touch);
        
        float rightPalmClench = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch);
        float rightFingerClench = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch);

        if (rightPalmClench > 0.2f && rightFingerClench > 0.2f)
        {
//            print("player has chosen right hand");
            transform.GetChild(currentRightMallet).gameObject.SetActive(true);
			sfx.Narrate ("intro2");
            lenSet = true;
            rightDominant = true;
            handSet = true;
            rotSet = true;
        }
        
        if (leftPalmClench > 0.2f && leftFingerClench > 0.2f)
        {
//            print("player has chosen left hand");
            rightDominant = false;
            handSet = true;
            lenSet = true;
            rotSet = true;
        }

        return false;
    }

    bool CheckForMalletLength()
    {
        if (lenSet)
        {
            return true;
        }

        // squeeze trigger to go up in size; squeeze palm to go down in size
        if (rightDominant)
        {
            if (rightFingerDown) // toggle up
            {
                gameObject.transform.GetChild(currentRightMallet).gameObject.SetActive(false);
                currentRightMallet = (currentRightMallet + 1) % 3;
                gameObject.transform.GetChild(currentRightMallet).gameObject.SetActive(true);
                rightFingerDown = false;
            }
            else if (rightPalmDown) // toggle down
            {
                gameObject.transform.GetChild(currentRightMallet).gameObject.SetActive(false);
                currentRightMallet--;
                if (currentRightMallet < 0)
                {
                    currentRightMallet = 2;
                }
                gameObject.transform.GetChild(currentRightMallet).gameObject.SetActive(true);
                rightPalmDown = false;
            }
        }
        else
        {
            if (leftFingerDown) // toggle up
            {
                leftFingerDown = false;

                lenSet = Confirm();
                return lenSet;
            }
            else if (leftPalmDown) // toggle down
            {
                leftPalmDown = false;

                lenSet = Confirm();
                return lenSet;
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three))
        {
            lenSet = true;
            return true;
        }

        return false;
    }

    bool CheckForMalletRotation()
    {
        if (rotSet)
        {
            return true;
        }

        // 90Deg (there will always only be 2 types of rotation)
        if (rightFingerDown && rightDominant || rightPalmDown && rightDominant)
        {
            gameObject.transform.GetChild(currentRightMallet).gameObject.SetActive(false);
            currentRightMallet = (currentRightMallet + 3) % 6;
            gameObject.transform.GetChild(currentRightMallet).gameObject.SetActive(true);

            rightFingerDown = false;
            rightPalmDown = false;
        }
        if (leftFingerDown && !rightDominant || leftPalmDown && !rightDominant) // Toggle down 
        {
            leftFingerDown = false;
            leftPalmDown = false;
        }

        if (OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Four))
        {
            rotSet = true;
            return true;
        }

        return false;
    }

    bool Confirm()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two) ||
            OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.Four))
        {
            return true;
        }

        // user has not yet indicated they are finished
        return false;
    }

    IEnumerator SetEyeHeight()
    {
        yield return new WaitForSeconds(0.25f);
        eyeHeight = eye.transform.position.y;

        if (eyeHeight > lgHeight)
        {
            currentRightMallet = 2;
        }
        else if (eyeHeight > midHeight)
        {
            currentRightMallet = 1;
        }
        else
        {
            currentRightMallet = 0;
        }
    }
}
