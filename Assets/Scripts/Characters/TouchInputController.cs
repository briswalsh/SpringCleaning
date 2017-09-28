using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputController : MonoBehaviour
{
    void Update()
    {
        OVRInput.Update(); // need to be called for checks below to work
        var leftDir = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        var rightDir = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        if (OVRInput.Get(OVRInput.Button.PrimaryTouchpad))
        {
            print("left button pressed");
        }
        if (OVRInput.Get(OVRInput.Button.SecondaryTouchpad))
        {
            print("right button pressed");
        }
    }
}