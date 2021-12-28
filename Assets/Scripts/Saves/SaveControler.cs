using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



public class SaveControler : MonoBehaviour
{
    string FileName;

    void Start()
    {
        FileName = Application.persistentDataPath + "/Saves";
    }

    public void SaveHelCol(string fileName, Color[] HelColor)
    {
        FileName = fileName;
        BinaryFormatter Br = new BinaryFormatter();
        FileStream fs = new FileStream(FileName, FileMode.Create);

        Save save = new Save();
        save.SaveHelColor(HelColor);

        Br.Serialize(fs, save);
        fs.Close();
    }

    public void LoadHelCol(string fileName, GameObject Hel)
    {
        FileName = fileName;

        BinaryFormatter Br = new BinaryFormatter();
        FileStream fs = new FileStream(FileName, FileMode.Open);

        Save save = (Save)Br.Deserialize(fs);
        fs.Close();
        if(Hel.GetComponent<HelicopterControler>())
            Hel.GetComponent<HelicopterControler>().LoadColor(save.helColors);
        else
            Hel.GetComponent<SwipeHelControler>().LoadColor(save.helColors);
    }
}



[System.Serializable]
public class Save
{
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
    public struct HelColors
    {
        public Vect4 col;
        
        public HelColors(Vect4 col)
        {
            this.col = col;
        }
    }

    public List<HelColors> helColors = new List<HelColors>();

    public void SaveHelColor(Color[] HelColor)
    {
        for (int i = 0; i < HelColor.Length; i++)
        {
            Vect4 col = new Vect4(HelColor[i].r, HelColor[i].g, HelColor[i].b, HelColor[i].a);

            helColors.Add(new HelColors(col));
        }
    }
}