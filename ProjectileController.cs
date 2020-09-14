using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1.0f);  //Keep gameobject active for 1 seconds.
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))    //Destroy projectile if it collides with wall
        {
            Destroy(gameObject);
            
        }
        else if (other.gameObject.CompareTag("Tree"))   //Destroy projectile if it collides with a tree in the forest
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Bushes")) //Destroy projectile if it collides with bushes
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ground")) //Destroy projectile if it collides with ground
        {
            Destroy(gameObject);
        }

    }
}
