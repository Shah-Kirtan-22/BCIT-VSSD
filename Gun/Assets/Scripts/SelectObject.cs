using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectObject : MonoBehaviour
{
    [SerializeField]
    private Material selectMaterial;    // the color to which the selected object should be changed to
    [SerializeField]
    private Material unSelectMaterial;  // ideally get the material of the selected component from its renderer

    private Transform raycastObject;  // highlight this object on hover
    private Transform selectedObject;  // the selected object on hover
    private Vector3 mousePosition;  // variable to convert and store the mouse position as a 3D vector

    [SerializeField]
    private Text hoverText;

    [SerializeField]
    private GameObject machineGun;  // reference to the gameobject (Ideally if more objects, use the index from spawnobject script)

    [SerializeField]
    [Range(0.1f,3.0f)]
    private float rotationSpeed = 5f;  // speed at which the object can be rotated

    private void Start()
    {
        machineGun = GameObject.FindGameObjectWithTag("MachineGun");
    }

    private void Update()
    {
        // store the current mouse position in the vector3 variable

        mousePosition.x = Input.mousePosition.x;
        mousePosition.y = Input.mousePosition.y;

        CheckRayCast();

        if (Input.GetMouseButton(2))  // middle mouse button held down
        {
            ChangePosition();
        }
        else if(Input.GetMouseButton(1))   // right mouse button held down
        {
            ChangeRotation();
        }
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
            //hoverText.text = raycastObject.gameObject.name;  
            textPosition();
            
            if(Input.GetMouseButton(0))  // left mouse button held down
            {
                raycastObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 5f));
            }
            
            Renderer raycastObjectRenderer = raycastObject.GetComponent<Renderer>();

            if (raycastObjectRenderer != null)
            {
                raycastObjectRenderer.material = selectMaterial;
            }

            selectedObject = raycastObject;
        }
    }

    // This method is called when the player presses and holds down middle mouse button (scroller)
    // The method is responsible for changing the position of the gameobject according to the mouse position in world space
    public void ChangePosition()
    {
        machineGun.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
    }
    
    // This method is called when the player presses and holds down right mouse button
    // The method is responsible for altering the rotation value according to the mouse X and mouse Y
    public void ChangeRotation()
    {
        machineGun.transform.Rotate(new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) * rotationSpeed * Time.deltaTime * 360);
    }

    public void textPosition()
    {
        hoverText.text = raycastObject.gameObject.name;
        //hoverText.transform.position = raycastObject.position + new Vector3(1f,1f,0);        
    }
}
