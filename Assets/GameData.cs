using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class GameData
{
    public int previousLevel;

    public int level;
    public int playerHealth;

    public ArrayList enemiesDestroyedScene2 = new ArrayList();
    public ArrayList enemiesDestroyedScene3 = new ArrayList();
    public ArrayList enemiesDestroyedScene4 = new ArrayList();
    public ArrayList enemiesDestroyedScene5 = new ArrayList();

    public ArrayList instructionsShown = new ArrayList();

    public int respawnLevel = 1;

    [System.NonSerialized] public GameObject spook, canvas, mainCamera, cursor, audioManager;

    public void LevelLoaded(int level)
    {
        if (level != 0)
        {
            ActivateAllObjects();
            Cursor.visible = false;

            this.level = level;

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetHealth(playerHealth);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = GameObject.Find("StartPos" + previousLevel.ToString()).transform.position;

            switch (level) { 
                case 1:
                    if (instructionsShown.Count >= 1) GameObject.Destroy(GameObject.Find((string)instructionsShown[0]));
                    break;
                case 2:
                    for (int i = 0; i < enemiesDestroyedScene2.Count; i++) GameObject.Destroy(GameObject.Find((string)enemiesDestroyedScene2[i]));
                    if(instructionsShown.Count >=2) GameObject.Destroy(GameObject.Find((string)instructionsShown[1]));
                    respawnLevel = 2;
                    break;
                case 3:
                    for (int i = 0; i < enemiesDestroyedScene3.Count; i++) GameObject.Destroy(GameObject.Find((string)enemiesDestroyedScene3[i]));
                    if (instructionsShown.Count >= 3) GameObject.Destroy(GameObject.Find((string)instructionsShown[2]));
                    break;
                case 4:
                    for (int i = 0; i < enemiesDestroyedScene4.Count; i++) GameObject.Destroy(GameObject.Find((string)enemiesDestroyedScene4[i]));
                    respawnLevel = 4;
                    break;
                case 5:
                    for (int i = 0; i < enemiesDestroyedScene5.Count; i++) GameObject.Destroy(GameObject.Find((string)enemiesDestroyedScene5[i]));
                    break;
            }

            Save();
        }
        else
        {
            spook = GameObject.Find("oldspook");
            if (spook != null) spook.SetActive(false);
            canvas = GameObject.Find("oldCanvas");
            if (canvas != null) canvas.SetActive(false);
            mainCamera = GameObject.Find("oldMain Camera");
            if (mainCamera != null) mainCamera.SetActive(false);
            cursor = GameObject.Find("oldCursor");
            if (cursor != null) cursor.SetActive(false);
            audioManager = GameObject.Find("oldAudioManager");
            if (audioManager != null) audioManager.SetActive(false);

            Cursor.visible = true;
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
            case 4:
                enemiesDestroyedScene4.Add(go.name);
                break;
            case 5:
                enemiesDestroyedScene5.Add(go.name);
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

        level = data.level;
        playerHealth = data.playerHealth;

        enemiesDestroyedScene2 = data.enemiesDestroyedScene2;
        enemiesDestroyedScene3 = data.enemiesDestroyedScene3;
        enemiesDestroyedScene4 = data.enemiesDestroyedScene4;
        enemiesDestroyedScene5 = data.enemiesDestroyedScene5;

        instructionsShown = data.instructionsShown;
    }

    public void NewGame()
    {
        previousLevel = 0;

        level = 1;
        playerHealth = 5;

        enemiesDestroyedScene2 = new ArrayList();
        enemiesDestroyedScene3 = new ArrayList();
        enemiesDestroyedScene4 = new ArrayList();
        enemiesDestroyedScene5 = new ArrayList();

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
