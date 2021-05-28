using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameMaster 
{
    public static void Respawn()
    {
        LoadScene(GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.respawnLevel);
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.previousLevel = 0;
        RespawnEnemies();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Reset();
    }

    private static void RespawnEnemies()
    {
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.enemiesDestroyedScene2 = new ArrayList();
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.enemiesDestroyedScene3 = new ArrayList();
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.enemiesDestroyedScene5 = new ArrayList();
        GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.enemiesDestroyedScene6 = new ArrayList();
    }

    private static void SecretEntrance(int level)
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        switch (level)
        {
            case 2:
                if(currentLevel == 6) GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.secret2 = true;
                break;
        }
    }

    public static void LoadScene(int levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
        SecretEntrance(levelToLoad);
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
