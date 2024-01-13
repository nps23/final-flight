using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class TrenchSpawnManager : MonoBehaviour
{
    [SerializeField] GameObject trenchPrefab;
    [SerializeField] List<GameObject> obstacleSpawns;
    [SerializeField] GameObject obstaclePrefab; // TODO replace with list after testing
    
    // callbacks
    [SerializeField] UnityEvent onTrenchesIncreased;
    [SerializeField] UnityEvent onTutorialComplete;

    
    // parameters controlling tutorial-only actions
    [SerializeField] bool isTutorial;


    private Queue<GameObject> trenchQueue = new Queue<GameObject>();
    private float spawnDistance = 1628;
    private int totalTrenchesSpawned = 0;

    private void Awake()
    {
        // Calculate the initial spawn position and rotation based on the prefab's forward direction
        Vector3 spawnPositionFirst = transform.position;
        Vector3 spawnPositionSecond = transform.position + (spawnDistance * trenchPrefab.transform.forward);
        
        Quaternion spawnRotation = Quaternion.LookRotation(trenchPrefab.transform.forward, transform.up);

        // Instantiate the first and second trenches and enqueue them
        GameObject firstTrench = Instantiate(trenchPrefab, spawnPositionFirst, spawnRotation);
        GameObject secondTrench = Instantiate(trenchPrefab, spawnPositionSecond, spawnRotation);

        trenchQueue.Enqueue(firstTrench);
        trenchQueue.Enqueue(secondTrench);
    }

    public void SpawnTrench()
    {

        if (totalTrenchesSpawned == 15 && isTutorial)
        {
            onTutorialComplete?.Invoke();
        }

        if (trenchQueue.Count > 0)
        {
            totalTrenchesSpawned += 1;
            if (totalTrenchesSpawned % 2 == 0)
            {
                onTrenchesIncreased?.Invoke();
            }
            
            GameObject currentTrench = trenchQueue.Dequeue();

            Vector3 spawnPosition = currentTrench.transform.position + (spawnDistance * currentTrench.transform.forward);
            Quaternion spawnRotation = Quaternion.LookRotation(currentTrench.transform.forward, transform.up);

            GameObject newTrench = Instantiate(trenchPrefab, spawnPosition, spawnRotation);

            // Enqueue the new trench for future spawns
            trenchQueue.Enqueue(newTrench);
            SpawnTrenchObstacles(newTrench);

            // Destroy the old trench after 1 second
            Destroy(currentTrench, 1f);
        }
    }
    
    // SpawnTrenchObstacle is repobsible for prodecurally spawning obstaces on a newly-instantiaed trench piecce
    private void SpawnTrenchObstacles(GameObject obj)
    {
        
        Transform spawnPoints = obj.transform.Find("SpawnPoints");
        
        if (spawnPoints != null)
        {
            foreach (Transform child in spawnPoints)
            {
                // ensure that it is a spawn point
                if (child.tag != "SpawnPoint")
                {
                    continue;
                }
                bool shouldSpawn = Random.Range(0f, 1f) > 0.5f;
                
                if (shouldSpawn)
                {
                    GameObject obstacle = Instantiate(obstaclePrefab, child.transform.position, Quaternion.identity);

                    // anchor the new obstacle to parent do that it get's destroyed 
                    obstacle.transform.SetParent(obj.transform);
                    obstacle.SetActive(true);
                    
                }

            }
        }
        
    }
}
