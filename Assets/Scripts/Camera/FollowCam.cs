using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Transform playerTransform;  // Assign the player's transform in the Inspector

    public Vector2 cameraOffset;  // The offset between the camera and the player

    public float smoothSpeed = 0.125f;  // The speed at which the camera moves towards the player



    private void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
    }


    private void LateUpdate()
    {
        // Calculate the position the camera should be in
        Vector3 desiredPosition = new Vector3(playerTransform.position.x + cameraOffset.x, playerTransform.position.y + cameraOffset.y, transform.position.z);

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        Vector3 pPosition = playerTransform.transform.position;
        pPosition.z = transform.position.z;
        transform.position = pPosition;
        // Set the camera's position to the smoothed position
        //transform.position = smoothedPosition;
    }
}