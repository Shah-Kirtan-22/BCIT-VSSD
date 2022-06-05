using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    [SerializeField]
    private Material selectMaterial;    // the color to which the selected object should be changed to
    [SerializeField]
    private Material unSelectMaterial;  // ideally get the material of the selected component from its renderer

    private Transform raycastObject;
    private Transform selectedObject;

    private void Update()
    {
        CheckRayCast();
    }

    public void CheckRayCast()
    {
        if (selectedObject != null)
        {
            Renderer selectedObjectRenderer = selectedObject.GetComponent<Renderer>();
            selectedObjectRenderer.material = unSelectMaterial;
            selectedObject = null;
        }


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            raycastObject = raycastHit.transform;

            Renderer raycastObjectRenderer = raycastObject.GetComponent<Renderer>();

            if (raycastObjectRenderer != null)
            {
                raycastObjectRenderer.material = selectMaterial;
            }

            selectedObject = raycastObject;
        }
    }
}
