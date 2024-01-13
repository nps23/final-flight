using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventProvider : MonoBehaviour
{
   
    [SerializeField] LinearSqeuenceQueue sequenceQueue;
    [SerializeField] ParticleSystem particleSystem;

    // callbacks
    [SerializeField] private UnityEvent triggerEnterEvent;
    
    
    [SerializeField] private List<string> tagList;

    
    // TODO Migrate functionality to a damageable/damager system
    public void OnTriggerEnter(Collider other)
    {

        
        //if (other.CompareTag("Player"))
        //{

        //    Destroy(gameObject);
        //    if (sequenceQueue != null)
        //    {
        //        sequenceQueue.DequeueNextEvent();
        //    }
        //}

        if (other.CompareTag("Laser"))
        {
            // TODO migrate to damageable event
            if (gameObject.tag == "Damageable")
            {
                if (particleSystem != null)
                {
                    // Unbind the particle position from parent so that destroying has no effect
                    particleSystem.transform.parent = null;
                    particleSystem.Play();
                }
                //Destroy(gameObject);
            }

        }

        // handle generic event types
        for (int i=0; i < tagList.Count; i++)
        {

            if (other.gameObject.tag == tagList[i])
            {
                triggerEnterEvent?.Invoke();
            }
        }
    }

}
