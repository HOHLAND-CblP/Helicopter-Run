using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLevel : MonoBehaviour
{

    public List<int> Temp;
    public string FileName;
    public int Level;
    public float CamMaxSize;
    public GameObject Hel;
    public GameObject TNTs;
    List<GameObject> TNT = new List<GameObject>();
    public GameObject Map;
    List<GameObject> Ground = new List<GameObject>();
    public GameObject PlacesForTNT;
    List<GameObject> PlaceForTNT = new List<GameObject>();

    void Start()
    {
        FileName = Application.dataPath + "/Saves/Level" + Level + ".levelsave";
    }


    private void Update()
    {

    }

    /*public void saveLevel()
    {
        BinaryFormatter Br = new BinaryFormatter();
        FileStream fs = new FileStream(FileName, FileMode.Create);

        SaveLevelParametrs save = new SaveLevelParametrs();

        save.LevelNumber = Level;

        save.SaveHelPos(Hel.transform.position);

        TNT.Clear();
        for (int i = 0; i < TNTs.transform.childCount; i++)
        {
            TNT.Add(TNTs.transform.GetChild(i).gameObject);
        }
        save.SaveTNTsPos(TNT);

        Ground.Clear();
        for (int i = 0; i < Map.transform.childCount; i++)
        {
            Ground.Add(Map.transform.GetChild(i).gameObject);
        }
        save.SaveGroundPos(Ground);

        PlaceForTNT.Clear();
        for (int i = 0; i < PlacesForTNT.transform.childCount; i++)
        {
            PlaceForTNT.Add(PlacesForTNT.transform.GetChild(i).gameObject);
        }
        save.SavePlacesForTNTPos(PlaceForTNT);

        Br.Serialize(fs, save);
        fs.Close();
    }*/

    public void LoadSaveLevel(int LevelNumber)
    {
        FileName = Application.dataPath + "/Saves/Levels/Level" + LevelNumber + ".levelsave";
        BinaryFormatter Br = new BinaryFormatter();
        FileStream fs = new FileStream(FileName, FileMode.Open);

        SaveLevelParametrs save = (SaveLevelParametrs)Br.Deserialize(fs);
        fs.Close();

        GetComponent<LoadLevel>().CreateMap(save);
    }
}

[System.Serializable]
public class SaveLevelParametrs
{
    [System.Serializable]
    public struct Vect2
    {
        public float x, y;

        public Vect2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    [System.Serializable]
    public struct Vect3
    {
        public float x, y, z;

        public Vect3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    [System.Serializable]
    public struct Vect4
    {
        public float x, y, z, w;

        public Vect4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
    }

    [System.Serializable]
    public struct HelStartPosition
    {
        public Vect3 helStartPosition;

        public HelStartPosition(Vect3 pos)
        {
            helStartPosition = pos;
        }
    }

    [System.Serializable]
    public struct TNTStartPosition
    {
        public Vect3 tntStartPosition;

        public TNTStartPosition(Vect3 pos)
        {
            tntStartPosition = pos;
        }
    }

    [System.Serializable]
    public struct Ground
    {
        public int ID;

        public Vect3 Position;
        public Vect3 Rotation;
        public Vect3 Scale;


        public Ground(int id, Vect3 pos, Vect3 rot, Vect3 scale)
        {
            ID = id;
            Position = pos;
            Rotation = rot;
            Scale = scale;
        }
    }

    [System.Serializable]
    public struct PlaceForTNT
    {
        public int ID;

        public Vect3 Position;
        public Vect3 Rotation;
        public Vect3 Scale;


        public PlaceForTNT(int id, Vect3 pos, Vect3 rot, Vect3 scale)
        {
            ID = id;
            Position = pos;
            Rotation = rot;
            Scale = scale;
        }
    }


    public int LevelNumber;
    public float CamMaxSize;
    public Vect2 CamMaxPos;
    public HelStartPosition HelPostion;
    public List<TNTStartPosition> TNTsPosition = new List<TNTStartPosition>();
    public List<PlaceForTNT> PlacesForTNT = new List<PlaceForTNT>();
    public List<Ground> Map = new List<Ground>();





   /* public void SaveHelPos(Vector3 helPos)
    {
        Vect3 Temp = new Vect3(helPos.x, helPos.y, helPos.z);
        HelPostion = new HelStartPosition(Temp);
    }

    public void SaveTNTsPos(List<GameObject> TNTs)
    {
        foreach (var tnt in TNTs)
        {
            Vect3 pos = new Vect3(tnt.transform.position.x, tnt.transform.position.y, tnt.transform.position.z);
            TNTsPosition.Add(new TNTStartPosition(pos));
        }
    }

    public void SavePlacesForTNTPos(List<GameObject> Places)
    {
        foreach (var place in Places)
        {
            Vect3 pos = new Vect3(place.transform.position.x, place.transform.position.y, place.transform.position.z);
            Vect3 rot = new Vect3(place.transform.rotation.eulerAngles.x, place.transform.rotation.eulerAngles.y, place.transform.rotation.eulerAngles.z);
            Vect3 scale = new Vect3(place.transform.localScale.x, place.transform.localScale.y, place.transform.localScale.z);
            //PlacesForTNT.Add(new PlaceForTNT(place.GetComponent<ItemGround>().ID, pos, rot, scale));
        }
    }

    public void SaveGroundPos(List<GameObject> map)
    {
        foreach (var ground in map)
        {
            Vect3 pos = new Vect3(ground.transform.position.x, ground.transform.position.y, ground.transform.position.z);
            Vect3 rot = new Vect3(ground.transform.rotation.eulerAngles.x, ground.transform.rotation.eulerAngles.y, ground.transform.rotation.eulerAngles.z);
            Vect3 scale = new Vect3(ground.transform.localScale.x, ground.transform.localScale.y, ground.transform.localScale.z);
            //Map.Add(new Ground(ground.GetComponent<ItemGround>().ID, pos, rot, scale));
        }
    }*/
}