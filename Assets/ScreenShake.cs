using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField]
    private GameObject camera;

    // Start is called before the first frame update


    // Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        ShakeCam();
    //    }
    //}

    public void ShakeCam()
    {
        iTween.PunchPosition(camera, new Vector3(0, 10, 0), 0.5f);
    }


    public void SmallShake()
    {
        iTween.PunchPosition(camera, new Vector3(0, 5, 0), 0.5f);
    }
}
