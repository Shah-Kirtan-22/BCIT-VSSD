using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    SpawnObject spawnObject;  
    public Material wireFrameMaterial;  // used for x-ray 
    public Material transparentMaterial;  // used for transparency 
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

        treeUI.onValueChanged.AddListener(delegate { OnDropDownValueChange();} );
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
        Renderer[] tempRenderer = GameObject.FindObjectsOfType<Renderer>();
        foreach(Renderer r in tempRenderer)
        {
            r.material = wireFrameMaterial;
        }
    }   
    public void Transparent()
    {
        Renderer[] tempRenderer = GameObject.FindObjectsOfType<Renderer>();
        foreach(Renderer r in tempRenderer)
        {
            r.material = transparentMaterial;
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

    public void OnDropDownValueChange()
    {
        string tempText = treeUI.options[treeUI.value].text.ToString();
        Debug.Log(tempText);
        //Renderer tempRenderer = GameObject.Find(treeUI.options[treeUI.value].text).GetComponent<Renderer>();
        //tempRenderer.material.color = Color.red;
    }
}
