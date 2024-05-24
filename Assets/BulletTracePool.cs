using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTracePool : MonoBehaviour
{
    [SerializeField] private GameObject bulletTracePrefab;
    [SerializeField] private GameObject energyTracePrefab;
    [SerializeField] private int poolSize;
    [SerializeField] private List<GameObject> bulletTracerList;
    [SerializeField] private List<GameObject> EnergyTracerList;
    void Start()
    {
        AddTracersToPool(poolSize);
        AddEnergyTracersToPool(poolSize);
    }

    private void AddTracersToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject tracer = Instantiate(bulletTracePrefab);
            tracer.SetActive(false);
            bulletTracerList.Add(tracer);
            tracer.transform.parent = transform;
        }

    }

    private void AddEnergyTracersToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject tracer = Instantiate(energyTracePrefab);
            tracer.SetActive(false);
            EnergyTracerList.Add(tracer);
            tracer.transform.parent = transform;
        }

    }

    public GameObject RequestBulletTracer( Transform x)
    {
        for (int i = 0; i < bulletTracerList.Count; i++)
        {
            if (!bulletTracerList[i].activeSelf)
            {
                bulletTracerList[i].gameObject.GetComponent<TracerBehavior>().EnableLogic(x);
                return bulletTracerList[i];
            }
        }

        return null;
    }

    public GameObject RequestEnergyTracer(Transform x)
    {
        for (int i = 0; i < EnergyTracerList.Count; i++)
        {
            if (!EnergyTracerList[i].activeSelf)
            {
                EnergyTracerList[i].gameObject.GetComponent<TracerBehavior>().EnableLogic(x);
                return EnergyTracerList[i];
            }
        }

        return null;
    }
}
