using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{

    [SerializeField] string nextSceneName;
    private int timesCalled = 0;

    public void OnEnable()
    {
        // hack since OnEnable is called before game object is enabled in the timeline
        timesCalled += 1;

        if (timesCalled == 2)
        {
            if (nextSceneName != "")
            {
                SceneManager.LoadScene(nextSceneName);
            }
        } // end if
        
    }
}
