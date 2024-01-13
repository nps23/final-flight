using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHighlighter : MonoBehaviour
{
    [SerializeField] private float highlightingDistance = 50f;
    [SerializeField] private Material highlightMaterial;

    private void Update()
    {
        // Cast a ray from the spaceship in the forward direction
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, highlightingDistance))
        {
            // Check if the ray hits a valid object
            GameObject hitObject = hit.collider.gameObject;

            // Apply the highlighting effect to the object
            ApplyHighlighting(hitObject);

            // TODO callback for custom handling
        }
    }

    private void ApplyHighlighting(GameObject obj)
    {
        // Apply the highlighting effect to the object
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = highlightMaterial;
        }
    }
}

