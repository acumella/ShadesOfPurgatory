using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameMaster 
{
    public static void Respawn()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Reset();
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.previousLevel = 0;
        SceneManager.LoadScene(GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.respawnLevel);
        RespawnEnemies();
    }

    private static void RespawnEnemies()
    {
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.enemiesDestroyedScene2 = new ArrayList();
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.enemiesDestroyedScene3 = new ArrayList();
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.enemiesDestroyedScene4 = new ArrayList();
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.enemiesDestroyedScene5 = new ArrayList();
    }
}
