using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public int Scene;

    public Image LoadImage;

    void Start()
    {
        StartCoroutine(AsyncLoad());
    }


    IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(Scene);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f / 2;
            LoadImage.fillAmount = progress;
            yield return true;
        }
    }
}
