using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedDifficulty : MonoBehaviour
{
    public int Dif;

    void Start()
    {
        if (PlayerPrefs.GetInt("Difficult") == Dif)
            gameObject.GetComponent<Image>().color = Color.red;
        else
            gameObject.GetComponent<Image>().color = Color.white;
    }


    void Update()
    {
        if (PlayerPrefs.GetInt("Difficult") == Dif)
            gameObject.GetComponent<Image>().color = Color.red;
        else
            gameObject.GetComponent<Image>().color = Color.white;
    }
}
