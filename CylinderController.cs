using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderController : MonoBehaviour
{
    public GameObject key;  //Reference to the key
    public float velocity;  //velocity of the cylinder

    void Update()
    {
        transform.Translate(2.0f * velocity * Time.deltaTime, 0.0f, 0.0f);  //Movement fo the cylinder in the air

        if (transform.position.x > 40.0f || transform.position.x < 20.0f)   //Change velocity sign if cylinder exceeds 40.0f and is less than 20.0f on x-axis
        {
            velocity = velocity * -1.0f;
        }
     
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))  //If cylinder is struck by projectile
        {
            ObjectsController.countRemainingObjects--;  //Decrement from this public static variable

            if (ObjectsController.countRemainingObjects == 0)   //If this public static variable decrements to 0
            {
                key.SetActive(true);    //Sets the key to be active
                key.transform.position = transform.position;    //Assigns the position of the key to be that of the cylinder
            }

            Destroy(other.gameObject);  //Destroys projectile
            Destroy(gameObject);    //Destroys cylinder
        }
    }
}
