using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class GameData
{
    public int previousLevel;

    //public int level;
    public int playerHealth;

    public bool secret2 = false, secret4 = false;

    public ArrayList enemiesDestroyedScene2 = new ArrayList();
    public ArrayList enemiesDestroyedScene3 = new ArrayList();
    public ArrayList enemiesDestroyedScene5 = new ArrayList();
    public ArrayList enemiesDestroyedScene6 = new ArrayList();
    public ArrayList enemiesDestroyedScene7 = new ArrayList();
    public ArrayList enemiesDestroyedScene8 = new ArrayList();

    public ArrayList instructionsShown = new ArrayList();

    public int respawnLevel = 1;

    public float bright;

    [System.NonSerialized] public GameObject spook, canvas, mainCamera, cursor, audioManager;

    public void LevelLoaded(int level)
    {
        if (level != 0)
        {
            ActivateAllObjects();
            Cursor.visible = false;

            //this.level = level;

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetHealth(playerHealth);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = GameObject.Find("StartPos" + previousLevel.ToString()).transform.position;

            switch (level) { 
                case 1:
                    if (instructionsShown.Count >= 1) GameObject.Destroy(GameObject.Find((string)instructionsShown[0]));
                    break;
                case 2:
                    for (int i = 0; i < enemiesDestroyedScene2.Count; i++) GameObject.Destroy(GameObject.Find((string)enemiesDestroyedScene2[i]));
                    if(instructionsShown.Count >=2) GameObject.Destroy(GameObject.Find((string)instructionsShown[1]));
                    if (secret2) GameObject.Find("Grid").transform.Find("Secret").gameObject.SetActive(false);
                    respawnLevel = 2;
                    break;
                case 3:
                    for (int i = 0; i < enemiesDestroyedScene3.Count; i++) GameObject.Destroy(GameObject.Find((string)enemiesDestroyedScene3[i]));
                    if (instructionsShown.Count >= 3) GameObject.Destroy(GameObject.Find((string)instructionsShown[2]));
                    break;
                case 4:
                    respawnLevel = 4;
                    if (secret4) GameObject.Find("Grid").transform.Find("Secret").gameObject.SetActive(false);
                    break;
                case 5:
                    for (int i = 0; i < enemiesDestroyedScene5.Count; i++) GameObject.Destroy(GameObject.Find((string)enemiesDestroyedScene5[i]));
                    break;
                case 6:
                    for (int i = 0; i < enemiesDestroyedScene6.Count; i++) GameObject.Destroy(GameObject.Find((string)enemiesDestroyedScene6[i]));
                    break;
                case 7:
                    for(int i = 0; i < enemiesDestroyedScene7.Count; i++) GameObject.Destroy(GameObject.Find((string)enemiesDestroyedScene7[i]));
                    break;
                case 8:
                    for(int i = 0; i < enemiesDestroyedScene8.Count; i++) GameObject.Destroy(GameObject.Find((string)enemiesDestroyedScene8[i]));
                    break;
            }

            if(playerHealth == 0)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Die();
            }

            Save();
        }
        else
        {
            if (spook != null) spook.SetActive(false);
            if (canvas != null) canvas.SetActive(false);
            if (mainCamera != null) mainCamera.SetActive(false);
            if (cursor != null) cursor.SetActive(false);
            if (audioManager != null) audioManager.SetActive(false);

            Cursor.visible = true;
        }

        if (previousLevel == 0)
        {
            mainCamera.GetComponent<Brightness>().brightness = bright;
            //Debug.Log("BRIGHTNESS: " + bright);
            //Debug.Log("VOLUME: " + AudioListener.volume);
        }

        previousLevel = level;

    }

    public void addEnemyDestroyed(int level, GameObject go)
    {
        switch (level)
        {
            case 2:
                enemiesDestroyedScene2.Add(go.name);
                break;
            case 3:
                enemiesDestroyedScene3.Add(go.name);
                break;
            case 5:
                enemiesDestroyedScene5.Add(go.name);
                break;
            case 6:
                enemiesDestroyedScene6.Add(go.name);
                break;
        }
    }


    public void Save()
    {
        SaveSystem.Save(this);
    }

    public void Load()
    {
        GameData data = SaveSystem.Load();

        previousLevel = 0;

        //level = data.level;
        playerHealth = data.playerHealth;

        secret2 = data.secret2;
        secret4 = data.secret4;

        enemiesDestroyedScene2 = data.enemiesDestroyedScene2;
        enemiesDestroyedScene3 = data.enemiesDestroyedScene3;
        enemiesDestroyedScene5 = data.enemiesDestroyedScene5;
        enemiesDestroyedScene6 = data.enemiesDestroyedScene6;
        enemiesDestroyedScene6 = data.enemiesDestroyedScene7;
        enemiesDestroyedScene6 = data.enemiesDestroyedScene8;

        respawnLevel = data.respawnLevel;

        instructionsShown = data.instructionsShown;
    }

    public void NewGame()
    {
        previousLevel = 0;

        //level = 1;
        playerHealth = 5;

        secret2 = false;
        secret4 = false;

        enemiesDestroyedScene2 = new ArrayList();
        enemiesDestroyedScene3 = new ArrayList();
        enemiesDestroyedScene5 = new ArrayList();
        enemiesDestroyedScene6 = new ArrayList();
        enemiesDestroyedScene7 = new ArrayList();
        enemiesDestroyedScene8 = new ArrayList();

        respawnLevel = 1;

        instructionsShown = new ArrayList();
    }

    private void ActivateAllObjects()
    {
        if (spook != null) spook.SetActive(true);
        if (canvas != null) canvas.SetActive(true);
        if (mainCamera != null) mainCamera.SetActive(true);
        if (cursor != null) cursor.SetActive(true);
        if (audioManager != null) audioManager.SetActive(true);
    }

}
