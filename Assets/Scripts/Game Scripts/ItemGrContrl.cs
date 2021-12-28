using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrContrl : MonoBehaviour
{
    public List<ItemGround> Grounds = new List<ItemGround>();

    private void Awake()
    {
        Camera.main.GetComponent<LoadLevel>().ItemGrContrl = gameObject;
    }
}
