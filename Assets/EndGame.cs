using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private GameObject mouse;
    public GameObject gameOverUI;
    private PlayerController player;
    public bool activateCheat = false;

    void Start()
    {
        mouse = GameObject.FindGameObjectWithTag("Mouse");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameOverUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) )
        {
            Continue();
            activateCheat = false;
        }

        if (activateCheat) Activate();
    }

    public void Activate()
    {
        gameOverUI.SetActive(true);
        player.jumpReleased = true;
        mouse.SetActive(false);
        Cursor.visible = true;
        Time.timeScale = 0;
        GameObject.Find("oldCanvas").GetComponent<PauseMenu>().showingInstruction = true;
    }

    private void Continue()
    {
        gameOverUI.SetActive(false);
        mouse.SetActive(true);
        Cursor.visible = false;
        Time.timeScale = 1;
        GameObject.Find("oldCanvas").GetComponent<PauseMenu>().showingInstruction = false;
    }


}
