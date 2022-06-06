using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    SpawnObject spawnObject;

    private void Start()
    {
        spawnObject = GameObject.Find("SpawnLocation").GetComponent<SpawnObject>();
    }


    public void ResetGame()
    {
        Time.timeScale = 0;
        RestartGame();
        /*
        Destroy(spawnObject.spawnedObjects[0]);  // destroy the gameobject in the scene (replace with the index number if more than one object)
        spawnObject.spawnedObjects.Clear();  // clear the list first

        spawnObject.Spawn();
        */
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
