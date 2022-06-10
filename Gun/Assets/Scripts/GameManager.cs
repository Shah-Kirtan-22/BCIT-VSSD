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
        tempObject = spawnObject.spawnedObjects[0].transform;  // store the spawned object's transform into a temporary transform variable

        treeUI = GameObject.Find("UITree").GetComponent<Dropdown>();

        CreateDropdown();

        treeUI.onValueChanged.AddListener(delegate { OnDropDownValueChange();} );  // fire an event everytime the value changes
    }

    public void ResetGame()  
    {
        Time.timeScale = 0;   // stop the time for the application
        RestartGame();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;  // set the time to normal 
        // Since there is only 1 scene, the following method is used (more than 1 scene: use a string)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void WireFrame()
    {
        Renderer[] tempRenderer = GameObject.FindObjectsOfType<Renderer>();
        foreach(Renderer r in tempRenderer)
        {
            r.material = wireFrameMaterial;  // sets the material to wireframe
        }
    }   

    public void Transparent()
    {
        Renderer[] tempRenderer = GameObject.FindObjectsOfType<Renderer>();
        foreach(Renderer r in tempRenderer)
        {
            r.material = transparentMaterial;  // sets the material to transparent
        }
    }

    // In this method, get all of the children of the spawned gameobject and store it in an array
    // next, iterate through the array and add the name to the list
    public void CreateDropdown()
    {
        Transform[] grandChildren = tempObject.GetComponentsInChildren<Transform>();

        foreach (Transform o in grandChildren)
        {
            machineGun.Add(o.gameObject.ToString());
        }

        treeUI.ClearOptions();  // clear the options everytime the function is called
        treeUI.AddOptions(machineGun);  // add the list of string to the dropdown
    }

    // This method is responsible for displaying the name of the selected option in the console
    // In future, the method can be used to call CheckRaycast method for highlighting the game object
    public void OnDropDownValueChange()
    {
        string tempText = treeUI.options[treeUI.value].text.ToString();
        Debug.Log(tempText);
        //Renderer tempRenderer = GameObject.Find(treeUI.options[treeUI.value].text).GetComponent<Renderer>();
        //tempRenderer.material.color = Color.red;
    }
}
