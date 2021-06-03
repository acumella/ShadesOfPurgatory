using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private GameObject gameobject;
    private int previousLevel = 1;

    void Awake()
    {
        gameobject = GameObject.Find("old"+this.gameObject.name);
        if (gameobject == null)
        {
            gameobject = this.gameObject;
            gameobject.name = "old"+this.gameobject.name;
            DontDestroyOnLoad(gameobject);
        }
        else
        {
            if (this.gameObject.name != "old"+ this.gameobject.name)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
