using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class LinearSqeuenceQueue : MonoBehaviour
{
    [SerializeField] private List<UnityEvent> events;

    private Queue<UnityEvent> eventQueue = new Queue<UnityEvent>();
    private void Awake()
    {        
        // pop each event from the list onto the queue
        foreach(var evt in events)
        {
            eventQueue.Enqueue(evt);
        }

        // immediatly dequeue next event
        DequeueNextEvent();
    }

    public void DequeueNextEvent()
    {
        // ensure that the queue isn't empty
        if (eventQueue.Count > 0)
        {
            
            // this is dequeing despite there being a wait for time in the tutorial
            eventQueue.Dequeue()?.Invoke();
        }
    }

}
