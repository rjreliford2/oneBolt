using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrossHair : MonoBehaviour
{
    void Update()
    {
        // Get the position of the mouse in world coordinates
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Set the position of the gameobject to the mouse position
        transform.position = new Vector3(mousePos.x, mousePos.y, 0f);
    }
}