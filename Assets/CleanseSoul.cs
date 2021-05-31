using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanseSoul : MonoBehaviour
{
    public int numSoul;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (numSoul)
            {
                case 1:
                    GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.soul1 = true;
                    break;
                case 2:
                    GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.soul2 = true;
                    break;
                case 3:
                    GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.soul3 = true;
                    break;
            }

            GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.numSouls++;
            GameObject.FindGameObjectWithTag("GameData").GetComponent<DataManager>().Gd.ActivateSouls();

            SoundManager.PlaySound("soulPickup");

            Destroy(gameObject);
        }
    }
}