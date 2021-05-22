using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{

    public static bool isPaused = false;
    public bool showingInstruction = false;
    private GameObject mouse;
    public GameObject gameOverUI;
    private PlayerController player;
    private GameData gd;

    private void Start()
    {
        gd = GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd;
        mouse = GameObject.FindGameObjectWithTag("Mouse");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void Continue()
    {
        gameOverUI.SetActive(false);
        mouse.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
        GameMaster.Respawn();
    }

    public void Pause()
    {
        player.jumpReleased = true;
        mouse.SetActive(false);
        gameOverUI.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ReturnToMenu()
    {
        Continue();
        Cursor.visible = true;
        GameMaster.LoadScene("MainMenu");

        //GameObject.Find("oldspook").SetActive(false);
        //GameObject.Find("oldCanvas").SetActive(false);
        //GameObject.Find("oldMain Camera").SetActive(false);
        //GameObject.Find("oldCursor").SetActive(false);
        //GameObject.Find("oldEventSystem").SetActive(false);
        //GameObject.Find("oldAudioManager").SetActive(false);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
