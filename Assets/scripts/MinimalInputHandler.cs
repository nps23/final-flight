using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// MinimalInputHandler will raise unity events for the following keycodes pressed:
// Escape
public class MinimalInputHandler : MonoBehaviour
{
    [SerializeField] UnityEvent onEscapePressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            onEscapePressed?.Invoke();
        }
    }

}
