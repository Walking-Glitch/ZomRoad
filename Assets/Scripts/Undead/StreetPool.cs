using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class StreetPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> streetPrefabs;


    [SerializeField] private int poolSize;
    [SerializeField] private List<GameObject> streetList;

    [SerializeField] private Transform createTransform;

    void Start()
    {
        AddStreetsToPool(poolSize);
    }

    private void AddStreetsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject streetPrefab = streetPrefabs[Random.Range(0, streetPrefabs.Count)];
            GameObject street = Instantiate(streetPrefab);
            street.SetActive(false);
            streetList.Add(street);
            street.transform.parent = transform;
        }
    }

    public GameObject RequestStreet()
    {
        Debug.Log("street has been requested");
        for (int i = Random.Range(0, streetList.Count - 1); i < streetList.Count; i++)
        {
            if (!streetList[i].activeSelf)
            {
                streetList[i].SetActive(true);
                return streetList[i];
            }

        }
        return null;

    }

    
}
