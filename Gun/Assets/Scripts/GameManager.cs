using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    SpawnObject spawnObject;
    [SerializeField]
    Slider zoomSlider;
    public Material wireFrameMaterial;

    private void Start()
    {
        spawnObject = GameObject.Find("SpawnLocation").GetComponent<SpawnObject>();
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

}
