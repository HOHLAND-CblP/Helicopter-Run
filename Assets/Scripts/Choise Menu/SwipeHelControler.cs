using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class SwipeHelControler : MonoBehaviour
{
    public int ID;

    public string FileName;

    public GameObject cam;

    public bool isCanPainted;
    public SpriteRenderer[] HelSprite;

    [Space]
    public GameObject Background;
    public float AttractionSpeed;
    public float MinSize;
    public float MaxSize;
    public bool InZero;
    float centerX = 0, edgeX = 11f;
    float MinY = 1.3f, MaxY = 2f;

    void Start()
    {
        FileName = Application.persistentDataPath + FileName;
        cam = GameObject.Find("Main Camera");
        if(isCanPainted)
            cam.GetComponent<SaveControler>().LoadHelCol(FileName, gameObject);
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x) < edgeX)
        {
            transform.localScale = new Vector2(MaxSize - (MaxSize - MinSize) * (Mathf.Abs(transform.position.x) / edgeX), MaxSize - (MaxSize - MinSize) * Mathf.Abs(transform.position.x) / edgeX);
            transform.position = new Vector2(transform.position.x, Mathf.Clamp(MinY + (MaxY - MinY) * Mathf.Pow((Mathf.Abs(transform.position.x) / edgeX), 2), MinY, MaxY));     
        }

        if ((transform.position.x < edgeX / 2) && (transform.position.x !=0) && (transform.position.x>=-edgeX/2))
        {
            Background.GetComponent<SwipeMenuControler>().isAttractoin = true;
            Background.GetComponent<SwipeMenuControler>().DistanceToAttraction = transform.position.x;
            InZero = false;
        }
        if (transform.position.x == 0 && !InZero)
        {
            InZero = true;
            if (isCanPainted)
                Background.GetComponent<SwipeMenuControler>().CurHel(gameObject.GetComponent<SwipeHelControler>(), HelSprite.Length);
            else
                Background.GetComponent<SwipeMenuControler>().CurHel(gameObject.GetComponent<SwipeHelControler>(), 0);
            Background.GetComponent<SwipeMenuControler>().ColorPanel.GetComponent<ChoiseColor>().NewHelicopter(gameObject);
        }
    }

    public void LoadColor(List<Save.HelColors> save)
    {
        for(int i = 0; i < HelSprite.Length; i++)
            HelSprite[i].color = new Color(save[i].col.x, save[i].col.y, save[i].col.z, save[i].col.w);
    }
}
