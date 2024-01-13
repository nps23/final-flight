using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// dep
public class Controller : MonoBehaviour
{
    [SerializeField] float pitchSpeed; 
    [SerializeField] float rollSpeed;
    [SerializeField] float moveSpeed;

    // parameters controlling the type and speed of the laser shot
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed;
    [SerializeField] float fireCooldown;


    // TODO set controllable
    public bool controllable = true;
    
    private Rigidbody rb;
    private bool canFire = true;
    private float fireTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  // disable gravity for the spaceship
    }

    private void FixedUpdate()
    {
        // If the game object is not in the controllable state, short cicruit
        if (!controllable)
        {
            return;
        }

        // Read input axes
        float pitch = Input.GetAxis("Vertical");    // "w" and "s" keys for pitch
        float roll = Input.GetAxis("Horizontal");   // "a" and "d" keys for roll

        // Calculate rotation based on input

        // From https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html:
        // Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis;
        // Applied in that order.

        Quaternion pitchRotation = Quaternion.Euler(pitch * pitchSpeed, 0f, 0f);
        Quaternion rollRotation = Quaternion.Euler(0f, 0f, -roll * rollSpeed);

        // Multiplying the Quaternions creates a new one that combines the each roation
        Quaternion targetRotation = rb.rotation * pitchRotation * rollRotation;

        // Apply rotation to the spaceship - spherical interpolation
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime));

        // Move the spaceship forward at a constant speed (user cannot change this)
        rb.MovePosition(transform.position + transform.forward * moveSpeed * Time.fixedDeltaTime);

        // TODO encapsulate into seperate script
        // Fire laser if the user pressess space
        if (canFire && Input.GetKey(KeyCode.Space))
        {
            SpawnLaserShot();
            StartCooldown();
        }

        // if we can't fire, add the time to the cooldown
        if (!canFire)
        {
            fireTimer += Time.fixedDeltaTime;
            if (fireTimer >= fireCooldown)
            {
                canFire = true;
            }
        }
    }

    private void StartCooldown()
    {
        canFire = false;
        fireTimer = 0f;
    }



    private void SpawnLaserShot()
    {
        GameObject laserShot = Instantiate(laserPrefab, transform.position + transform.forward, transform.rotation);

        // Apply speed to the laser shot
        Rigidbody laserRigidbody = laserShot.GetComponent<Rigidbody>();
        laserRigidbody.velocity = transform.forward * laserSpeed;
        
        // Destroy the laser shot after 1 second
        Destroy(laserShot, 1f);

    }

    // If anything collides with the spaceship, destroy it
    public void OnCollisionEnter(Collision collision)
    {
        // TODO build in check to see if we collided with tutorial ring and, if so, raise unity event instead
        // This should actually be fixed since the laser collider is now trigger
        if (collision.gameObject.tag != "Laser")
        {
            Destroy(gameObject);
        }

        // Raise onGameOver event
    }

}

