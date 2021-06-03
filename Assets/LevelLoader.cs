using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;

    public int intLevelToLoad;

    CinemachineConfiner confiner;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canMove = false;
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(0.5f);

        GameMaster.LoadScene(intLevelToLoad);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canMove = true;
    }

}
