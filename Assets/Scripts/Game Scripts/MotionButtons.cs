using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionButtons : MonoBehaviour
{
    GameObject Cam;
    GameObject Hel;

    void Start()
    {
        Cam = GameObject.Find("Main Camera");
        Hel = Cam.GetComponent<GameControler>().Helicopter;
    }

    public void HorizontalAxisEnter(int k)
    {
        Hel.GetComponent<HelicopterControler>().HorizontalAxis = Mathf.Lerp(Hel.GetComponent<HelicopterControler>().HorizontalAxis, k, 1);//Axis
    }

    public void StopHorizontal()
    {
        Hel.GetComponent<HelicopterControler>().HorizontalAxis = Mathf.Lerp(Hel.GetComponent<HelicopterControler>().HorizontalAxis, 0, 1);//Axis
    }

    public void VerticalAxisEnter(int k)
    {
        Hel.GetComponent<HelicopterControler>().VerticalAxis = Mathf.Lerp(Hel.GetComponent<HelicopterControler>().VerticalAxis, k, 1);//Axis
    }

    public void StopVertical()
    {
        Hel.GetComponent<HelicopterControler>().VerticalAxis = Mathf.Lerp(Hel.GetComponent<HelicopterControler>().VerticalAxis, 0, 3);//Axis
    }

    public void SpaceButton()
    {
        Hel.GetComponent<HelicopterControler>().CargoDischarge();
    }
}
