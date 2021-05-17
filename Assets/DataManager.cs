using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private GameData gd;

    void Start()
    {
        Gd = new GameData();
        gd.spook = GameObject.Find("oldspook");
        if (gd.spook != null) gd.spook.SetActive(false);
        gd.canvas = GameObject.Find("oldCanvas");
        if (gd.canvas != null) gd.canvas.SetActive(false);
        gd.mainCamera = GameObject.Find("oldMain Camera");
        if (gd.mainCamera != null) gd.mainCamera.SetActive(false);
        gd.cursor = GameObject.Find("oldCursor");
        if (gd.cursor != null) gd.cursor.SetActive(false);
        gd.audioManager = GameObject.Find("oldAudioManager");
        if (gd.audioManager != null) gd.audioManager.SetActive(false);

        Cursor.visible = true;
    }

    public GameData Gd { get => gd; set => gd = value; }


    private void OnLevelWasLoaded(int level)
    {
        if (gameObject.name != "GameData")
        {
            Gd.LevelLoaded(level);
        }
    }

}
