using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crematoria : MonoBehaviour
{
    [Header("Flame On")]
    public GameObject fire;
    public GameObject youWillBeBaked; //User camera object
    public AudioSource thereWillBeCake; //The flame sound of your demise
    public bool flaming;
    private GameObject wildfire;

    [Header("Fade to Black")]
    public Image black; //fade screen to black
    public float duration;

    private Color solid;
    private Color transparent;
    private float t;


    // Use this for initialization
    void Start()
    {
        thereWillBeCake = GetComponent<AudioSource>();
        wildfire = Instantiate(fire);
        flaming = false;

        solid = black.color;
        transparent = black.color;
        transparent.a = 0;
        black.color = transparent;
        t = 0;
    }

    // Update is called once per frame
    void Update()
    { /*
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
        */
        if(flaming)
        {
            if (t > 1)
            {
                flaming = false;
            }
            black.color = Color.Lerp(transparent, solid, t);
            t += Time.deltaTime / duration;
        }
    }

    public void Immolation()
    {
        flaming = true;
    }
}