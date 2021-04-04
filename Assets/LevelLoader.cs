using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public int intLevelToLoad;
    public string stringLevelToLoad;
    public bool useIntegerToLoadLevel = false;

    CinemachineConfiner confiner;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if (useIntegerToLoadLevel)
        {
            SceneManager.LoadScene(intLevelToLoad);
        }
        else
        {
            SceneManager.LoadScene(stringLevelToLoad);
        }
    }

}
