using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonScript : MonoBehaviour
{
    public int Scene;

    public void HardDifficult()
    {
        PlayerPrefs.SetInt("Difficult", 1);
    }

    public void EasyDifficult()
    {
        PlayerPrefs.SetInt("Difficult", 0);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(Scene);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
