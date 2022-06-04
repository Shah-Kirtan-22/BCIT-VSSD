using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    [SerializeField]
    private Material selectMaterial;
    
    private void Update()
    {
        CheckRayCast();
    }

    public void CheckRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            Transform selectedObject = raycastHit.transform;

            Renderer selectedObjectRenderer = selectedObject.GetComponent<Renderer>();

            if (selectedObjectRenderer != null)
            {
                selectedObjectRenderer.material = selectMaterial;
            }
        }
    }
}
