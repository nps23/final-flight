using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirlceManager : MonoBehaviour
{

    // Parameters controlling how far away from the spaceship we should spawn the new circle
    // The circle is resposible for dequeing the next event in the queue
    [SerializeField] float distanceFromPlayer;
    [SerializeField] GameObject player; // can potentially get this from tag instead?
    [SerializeField] GameObject preFabToSpawn;


    public void SpawnNewCircle()
    {
        
        // TODO Implement randomized offset in player-space y or z to ensure the player has to actually maneurver to proceed through ring
        Vector3 spawnPosition = player.transform.position + player.transform.forward * distanceFromPlayer;
        
        // Ensure that the newly-spawned ring is always looking at the player
        Quaternion spawnRotation = Quaternion.LookRotation(player.transform.position - spawnPosition);

        GameObject newCircle = Instantiate(preFabToSpawn, spawnPosition, spawnRotation);

        newCircle.SetActive(true);

    }

}
