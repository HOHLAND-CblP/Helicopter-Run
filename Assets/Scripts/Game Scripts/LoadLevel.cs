using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public int LevelNumber;
    public GameObject Map;
    public GameObject PlacesForTNT;
    public GameObject TNTPref;
    public GameObject ItemGrContrl;
    public GameObject LoadCanvas;
    public Image LoadIm; 

    private void Start()
    {
        GetComponent<StartLevelAnimation>().enabled = false;
        GetComponent<GameControler>().enabled = false;
        GetComponent<SaveLevel>().LoadSaveLevel(LevelNumber);
    }

    public void CreateMap(SaveLevelParametrs save)
    {
        GetComponent<GameControler>().Helicopter.transform.position = new Vector3(save.HelPostion.helStartPosition.x, save.HelPostion.helStartPosition.y, save.HelPostion.helStartPosition.z);
        GetComponent<GameControler>().StartHelPosition = new Vector3(save.HelPostion.helStartPosition.x, save.HelPostion.helStartPosition.y, save.HelPostion.helStartPosition.z);
        GetComponent<StartLevelAnimation>().CamMaxPosition = new Vector2(save.CamMaxPos.x, save.CamMaxPos.y);
        LoadIm.fillAmount = 0.6f;
        foreach (var tnt in save.TNTsPosition)
        {
            Vector3 TempPos = new Vector3(tnt.tntStartPosition.x, tnt.tntStartPosition.y, 0);
            GameObject T = Instantiate(TNTPref, TempPos, Quaternion.identity);
            GetComponent<GameControler>().StartTNTPosition = new Vector2(tnt.tntStartPosition.x, tnt.tntStartPosition.y);
            GetComponent<GameControler>().TNT = T;
        }
        LoadIm.fillAmount = 0.7f;
        foreach (var place in save.PlacesForTNT)
        {
            Vector3 TempPos = new Vector3(place.Position.x, place.Position.y, 0);
            Vector3 TempRot = new Vector3(place.Rotation.x, place.Rotation.y, place.Rotation.z);
            Vector3 TempScl = new Vector3(place.Scale.x, place.Scale.y, place.Scale.z);
            GameObject P = Instantiate(ItemGrContrl.GetComponent<ItemGrContrl>().Grounds[place.ID].gameObject, TempPos, Quaternion.Euler(TempRot), PlacesForTNT.transform);
            P.transform.localScale = TempScl;
        }
        LoadIm.fillAmount = 0.8f;
        foreach (var ground in save.Map)
        {
            Vector3 TempPos = new Vector3(ground.Position.x, ground.Position.y, 0);
            Vector3 TempRot = new Vector3(ground.Rotation.x, ground.Rotation.y, ground.Rotation.z);
            Vector3 TempScl = new Vector3(ground.Scale.x, ground.Scale.y, ground.Scale.z);
            GameObject G = Instantiate(ItemGrContrl.GetComponent<ItemGrContrl>().Grounds[ground.ID].gameObject, TempPos, Quaternion.Euler(TempRot), Map.transform);
            G.transform.localScale = TempScl;
        }
        LoadIm.fillAmount = 0.9f;
        GetComponent<StartLevelAnimation>().SetParametrs();
        LoadIm.fillAmount = 1;
        LoadCanvas.SetActive(false);
        GetComponent<StartLevelAnimation>().enabled = true;
    }
}