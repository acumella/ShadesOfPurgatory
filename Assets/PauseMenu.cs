using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;
    public bool showingInstruction = false;
    private GameObject mouse;
    public GameObject pauseMenuUI;
    private PlayerController player;
    private GameData gd;

    private void Start()
    {
        gd = GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd;
        mouse = GameObject.FindGameObjectWithTag("Mouse");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !showingInstruction && !player.isDying) {
            if (isPaused) Resume();
            else Pause();
        }    
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        mouse.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false;
    }

    private void Pause()
    {
        player.jumpReleased = true;
        mouse.SetActive(false);
        pauseMenuUI.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
        isPaused = true;
    }

    public void ReturnToMenu()
    {
        Resume();
        Cursor.visible = true;
        GameMaster.LoadScene(0);

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
