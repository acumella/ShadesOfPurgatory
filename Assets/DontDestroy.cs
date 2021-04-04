using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private GameObject gameobject;

    void Awake()
    {
        //When the scene loads it checks if there is an object called "$$anonymous$$USIC".
        gameobject = GameObject.Find("old"+this.gameObject.name);
        if (gameobject == null)
        {
            //If this object does not exist then it does the following:
            //1. Sets the object this script is attached to as the music player
            gameobject = this.gameObject;
            //2. Renames THIS object to "$$anonymous$$USIC" for next time
            gameobject.name = "old"+this.gameobject.name;
            //3. Tells THIS object not to die when changing scenes.
            DontDestroyOnLoad(gameobject);
        }
        else
        {
            //If there WAS an object in the scene called "$$anonymous$$USIC" (because we have come back to
            //the scene where the music was started) then it just tells this object to
            //destroy itself
            if (this.gameObject.name != "old"+ this.gameobject.name)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
