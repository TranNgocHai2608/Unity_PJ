using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : colliable
{
    public string[] sceneNames;

    protected override void onCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            // teleport the player
            gameManager.instance.SaveState();
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            SceneManager.LoadScene(sceneName);  
        }
    }
}
