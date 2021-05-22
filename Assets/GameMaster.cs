using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameMaster 
{
    public static void Respawn()
    {
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.previousLevel = 0;
        LoadScene(GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.respawnLevel);
        RespawnEnemies();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Reset();
    }

    private static void RespawnEnemies()
    {
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.enemiesDestroyedScene2 = new ArrayList();
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.enemiesDestroyedScene3 = new ArrayList();
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.enemiesDestroyedScene4 = new ArrayList();
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.enemiesDestroyedScene5 = new ArrayList();
    }

    public static void LoadScene(int levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public static void LoadScene(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public static void SetBrightness(float brightness)
    {
        GameData gd = GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd;
        gd.bright = brightness;
    }
    
    public static float GetBrightness()
    {
        GameData gd = GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd;
        return gd.bright;
    }
}
