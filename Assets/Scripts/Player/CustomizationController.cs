using UnityEngine;

public class CustomizationController : MonoBehaviour {
    public bool isActive = true;
    public GameObject[] leftMallets;
    public GameObject[] rightMallets;
    public bool rightDominant;

    private bool userInitialized = false;
    private bool initialSetup = true;
    private bool menuActive = true;

    private int currentRightMallet = 0;
    private int currentLeftMallet = 0;

    private bool handSet = false;
    private bool lenSet = false;
    private bool rotSet = false;

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update ()
    {
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

    bool CheckForDominantHand()
    {
        if (handSet)
        {
            return true;
        }

        float rightHandClench = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch);
        float rightFingerClench = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.Touch);

        float leftHandClench = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch);
        float leftFingerClench = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch);

        if (rightHandClench > 0.2f && rightFingerClench > 0.2f)
        {
            rightMallets[currentRightMallet].SetActive(true);

            rightDominant = true;
            handSet = true;
            return true;
        }
        
        if (leftHandClench > 0.2f && leftFingerClench > 0.2f)
        {
            leftMallets[currentLeftMallet].SetActive(true);

            rightDominant = false;
            handSet = true;
            return true;
        }

        return false;
    }

    bool CheckForMalletLength()
    {
        if (lenSet)
        {
            return true;
        }

        float rightHandClench = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.Touch);
        float rightFingerClench = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.Touch);

        float leftHandClench = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.Touch);
        float leftFingerClench = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.Controller.Touch);

        // 90Deg (there will always only be 2 types of rotation)
        if (rightFingerClench > 0.2f && rightDominant || rightHandClench > 0.2f && rightDominant)
        {
            
        }
        if (leftFingerClench > 0.2f && !rightDominant || leftHandClench > 0.2f && !rightDominant) // Toggle down 
        {

        }

        if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two) ||
            OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.Four))
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

        if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Two) ||
            OVRInput.GetDown(OVRInput.Button.Three) || OVRInput.GetDown(OVRInput.Button.Four))
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
}
