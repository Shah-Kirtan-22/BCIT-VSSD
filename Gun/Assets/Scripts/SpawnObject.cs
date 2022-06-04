using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> objectsToSpawn;  // a list holding the objects to spawn
    [HideInInspector]
    public List<GameObject> spawnedObjects;  // a list holding the spawned objects

    private int i = 0; // an index referring to the position on the list (should there be more objects: updated in the function)

    private void Awake()
    {
        Spawn();
    }

    public void Spawn()
    {
        GameObject gameObject = Instantiate(objectsToSpawn[i]);  // spawn the gameobject (machine gun) with its original position and rotation
        spawnedObjects.Add(gameObject); // add the spawned gameobject to the spawned list for future use
    }
}
