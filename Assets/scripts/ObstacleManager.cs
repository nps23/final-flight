using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] int ShotsToDestroy;
    [SerializeField] UnityEvent onDestroyed;
    

    private int numShotsTaken = 0;

    
    public void ApplyDamage()
    {
        numShotsTaken += 1;

        
        if (numShotsTaken >= ShotsToDestroy)
        {
            // TODO flesh this out by adding animations etc
            onDestroyed?.Invoke();
            Destroy(gameObject);

        }

    }
    
}
