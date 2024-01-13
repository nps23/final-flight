using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO potentially handle spawning funcitonality here as well
public class LaserManager : MonoBehaviour
{
    //[SerializeField] GameObject laserPrefab;
    //[SerializeField] GameObject sourceObject;
    //[SerializeField] float laserSpeed;
    
    //public void Fire()
    //{
    //    if (laserPrefab != null && sourceObject != null)
    //    {
    //        GameObject laserShot = Instantiate(laserPrefab, sourceObject.transform.position + gameObject.transform.forward, gameObject.transform.rotation);
    //        Rigidbody rb = laserShot.GetComponent<Rigidbody>();
    //        rb.velocity = sourceObject.transform.forward * laserSpeed;
    //    }
    //}

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && other.tag != "Environment")
        {
            print(other.tag);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
