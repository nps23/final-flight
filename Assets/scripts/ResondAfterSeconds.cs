using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResondAfterSeconds : MonoBehaviour
{
    [SerializeField] UnityEvent onSecondsComplete;
    [SerializeField] float timeSeconds;
    public void HandleAfterSeconds()
    {
        StartCoroutine(RunHandle());
    }

    public IEnumerator RunHandle()
    {
        yield return new WaitForSeconds(timeSeconds);
        onSecondsComplete?.Invoke();
    }
}
