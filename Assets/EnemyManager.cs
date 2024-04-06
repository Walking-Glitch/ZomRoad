using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemy;
    public Transform[] spawnPoint;

    public int enemyCtr = 0;
    public int maxEnemy;
    public float delay;

    private bool isSpawning;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isSpawning && enemyCtr < maxEnemy)
        {
            StartCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        isSpawning = true;

        Transform selectedSpawnPoint = GetValidSpawnPoint();
        if (selectedSpawnPoint != null)
        {
            //Instantiate(enemy[Random.Range(0, enemy.Length)], spawnPoint[Random.Range(0, spawnPoint.Length)].position, spawnPoint[Random.Range(0, spawnPoint.Length)].rotation);
            //enemyCtr++;
            GameObject tempEnemy = gameManager.enemyPool.RequestEnemy();
            tempEnemy.transform.position = spawnPoint[Random.Range(0, spawnPoint.Length)].position;
            tempEnemy.transform.rotation = spawnPoint[Random.Range(0, spawnPoint.Length)].rotation;
            enemyCtr++;
        }
        yield return new WaitForSeconds(delay);
        isSpawning = false;

    }

    Transform GetValidSpawnPoint()
    {

        foreach (Transform spawnP in spawnPoint)
        {
            Vector3 spawnPos = spawnP.position;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(spawnPos, out hit, 1.0f, NavMesh.AllAreas))
            {
                return spawnP;
            }
        }

        // If no valid spawn point is found, return null
        return null;
    }

    public void DecreaseEnemyCtr()
    {
        enemyCtr--;
    }
}
