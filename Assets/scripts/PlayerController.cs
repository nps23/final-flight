using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrenchPlayerController : MonoBehaviour
{
    [SerializeField] float forwardSpeed;
    [SerializeField] float sideSpeed;
    [SerializeField] bool disableAnimation;

    // callbacks
    [SerializeField] UnityEvent onRaycastHit;
    [SerializeField] UnityEvent onRaycastNotHit;
    [SerializeField] UnityEvent onSpeedIncreaseApplied;
    [SerializeField] UnityEvent onGameOver;

    // needed to align the ship with trench spawn vectors
    [SerializeField] GameObject startingTrenchPrefab;

    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed;
    [SerializeField] float fireCooldown;
    [SerializeField] AudioClip laserSound;

    [SerializeField] public bool controllable;

    private Rigidbody rb;
    private float deadzone = 0.1f;
    private Animator animator;
    private float maxSpeed = 200;
    private float currentSpeed;
    private AudioSource audioSource;
    
   
    private bool canFire = true;
    private float fireTimer;

    Vector3 originalForward;
    Vector3 originalRight;

    private void Start()
    {
        currentSpeed = forwardSpeed;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = forwardSpeed * transform.forward;
        audioSource = gameObject.GetComponent<AudioSource>();


        // set the original vectors so that the animation doesn't mess with them
        if (startingTrenchPrefab != null)
        {
            originalForward = startingTrenchPrefab.transform.forward;
        }
        originalRight = transform.right;


        animator = GetComponent<Animator>();
        
    }


    private void FixedUpdate()
    {
        // if the ship isn't controllable, short circuit
        if (!controllable)
        {
            return;
        }
        
        
        float direction = Input.GetAxisRaw("Horizontal");

        if (animator != null && !disableAnimation)
        {
            print(direction);
            animator.SetFloat("Direction", direction);
        }

        if (Mathf.Abs(direction) > deadzone)
        {
            rb.velocity = (currentSpeed * originalForward ) + (sideSpeed * direction * originalRight);
            
        }
        else
        {
            rb.velocity = (currentSpeed * originalForward);
        }

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

        // Perform raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.gameObject.tag != "Environment" && hit.collider.gameObject.tag != "Laser")
            {
                onRaycastHit?.Invoke();
            }
            else
            {
                onRaycastNotHit?.Invoke();
            }
        }
        else
        {
            onRaycastNotHit?.Invoke();
        }

    }

    public void IncreaseSpeed()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += 10;
            onSpeedIncreaseApplied?.Invoke();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        onGameOver?.Invoke();
    }

    public void SetControllable(bool controlState)
    {
        controllable = controlState;
        rb.velocity = 0 * transform.forward;
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
        if (laserSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(laserSound);

        }

        // Destroy the laser shot after 1 second
        Destroy(laserShot, 1f);

    }

}
