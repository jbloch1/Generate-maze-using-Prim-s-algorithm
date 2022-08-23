using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The below link is where I got the code for the camera's yaw and pitching. Check that out!
//https://www.youtube.com/watch?v=lYIRm4QEqro
public class CameraController : MonoBehaviour
{
    public GameObject player;   //Reference to the player

    public float horizontalSpeed = 2.0f;    //Horizontal speed on x axis 
    public float verticalSpeed = 2.0f;  //Vertical speed on y axis

    private float yaw = 0.0f;   //Initialize yaw to 0.0
    private float pitch = 0.0f; //Initialize pitch to 0.0

    // Update is called once per frame
    void Update()
    {
        /*Yaw increases so that when the user moves the mouse to whichever horizontal direction, 
        the camera follows it. Otherwise the camera will not. It would go opposite direction of where the user moves the mouse to.*/
        yaw += horizontalSpeed * Input.GetAxis("Mouse X");

        /*Pitch decreases so that when the user moves the mouse to whichever vertical direction, 
        the camera follows it. Otherwise the camera will not. It would go opposite direction of where the user moves the mouse to.*/
        pitch -= verticalSpeed * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);  //Applying yaw and pitch value on the transform on euler angles
    }

    void LateUpdate()
    {
        //Make player follow camera's forward direction if it has not yet been destroyed.
        if (player)
            player.transform.forward = new Vector3(transform.forward.x, player.transform.forward.y, transform.forward.z);
            
    }
}
