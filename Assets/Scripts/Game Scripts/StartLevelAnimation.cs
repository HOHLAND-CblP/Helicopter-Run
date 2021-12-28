    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelAnimation : MonoBehaviour
{
    float TempGravity;
    bool FirstPhase, SecondPhase, ThirdPhase;
    float SpeedTransformX, SpeedTransformY;
    GameObject TNT, Hel;

    public Vector3 CamMaxPosition;
    public float MaxCamSize;
    float MinCamSize;
    float CamSpeed;

    private void Start()
    {
        if (GetComponent<LoadLevel>() == null)
        {
            GetComponent<GameControler>().enabled = false;
            SetParametrs();
        }
    }

    public void SetParametrs()
    {
        Debug.Log(1);
        Hel = GetComponent<GameControler>().Helicopter;
        Hel.GetComponent<HelicopterControler>().enabled = false;
        TempGravity = Hel.GetComponent<Rigidbody2D>().gravityScale;
        Hel.GetComponent<Rigidbody2D>().gravityScale = 0;
        TNT = GetComponent<GameControler>().TNT;

        TNT.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.1f);

        MinCamSize = 2;
        CamSpeed = 0.05f;

        FirstPhase = true;
        SecondPhase = false;
        ThirdPhase = false;
        Time.timeScale = 1;
    }

    
    void Update()
    {
        if (FirstPhase)
        {
            if (TNT.transform.position.y == GetComponent<GameControler>().StartTNTPosition.y || TNT.GetComponent<Rigidbody2D>().velocity.y != 0)
            {
                transform.position = new Vector3(TNT.transform.position.x, TNT.transform.position.y, transform.position.z);
                GetComponent<Camera>().orthographicSize = MinCamSize;
                
            }
            else 
                StartCoroutine(EndOfFirstPhase());
        }
        if (SecondPhase)
        { 
            if (GetComponent<Camera>().orthographicSize != MaxCamSize)
            {
                transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, CamMaxPosition.x, SpeedTransformX * Time.deltaTime * 300), Mathf.MoveTowards(transform.position.y, CamMaxPosition.y, SpeedTransformY * Time.deltaTime* 300), transform.position.z);
                GetComponent<Camera>().orthographicSize = Mathf.MoveTowards(GetComponent<Camera>().orthographicSize, MaxCamSize, CamSpeed * Time.deltaTime*300);
                
            }
            else
                StartCoroutine(EndOfSecondPhase());
        }
        if (ThirdPhase)
        {
            if (GetComponent<Camera>().orthographicSize != 5)
            {
                transform.position = new Vector3(Mathf.MoveTowards(transform.position.x, Hel.transform.position.x + 1, SpeedTransformX * Time.deltaTime * 300), Mathf.MoveTowards(transform.position.y, Hel.transform.position.y, SpeedTransformY * Time.deltaTime * 300), transform.position.z);
                GetComponent<Camera>().orthographicSize = Mathf.MoveTowards(GetComponent<Camera>().orthographicSize, 5, CamSpeed * Time.deltaTime * 300);
            }
            else
                StartCoroutine(EndOfThirdPhase());
        }
    }


    IEnumerator EndOfFirstPhase()
    {
        FirstPhase = false;
        SpeedTransformX = Mathf.Abs(transform.position.x - CamMaxPosition.x) / ((MaxCamSize - GetComponent<Camera>().orthographicSize) / CamSpeed);
        SpeedTransformY = Mathf.Abs(transform.position.y - CamMaxPosition.y) / ((MaxCamSize - GetComponent<Camera>().orthographicSize) / CamSpeed);
        yield return new WaitForSeconds(0.4f);
        SecondPhase = true;
    }

    IEnumerator EndOfSecondPhase()
    {
        SecondPhase = false;
        SpeedTransformX = Mathf.Abs(transform.position.x - (Hel.transform.position.x+1)) / ((GetComponent<Camera>().orthographicSize - 5) / CamSpeed);
        SpeedTransformY = Mathf.Abs(transform.position.y - Hel.transform.position.y) / ((GetComponent<Camera>().orthographicSize - 5) / CamSpeed);
        yield return new WaitForSeconds(1f);
        ThirdPhase = true;
    }

    IEnumerator EndOfThirdPhase()
    {
        ThirdPhase = false;
        yield return new WaitForSeconds(0.5f);
        Hel.SetActive(true);
        Hel.GetComponent<HelicopterControler>().enabled = true;
        GetComponent<GameControler>().enabled = true;
        Hel.GetComponent<Rigidbody2D>().gravityScale = TempGravity;
        GetComponent<GameControler>().canvas.SetActive(true); 
    }
}