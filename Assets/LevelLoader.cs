﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;

    public int intLevelToLoad;
    public string stringLevelToLoad;
    public bool useIntegerToLoadLevel = false;

    CinemachineConfiner confiner;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(0.5f);
        if (useIntegerToLoadLevel)
        {
            GameMaster.LoadScene(intLevelToLoad);
        }
        else
        {
            GameMaster.LoadScene(stringLevelToLoad);
        }
    }

}
