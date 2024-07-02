using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public Transform parentSpawnPoint;

    [SerializeField] private List<Transform> spawnPointsList = new List<Transform>();

    public int enemyCtr = 0;
    public int maxEnemy;
    public float delay;

    private bool isSpawning;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;

        CollectChildObjects(parentSpawnPoint);
    }
    // Update is called once per frame
    void Update()
    {
        if (!isSpawning && enemyCtr < maxEnemy)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    void CollectChildObjects(Transform parentSpawnPoint)
    {
        // Iterate through each child of the parent
        foreach (Transform sp in parentSpawnPoint)
        {
            // Add the child object to the list
            spawnPointsList.Add(sp.transform);

            //// Recursively call this method for each child to also add their children
            //CollectChildObjects(child);
        }
    }
    private IEnumerator SpawnEnemies()
    {
        isSpawning = true;

        Transform selectedSpawnPoint = GetValidSpawnPoint();

        int attempts = 0;

        while (selectedSpawnPoint == null && attempts < 10)
        {
            selectedSpawnPoint =  GetValidSpawnPoint();
            attempts++;
        }

        if (selectedSpawnPoint == null)
        {
            Debug.Log("Failed to find a valid spawn point after multiple attempts.");
            isSpawning = false;
            yield break;
        }

        GameObject tempEnemy = gameManager.enemyPool.RequestEnemy();
        tempEnemy.transform.position = selectedSpawnPoint.position;
        tempEnemy.transform.rotation = selectedSpawnPoint.rotation;
        tempEnemy.GetComponent<NavMeshAgent>().enabled = true;
        tempEnemy.SetActive(true);
        enemyCtr++;
        
        yield return new WaitForSeconds(delay);
        isSpawning = false;

    }

    Transform GetValidSpawnPoint()
    {

        int i = Random.Range(0, spawnPointsList.Count-1);
        Vector3 spawnPos = spawnPointsList[i].position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(spawnPos, out hit, 1.0f, NavMesh.AllAreas))
        {
            return spawnPointsList[i];
        }
        

        // If no valid spawn point is found, return null
        return null;
    }

    public void DecreaseEnemyCtr()
    {
        enemyCtr--;
    }
}
