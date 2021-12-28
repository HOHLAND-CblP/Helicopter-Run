using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoiseColor : MonoBehaviour
{
    int Num;
    public GameObject Hel;
    SpriteRenderer CurSR;
    [SerializeField]
    Button ChoiseButton1, ChoiseButton2;


    public void NewHelicopter(GameObject hel)
    {
        Hel = hel;
        ChoiseButton1.interactable = false;
        ChoiseButton2.interactable = true;
        CurSR = Hel.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void ChoiseSprirte(int num)
    {
        Num = num;
        CurSR = Hel.transform.GetChild(Num).GetComponent<SpriteRenderer>();
    }

    public void SetColor(GameObject col)
    {
        CurSR.color = col.GetComponent<Image>().color;
    }
}
