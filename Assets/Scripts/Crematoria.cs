using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crematoria : MonoBehaviour
{
    [Header("Flame On")]
    public GameObject fire;
    public GameObject youWillBeBaked; //User camera object
    public AudioSource thereWillBeCake; //The flame sound of your demise
    private bool oneTime;
    private GameObject wildfire;

    // Use this for initialization
    void Start()
    {
        thereWillBeCake = GetComponent<AudioSource>();
        oneTime = false;
        wildfire = Instantiate(fire);
    }

    // Update is called once per frame
    void Update()
    {
        //oneTime = true;
        Vector3 userPosn = youWillBeBaked.transform.position;
        //if (oneTime == false)
        //{
         //   oneTime = true;
         //   GameObject fireFront = Instantiate(fire);
            //GameObject fireLeft = Instantiate(fire);
            //GameObject fireRight = Instantiate(fire);
            //GameObject fireBack = Instantiate(fire);

            //fireLeft.transform.position = userPosn + Vector3.left;
            //fireRight.transform.position = userPosn - Vector3.forward;
            //fireBack.transform.position = userPosn + Vector3.right;
       // }
        wildfire.transform.position = userPosn + 0.1f * Vector3.left;
    }
}