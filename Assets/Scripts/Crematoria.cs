using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crematoria : MonoBehaviour
{
    [Header("Flame On")]
    public GameObject fire;
    public GameObject youWillBeBaked; //User camera object
    public AudioSource thereWillBeCake; //The flame sound of your demise

    // Use this for initialization
    void Start()
    {
        thereWillBeCake = GetComponent<AudioSource>();
        Vector3 userPosn = youWillBeBaked.transform.position;

        GameObject fireFront = Instantiate(fire);
        GameObject fireLeft = Instantiate(fire);
        GameObject fireRight = Instantiate(fire);
        GameObject fireBack = Instantiate(fire);

        fireFront.transform.position = userPosn + Vector3(2, 0, 0);
        fireLeft.transform.position = userPosn + Vector3(0, 2, 0);
        fireRight.transform.position = userPosn + Vector3(0, -2, 0);
        fireBack.transform.position = userPosn + Vector3(-2, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}