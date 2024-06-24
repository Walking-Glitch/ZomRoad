using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerManager : MonoBehaviour
{
    public Transform spawnTransformRight;
    public Transform spawnTransformLeft;

    private GameManager gameManager;
    private float delay = 0.5f;
  
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void SpawnShellTracer(Transform x)
    {
        GameObject tracer = gameManager.bulletTracePool.RequestBulletTracer(x);
        tracer.transform.position = spawnTransformRight.position;
        tracer.transform.rotation = spawnTransformRight.rotation;

        StartCoroutine(DisableTracer(tracer));

    }

    public void SpawnBulletTracer(Transform x)
    {
        GameObject tracerRight = gameManager.bulletTracePool.RequestBulletTracer(x);
        tracerRight.transform.position = spawnTransformRight.position;
        tracerRight.transform.rotation = spawnTransformRight.rotation;

        StartCoroutine(DisableTracer(tracerRight));

        GameObject tracerLeft = gameManager.bulletTracePool.RequestBulletTracer(x);
        tracerLeft.transform.position = spawnTransformLeft.position;
        tracerLeft.transform.rotation = spawnTransformLeft.rotation;

        StartCoroutine(DisableTracer(tracerLeft));

    }

    public void SpawnEnergyTracer(Transform x)
    {
        GameObject tracerRight = gameManager.bulletTracePool.RequestEnergyTracer(x);
        tracerRight.transform.position = spawnTransformRight.position;
        tracerRight.transform.rotation = spawnTransformRight.rotation;

        StartCoroutine(DisableTracer(tracerRight));

        GameObject tracerLeft = gameManager.bulletTracePool.RequestEnergyTracer(x);
        tracerLeft.transform.position = spawnTransformLeft.position;
        tracerLeft.transform.rotation = spawnTransformLeft.rotation;

        StartCoroutine(DisableTracer(tracerLeft));

    }


    private IEnumerator DisableTracer(GameObject tracer)
    {
        yield return new WaitForSeconds(delay);
        tracer.GetComponent<TracerBehavior>().DisableLogic();
    }

}
