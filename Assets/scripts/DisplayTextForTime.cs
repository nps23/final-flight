using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTextForTime : MonoBehaviour
{
    private TextMeshProUGUI displayText;
    [SerializeField] private float displayDuration = 3f; // Adjust the duration as needed

    private Coroutine displayCoroutine;

    private void Awake()
    {
        // Get the TextMeshPro - Text component from the GameObject
        displayText = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayText(string text)
    {        
        displayText.text = text;

        if (displayCoroutine != null)
            StopCoroutine(displayCoroutine);

        displayCoroutine = StartCoroutine(DisplayRoutine());
    }

    private IEnumerator DisplayRoutine()
    {
        displayText.enabled = true;

        yield return new WaitForSeconds(displayDuration);

        displayText.enabled = false;
    }
}
