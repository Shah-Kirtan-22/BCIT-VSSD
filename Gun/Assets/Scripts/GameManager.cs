using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    SpawnObject spawnObject;
    public Material wireFrameMaterial;
    public Dropdown treeUI;
    
    [SerializeField]
    private List<string> machineGun;
    
    Transform tempObject;

    private void Start()
    {
        spawnObject = GameObject.Find("SpawnLocation").GetComponent<SpawnObject>();
        tempObject = spawnObject.spawnedObjects[0].transform;

        treeUI = GameObject.Find("UITree").GetComponent<Dropdown>();

        CreateDropdown();
    }

    public void ResetGame()
    {
        Time.timeScale = 0;
        RestartGame();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WireFrame()
    {
        GL.wireframe = true;
        Renderer[] tempRenderer = GameObject.FindObjectsOfType<Renderer>();
        foreach(Renderer r in tempRenderer)
        {
            r.material = wireFrameMaterial;
        }
    }

    public void CreateDropdown()
    {
        Transform[] grandChildren = tempObject.GetComponentsInChildren<Transform>();

        foreach (Transform o in grandChildren)
        {
            machineGun.Add(o.gameObject.ToString());
        }

        treeUI.ClearOptions();
        treeUI.AddOptions(machineGun);
    }

}
