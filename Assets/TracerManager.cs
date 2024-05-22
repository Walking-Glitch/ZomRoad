using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracerManager : MonoBehaviour
{
    public Transform spawnTransformRight;
     
    private GameManager gameManager;
    private float delay = 0.5f;
  
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void SpawnBulletTracer(Transform x)
    {
        Debug.Log(x);
        // Spawn casing for the right turret
        GameObject tracer = gameManager.bulletTracePool.RequestBulletTracer(x);
        tracer.transform.position = spawnTransformRight.position;
        tracer.transform.rotation = spawnTransformRight.rotation;


    

        StartCoroutine(DisableTracer(tracer));



    }

    private IEnumerator DisableTracer(GameObject tracer)
    {
        yield return new WaitForSeconds(delay);
        tracer.GetComponent<TracerBehavior>().DisableLogic();
    }

}
