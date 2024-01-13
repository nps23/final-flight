using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO look into prefabbing this instead of messing with activation states
public class UITextControls : MonoBehaviour
{
    [SerializeField] float timeToDisplay;

    // TODO emit event instead of explicilty passing and controlling the ship controller
    [SerializeField] Controller player;
    [SerializeField] LinearSqeuenceQueue sequenceQueue;
        
    
    public void PauseGameAndDisplay()
    {
        // start up the game object
        player.controllable = false;
        gameObject.SetActive(true);
        
        StartCoroutine(RunPause());
    }

    private IEnumerator RunPause()
    {
        player.controllable = false;
        TextMesh text = gameObject.GetComponent<TextMesh>();

        // ensure that the component exists, if not do nothing
        //if (text == null)
        //{
        //    Debug.Log("its null");
        //    return;
        //}


        yield return new WaitForSeconds(timeToDisplay);

        player.controllable = true;

        // let the player play for 10 seconds, then activate next event
        yield return new WaitForSeconds(10f);
        
        sequenceQueue.DequeueNextEvent();
        gameObject.SetActive(false);
    }

}
