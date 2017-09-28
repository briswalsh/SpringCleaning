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

    // Use this for initialization
    void Start()
    {
        thereWillBeCake = GetComponent<AudioSource>();
        oneTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(oneTime == false)
        {
            oneTime = true;
            Vector3 userPosn = youWillBeBaked.transform.position;

            GameObject fireFront = Instantiate(fire);
            GameObject fireLeft = Instantiate(fire);
            GameObject fireRight = Instantiate(fire);
            GameObject fireBack = Instantiate(fire);

            fireFront.transform.position = userPosn + Vector3.forward;
            fireLeft.transform.position = userPosn + Vector3.left;
            fireRight.transform.position = userPosn - Vector3.forward;
            fireBack.transform.position = userPosn + Vector3.right;
        }

    }
}