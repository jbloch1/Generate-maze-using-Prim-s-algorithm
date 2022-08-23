using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public GameObject key; //This variable refers to the key
    public float velocity; //this variable is the velocity of the sphere

    void Update()
    {
        transform.Translate(2.0f * velocity * Time.deltaTime, 0.0f, 0.0f); //This is the movement of the sphere


        //Each time the sphere goes beyond these limits, the sign of the velocity changes
        if (transform.position.x > 45.0f || transform.position.x < 20.0f)
        {
            velocity *= -1.0f;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //Checks if the sphere collided with a projectile from the player
        if (other.gameObject.CompareTag("Projectile"))
        {
            ObjectsController.countRemainingObjects--; //If so, decrement this variable to notify that it is going to disappear

            if (ObjectsController.countRemainingObjects == 0)   //If the sphere decrements this variable to 0
            {
                key.SetActive(true);    //key comes into the scene
                key.transform.position = transform.position;    //The position of the key is the position of the sphere in this case
            }
            Destroy(other.gameObject); //Projectile is destroyed
            Destroy(gameObject);    //Sphere is destroyed
        }
    }
}
