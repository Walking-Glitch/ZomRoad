using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CasingManager : MonoBehaviour
{
    public Transform spawnTransform;
    private GameManager gameManager;
    private bool isSpawning;
    private float delay = 2f;
    public float ejectForce;
    public float ejectTorque;
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void SpawnCasing()
    {
        GameObject casing =  gameManager.casingPool.RequestCasing();
        casing.transform.position = spawnTransform.position;
        casing.transform.rotation = spawnTransform.rotation;

        casing.GetComponent<Rigidbody>().isKinematic = false;
        casing.GetComponent<Rigidbody>().AddForce(((spawnTransform.right * 5f) + (spawnTransform.forward * 10f )+ (spawnTransform.up * 10f)) * ejectForce);
        casing.GetComponent<Rigidbody>().AddTorque(casing.transform.position * ejectTorque);

        StartCoroutine(DisableCasing(casing));
    }

    private IEnumerator DisableCasing(GameObject casing)
    {
        yield return new WaitForSeconds(delay);
        casing.SetActive(false);
    }
}
