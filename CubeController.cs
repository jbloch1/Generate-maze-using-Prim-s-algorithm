using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public GameObject key;  //Public variable that references the key 
    public float velocity; //The velocity of the cube

    void Update()
    {
        transform.Translate(2.0f * velocity * Time.deltaTime, 0.0f, 0.0f);  //Movement of the cube in the air

        if (transform.position.x > 40.0f || transform.position.x < 20.0f)   //Change sign of velocity if cube position exceeds 40.0f and is less than 20.0f on x-axis
        {
            velocity *= -1.0f;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Projectile"))   //If cube collides with projectile
        {
            ObjectsController.countRemainingObjects--;  //Decrement public static variable

            if (ObjectsController.countRemainingObjects == 0)   //If decremented to 0
            {
                key.SetActive(true);    //Set key to be active
                key.transform.position = transform.position;    //Make the position of the key the position of the cube
            }

            Destroy(other.gameObject);  //Destroy projectile
            Destroy(gameObject);    //Destroy cube
        }
        
    }

}
