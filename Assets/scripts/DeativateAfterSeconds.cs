using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


// DeactivateAfterSeconds will start a coroutine which will deactivate the specified game object
public class DeativateAfterSeconds : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float timeSeconds;

    public void DeactivateAfterSeconds()
    {
        if (gameObject != null)
        {
            StartCoroutine(RunDeactivate());
        }
    }

    private IEnumerator RunDeactivate()
    {
        yield return new WaitForSeconds(timeSeconds);
        gameObject.SetActive(false);
    }


}
