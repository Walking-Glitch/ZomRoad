using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ConsumablesManager : MonoBehaviour
{
    public Transform parentSpawnPoint;

    [SerializeField] private List<Transform> spawnPointsList = new List<Transform>();

    public int medkitCtr = 0;
    public int maxMedkit;

    public int slugCtr = 0;
    public int maxSlug;

    public int bulletCtr = 0;
    public int maxBullet;

    public int energyCellCtr = 0;
    public int maxEnergyCell;

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
        if (!isSpawning && medkitCtr < maxMedkit)
        {
            StartCoroutine(SpawnMedkits());
        }

        if (!isSpawning && slugCtr < maxSlug)
        {
            StartCoroutine(SpawnSlugs());
        }

        if (!isSpawning && bulletCtr < maxBullet)
        {
            StartCoroutine(SpawnBullets());
        }

        if (!isSpawning && energyCellCtr < maxEnergyCell)
        {
            StartCoroutine(SpawnEnergyCells());
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
    private IEnumerator SpawnMedkits()
    {
        isSpawning = true;

        Transform selectedSpawnPoint = GetValidSpawnPoint();
        if (selectedSpawnPoint != null)
        {
            //Instantiate(enemy[Random.Range(0, enemy.Length)], spawnPoint[Random.Range(0, spawnPoint.Length)].position, spawnPoint[Random.Range(0, spawnPoint.Length)].rotation);
            //enemyCtr++;
            GameObject tempMedkit = gameManager.ConsumablesPool.RequestMedkit();
            tempMedkit.transform.position = selectedSpawnPoint.position;
            //tempMedkit.transform.rotation = tempMedkit.transform.rotation;

            tempMedkit.SetActive(true);
            medkitCtr++;
        }
        yield return new WaitForSeconds(delay);
        isSpawning = false;

    }

    private IEnumerator SpawnSlugs()
    {
        isSpawning = true;

        Transform selectedSpawnPoint = GetValidSpawnPoint();
        if (selectedSpawnPoint != null)
        {
            //Instantiate(enemy[Random.Range(0, enemy.Length)], spawnPoint[Random.Range(0, spawnPoint.Length)].position, spawnPoint[Random.Range(0, spawnPoint.Length)].rotation);
            //enemyCtr++;
            GameObject tempSlug = gameManager.ConsumablesPool.RequestSlug();
            tempSlug.transform.position = selectedSpawnPoint.position;
            tempSlug.transform.rotation = selectedSpawnPoint.rotation;

            tempSlug.SetActive(true);
            slugCtr++;
        }
        yield return new WaitForSeconds(delay);
        isSpawning = false;

    }

    private IEnumerator SpawnBullets()
    {
        isSpawning = true;

        Transform selectedSpawnPoint = GetValidSpawnPoint();
        if (selectedSpawnPoint != null)
        {
            //Instantiate(enemy[Random.Range(0, enemy.Length)], spawnPoint[Random.Range(0, spawnPoint.Length)].position, spawnPoint[Random.Range(0, spawnPoint.Length)].rotation);
            //enemyCtr++;
            GameObject tempBullet = gameManager.ConsumablesPool.RequestBullet();
            tempBullet.transform.position = selectedSpawnPoint.position;
            tempBullet.transform.rotation = selectedSpawnPoint.rotation;

            tempBullet.SetActive(true);
            bulletCtr++;
        }
        yield return new WaitForSeconds(delay);
        isSpawning = false;
    }

    private IEnumerator SpawnEnergyCells()
    {
        isSpawning = true;

        Transform selectedSpawnPoint = GetValidSpawnPoint();
        if (selectedSpawnPoint != null)
        {
            //Instantiate(enemy[Random.Range(0, enemy.Length)], spawnPoint[Random.Range(0, spawnPoint.Length)].position, spawnPoint[Random.Range(0, spawnPoint.Length)].rotation);
            //enemyCtr++;
            GameObject tempEnergyCell = gameManager.ConsumablesPool.RequestEnergyCell();
            tempEnergyCell.transform.position = selectedSpawnPoint.position;
            tempEnergyCell.transform.rotation = selectedSpawnPoint.rotation;

            tempEnergyCell.SetActive(true);
            energyCellCtr++;
        }
        yield return new WaitForSeconds(delay);
        isSpawning = false;
    }


    Transform GetValidSpawnPoint()
    {

        int i = Random.Range(0, spawnPointsList.Count - 1);
        Vector3 spawnPos = spawnPointsList[i].position;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(spawnPos, out hit, 1.0f, NavMesh.AllAreas))
        {
            return spawnPointsList[i];
        }


        // If no valid spawn point is found, return null
        return null;
    }

    public void DecreaseMedkitCtr()
    {
        medkitCtr--;
    }

    public void DecreaseSlugCtr()
    {
        slugCtr--;
    }

    public void DecreaseBulletCtr()
    {
        bulletCtr--;
    }

    public void DecreaseEnergyCellCtr()
    {
        energyCellCtr--;
    }
}
