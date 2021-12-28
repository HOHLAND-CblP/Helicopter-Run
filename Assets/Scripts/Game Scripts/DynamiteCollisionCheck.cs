using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteCollisionCheck : MonoBehaviour
{
    public float Height;
    public bool OnBoard;
    GameObject cam;

    void Start()
    {
        cam = Camera.main.gameObject;
        OnBoard = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.parent == null)
            if (collision.transform.tag == "Ground")
                OnBoard = false;
        if (OnBoard)
            for (int i = cam.GetComponent<GameControler>().HP; i>0; i--)
                transform.GetComponentInParent<HelicopterControler>().Damage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Helicopter")
            OnBoard = true;

        if (collision.tag == "Place" && !OnBoard)
            cam.GetComponent<GameControler>().MissionCompleted();
    }
}