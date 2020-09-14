using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//website for the jumping of the player
//https://www.noob-programmer.com/unity3d/how-to-make-player-object-jump-in-unity-3d/
public class PlayerController : MonoBehaviour
{
    public float speed; //How fast the player can move
    public float jumpSpeed; //How fast the player can jump
    bool isGrounded;    //Flag for determining if the player is on ground
    public float projectileSpeed;   //Speed of the generated projectile
    public GameObject projectile;   //Reference to a projectile
    Rigidbody rb;   //Reference to the player's rigidbody component

    bool canShoot;  //Flag for determining if the player can shoot

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        canShoot = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGrounded) //If the player is on ground
        {
            if (Input.GetKeyDown(KeyCode.Space))    //If user presses the space key
            {
                rb.AddForce(Vector3.up * jumpSpeed * Time.fixedDeltaTime);  //Player jumps up
                isGrounded = false; //Set flag isGrounded to false to indicate that the player is not grounded and thus cannot jump 
            }

            //Movement of player
            transform.Translate(Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime, 0.0f, Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime);
        }
        if(ObjectsController.countRemainingObjects > 0) //If there are still some objects that were not yet shot
        {
            if (canShoot)   //If player can shoot
            {
                if (Input.GetMouseButtonDown(0))    //If user left clicks mouse
                {
                    //Compute the direction projectile travels
                    Vector3 shootingDirection = new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y, Camera.main.transform.forward.z) * projectileSpeed;
                    //Create projectile as a rigidbody
                    Rigidbody movingProjectile = Instantiate(projectile.GetComponent<Rigidbody>(), Camera.main.transform.position + (shootingDirection.normalized), projectile.transform.rotation) as Rigidbody;
                    movingProjectile.AddForce(Camera.main.transform.forward * projectileSpeed); //Projectile moves by rigidbody force
                    canShoot = false;   //Player cannot shoot since a projectile exists
                    Invoke("CanShootProjectile", 1.2f); //Player must wait for 1.2 seconds until it can shoot again
                }
            }
        }
    }

    void CanShootProjectile()   //Invoked after 1.2f seconds to allow player to shoot
    {
        canShoot = true;
    }

    void OnCollisionEnter(Collision col)
    { 
        if (col.gameObject.CompareTag("Ground") && isGrounded == false) //If player hits the ground and isGrounded is still set to false
        {
            isGrounded = true;  //isGrounded is set to true
        }

        if(col.gameObject.CompareTag("Key"))    //If player walks over key
        {
            Destroy(col.gameObject);    //Destroy the key
            GameController.playerHasKey = true; //Notifies the GameController that the player has the key and can then start generating the maze

        }
    }

    void LateUpdate()
    {
        //Sets position of camera to be at the head of the player
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
    }
}
