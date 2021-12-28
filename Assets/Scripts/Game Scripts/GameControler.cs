using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{
    public int HP;

    public bool StartGame;

    public GameObject Helicopter;
    public Vector2 StartHelPosition;
    public GameObject TNT;
    public Vector2 StartTNTPosition;

    public GameObject[] Helicopters;
    
    [Space]
    public GameObject GameOverPanel;
    public GameObject MissionCompletedPanel;

    public GameObject canvas;
    public GameObject[] Health = new GameObject[3];
    public GameObject[] Stars = new GameObject[3];

    public Text VST;   //Text
    public Text HST;

    private void Awake()
    {
        Helicopter = Instantiate(Helicopters[PlayerPrefs.GetInt("HelicopterID")], StartHelPosition, Quaternion.identity);
        HP = 3;
        for (int i = 0; i < 3; i++)
            Health[i].transform.GetChild(0).gameObject.SetActive(true);
        canvas.SetActive(false);
    }

    void Start()
    {
        Time.timeScale = 1;
        //GetComponent<GameControler>().canvas.SetActive(true);

        HelicopterControler helCtrl = Helicopter.GetComponent<HelicopterControler>();
        helCtrl.VerticaSpeedText = VST;
        helCtrl.HorizontalSpeedText = HST;
    }

    void Update()
    {
        transform.position = new Vector3(Helicopter.transform.position.x + 1, Helicopter.transform.position.y, -10);
    }


    public void Damage()
    {
        HP--;
        Health[HP].transform.GetChild(0).gameObject.SetActive(false);
        if (HP == 0)
            GameOver();
    }

    void GameOver()
    {
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);
    }


    public void ResetParametrs()
    {
        Time.timeScale = 1;

        HP = 3;
        Helicopter.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        Helicopter.GetComponent<HelicopterControler>().HorizontalAxis = 0;
        Helicopter.GetComponent<HelicopterControler>().VerticalAxis = 0;
        Helicopter.transform.rotation = new Quaternion(0, 0, 0, 0);

        for (int i = 0; i < 3; i++)
            Health[i].transform.GetChild(0).gameObject.SetActive(true);
        Helicopter.GetComponent<BoxCollider2D>().enabled = true;
        Helicopter.GetComponent<HelicopterControler>().TNT = null;
        Helicopter.transform.position = StartHelPosition;

        TNT.transform.SetParent(null);
        TNT.AddComponent<Rigidbody2D>();
        TNT.GetComponent<BoxCollider2D>().enabled = true;
        TNT.GetComponent<DynamiteCollisionCheck>().OnBoard = false;
        TNT.transform.position = StartTNTPosition;
    }

    public void MissionCompleted()
    {
        Time.timeScale = 0;
        MissionCompletedPanel.SetActive(true);
        for (int i = 0; i<3; i++)
        {
            if (Health[i].transform.GetChild(0).gameObject.activeSelf == true)
            Stars[i].transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}