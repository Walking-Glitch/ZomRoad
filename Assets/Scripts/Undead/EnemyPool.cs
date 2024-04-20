using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> EnemyPrefabs;


    [SerializeField] private int poolSize;
    [SerializeField] private List<GameObject> EnemyList;

    void Start()
    {
        AddEnemiesToPool(poolSize);
    }

    private void AddEnemiesToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject enemyPrefab = EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count)];
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            EnemyList.Add(enemy);
            enemy.transform.parent = transform;
        }
    }

    public GameObject RequestEnemy()
    {
        //for (int i = Random.Range(0, EnemyList.Count - 1); i < EnemyList.Count; i++)
        for (int i = 0; i < EnemyList.Count; i++)
        {
            if (!EnemyList[i].activeSelf)
            {
                //EnemyList[i].SetActive(true);
                EnemyList[i].GetComponent<NavMeshAgent>().enabled = false;
                return EnemyList[i];
            }

        }
        return null;

    }
}
