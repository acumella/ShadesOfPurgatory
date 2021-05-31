using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public GameData Gd;

    void Start()
    {
        Gd = new GameData();
        Gd.spook = GameObject.Find("oldspook");
        if (Gd.spook != null) Gd.spook.SetActive(false);

        Gd.mainCamera = GameObject.Find("oldMain Camera");
        if (Gd.mainCamera != null) Gd.mainCamera.SetActive(false);
        Gd.cursor = GameObject.Find("oldCursor");
        if (Gd.cursor != null) Gd.cursor.SetActive(false);
        Gd.audioManager = GameObject.Find("oldAudioManager");
        if (Gd.audioManager != null) Gd.audioManager.SetActive(false);

        Gd.spriteSoul1 = GameObject.Find("oldCanvas").transform.Find("Souls").gameObject.transform.Find("Soul1").gameObject;
        if (Gd.spriteSoul1 != null) Gd.spriteSoul1.SetActive(false);
        Gd.spriteSoul2 = GameObject.Find("oldCanvas").transform.Find("Souls").gameObject.transform.Find("Soul2").gameObject;
        if (Gd.spriteSoul2 != null) Gd.spriteSoul2.SetActive(false);
        Gd.spriteSoul3 = GameObject.Find("oldCanvas").transform.Find("Souls").gameObject.transform.Find("Soul3").gameObject;
        if (Gd.spriteSoul3 != null) Gd.spriteSoul3.SetActive(false);
        Gd.canvas = GameObject.Find("oldCanvas");
        if (Gd.canvas != null) Gd.canvas.SetActive(false);

        Cursor.visible = true;
    }



    private void OnLevelWasLoaded(int level)
    {
        if (gameObject.name != "GameData")
        {
            Gd.LevelLoaded(level);
        }
    }
}
