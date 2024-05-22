using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTracePool : MonoBehaviour
{
    [SerializeField] private GameObject bulletTracePrefab;
    [SerializeField] private int poolSize;
    [SerializeField] private List<GameObject> tracerList;
    void Start()
    {
        AddTracersToPool(poolSize);
    }

    private void AddTracersToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject tracer = Instantiate(bulletTracePrefab);
            tracer.SetActive(false);
            tracerList.Add(tracer);
            tracer.transform.parent = transform;
        }
    }

    public GameObject RequestBulletTracer( Transform x)
    {
        for (int i = 0; i < tracerList.Count; i++)
        {
            if (!tracerList[i].activeSelf)
            {
                tracerList[i].gameObject.GetComponent<TracerBehavior>().EnableLogic(x);
                return tracerList[i];
            }
        }

        return null;
    }
}
