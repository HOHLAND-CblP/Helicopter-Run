using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HelicopterCollisionCheck : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.GetComponentInParent<HelicopterControler>().Damage();
    }
}