using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeMenuControler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public GameObject cam;
    public GameObject Hels;
    private SwipeHelControler curHel;
    int HelsCount;
    public float Space;
    public float SpeedMove;
    public float AttractionMove;
    public float SpeedSwipe;
    public bool isInertiaSwipe = false;
    public bool isAttractoin = false;
    public float DistanceToAttraction;
    private bool isDrag;
    private int CurHelID;
    public GameObject ColorPanel;

    float MaxX, MinX;

    void Start()
    {
        Time.timeScale = 1;
        cam = GameObject.Find("Main Camera");
        float TempSpace = 0;
        HelsCount = Hels.transform.childCount;
        CurHelID = PlayerPrefs.GetInt("HelicopterID");
        for (int i = 0; i < HelsCount; i++)
        {
            GameObject Temp = Hels.transform.GetChild(i).gameObject;   
            Temp.transform.position = new Vector2(i * Space, Temp.transform.position.y);
            if (Temp.GetComponent<SwipeHelControler>().ID == CurHelID)
                TempSpace = Temp.transform.position.x;
        }
        Hels.transform.position -= new Vector3(TempSpace, 0, 0);

        MaxX = 5;
        MinX = -5 - Space * (HelsCount - 1);
    }


    void Update()
    {
        if (!isDrag)
        {
            if (isInertiaSwipe)
            {
                Hels.transform.position = new Vector2(Mathf.Lerp(Hels.transform.position.x, SpeedSwipe, SpeedMove), Hels.transform.position.y);
                if (((Math.Round(Hels.transform.position.x, 1) >= Math.Round(SpeedSwipe, 1) - 0.5f) && (Math.Round(Hels.transform.position.x, 1) <= Math.Round(SpeedSwipe, 1) + 0.5f) || (Hels.transform.position.x >= MaxX || Hels.transform.position.x <= MinX)));
                    isInertiaSwipe = false;
            }
            else if (isAttractoin)
            {
                Hels.transform.position = new Vector2(Mathf.MoveTowards(Hels.transform.position.x, Hels.transform.position.x - DistanceToAttraction, AttractionMove), Hels.transform.position.y);
            }
        }
        Hels.transform.position = new Vector2(Mathf.Clamp(Hels.transform.position.x, MinX, MaxX), Hels.transform.position.y);
    }

    public void CurHel(SwipeHelControler hel, int coutColors)
    {
        if (coutColors > 0)
            ColorPanel.SetActive(true);
        else
            ColorPanel.SetActive(false);
        isAttractoin = false;
        curHel = hel;
        CurHelID = hel.ID;
    }

    public void ButtonChoise()
    {
        Color[] HelColors = new Color[curHel.GetComponent<SwipeHelControler>().HelSprite.Length];
        for (int i = 0; i < curHel.HelSprite.Length; i++)
            HelColors[i] = curHel.GetComponent<SwipeHelControler>().HelSprite[i].color;
        PlayerPrefs.SetInt("HelicopterID", CurHelID);
        cam.GetComponent<SaveControler>().SaveHelCol(curHel.GetComponent<SwipeHelControler>().FileName, HelColors);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        SpeedSwipe = SpeedSwipe/1000 + Hels.transform.position.x;
        isInertiaSwipe = true;
        isDrag = false;
    }

    

    public void OnDrag(PointerEventData eventData)
    {
        isDrag = true;
        isInertiaSwipe = false;
        Hels.transform.position = new Vector3(Mathf.Lerp(Hels.transform.position.x, Hels.transform.position.x + eventData.delta.x, SpeedMove), Hels.transform.position.y, 0);
        SpeedSwipe = eventData.delta.x / Time.deltaTime;
    }
}