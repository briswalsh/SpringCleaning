using UnityEngine;

public class CameraController : MonoBehaviour {
    private int mainCam;

    public Camera[] cameras;

	// Use this for initialization
	void Start ()
    {
        mainCam = 1;
        SetMainCamera(); // Set camera 1 to be the main
	}

    void SetMainCamera ()
    {
        var mainCamIndex = mainCam - 1;
        if (cameras.Length < 1 || mainCam > cameras.Length)
            return;

        cameras[mainCamIndex].enabled = true;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (i != mainCamIndex)
            {
                cameras[i].enabled = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            NextCam();
            SetMainCamera();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow)) // else as to avoid simultaneous changes
        {
            LastCam();
            SetMainCamera();
        }
	}

    void NextCam ()
    {
        if (mainCam >= cameras.Length)
        {
            mainCam = 1;
        }
        else
        {
            mainCam++;
        }
    }

    void LastCam ()
    {
        if (mainCam <= 1)
        {
            mainCam = cameras.Length;
        }
        else
        {
            mainCam--;
        }
    }
}
