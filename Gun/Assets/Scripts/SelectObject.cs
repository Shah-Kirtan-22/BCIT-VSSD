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
        machineGun = GameObject.FindGameObjectWithTag("MachineGun");  // get the gameobject machinegun for future use
    }

    private void Update()
    {
        // store the current mouse position in the vector3 variable
        mousePosition.x = Input.mousePosition.x;
        mousePosition.y = Input.mousePosition.y;

        CheckRayCast();

        if(Input.GetMouseButton(0))  // left mouse button held down
        {
            OnMouseHold(); 
        }
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
        if (selectedObject != null)  // check if there is a raycast: if there is, change its material
        {
            Renderer selectedObjectRenderer = selectedObject.GetComponent<Renderer>();
            selectedObjectRenderer.material = unSelectMaterial;  // change the material back to original
            selectedObject = null;
        }


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // send a ray from the mouse position onto the screen
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))  // will activate as soon as the ray hits something (with a collider)
        {
            raycastObject = raycastHit.transform;  // store the raycast position into another object(transform)
            
            textPosition();

            Renderer raycastObjectRenderer = raycastObject.GetComponent<Renderer>();

            if (raycastObjectRenderer != null)
            {
                raycastObjectRenderer.material = selectMaterial;  // set the material when the raycast hits a gameobject to red
            }

            selectedObject = raycastObject;  // simply copy the material and object properties for deselection
        }
    }

    // This method is called when the player presses and holds down left mouse button 
    // The method is responsible for changing the position of the gameobject according to the mouse position in world space
    public void OnMouseHold()
    {
        // This method uses the same principle as the OnMouseDrag method
        // the z-postion offset is in accordance to the distance from camera to the gameobject
        // if multiple objects - Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, raycastObject.position.z))
        raycastObject.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 5f));
    }

    // This method is called when the player presses and holds down middle mouse button (scroller)
    // The method is responsible for changing the position of the gameobject according to the mouse position in world space
    public void ChangePosition()
    {
        // change the position of the gameobject according to the position of the mouse
        // the offset added to the z-axis is to counteract the camera's z position (at -5)
        machineGun.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 5f)); 
    }
    
    // This method is called when the player presses and holds down right mouse button
    // The method is responsible for altering the rotation value according to the mouse X and mouse Y
    public void ChangeRotation()
    {
        machineGun.transform.Rotate(new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0) * rotationSpeed * Time.deltaTime * 360);
    }

    // This method is called when the player hovers over certain parts of the gameobject
    // The method is responsible for changing the position of the text box as well as thet text itself
    public void textPosition()
    {
        hoverText.text = raycastObject.gameObject.name;
        hoverText.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x + 100f, mousePosition.y - 100f, 4f));
    }
}
