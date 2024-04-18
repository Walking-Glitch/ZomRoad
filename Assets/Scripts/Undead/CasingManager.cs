using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CasingManager : MonoBehaviour
{
    public Transform spawnTransformRight;
    public Transform spawnTransformLeft;
    private GameManager gameManager;
    private float delay = 1f;
    public float ejectForce;
    public float ejectTorque;
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void SpawnBulletCasing ()
    { 
        // Spawn casing for the right turret
        GameObject casingRight = gameManager.BulletCasingPool.RequestCasing();
        casingRight.transform.position = spawnTransformRight.position;
        casingRight.transform.rotation = spawnTransformRight.rotation;
        Rigidbody casingRightRigidbody = casingRight.GetComponent<Rigidbody>();

        casingRightRigidbody.isKinematic = false;
        Vector3 rightForce = spawnTransformRight.right * 5f + spawnTransformRight.forward * 10f + spawnTransformRight.up * 10f;
        casingRightRigidbody.AddForce(rightForce * ejectForce);
        casingRightRigidbody.AddTorque(casingRight.transform.position * ejectTorque);

        StartCoroutine(DisableCasing(casingRight));

        // Spawn casing for the left turret
        GameObject casingLeft = gameManager.BulletCasingPool.RequestCasing();
        casingLeft.transform.position = spawnTransformLeft.position;
        casingLeft.transform.rotation = spawnTransformLeft.rotation;
        Rigidbody casingLeftRigidbody = casingLeft.GetComponent<Rigidbody>();

        casingLeftRigidbody.isKinematic = false;
        // Adjust force vector for diagonal left direction
        Vector3 leftForce = spawnTransformLeft.right * -5f + spawnTransformLeft.forward * 10f + spawnTransformLeft.up * 10f;
        casingLeftRigidbody.AddForce(leftForce * ejectForce);
        casingLeftRigidbody.AddTorque(casingLeft.transform.position * ejectTorque);
        
        StartCoroutine(DisableCasing(casingLeft));
        


    }

    public void SpawnShellCasing()
    {
        // Spawn casing for the right turret
        GameObject casingRight = gameManager.ShellCasingPool.RequestCasing();
        casingRight.transform.position = spawnTransformRight.position;
        casingRight.transform.rotation = spawnTransformRight.rotation;
        Rigidbody casingRightRigidbody = casingRight.GetComponent<Rigidbody>();

        casingRightRigidbody.isKinematic = false;
        Vector3 rightForce = spawnTransformRight.right * 5f + spawnTransformRight.forward * 10f + spawnTransformRight.up * 10f;
        casingRightRigidbody.AddForce(rightForce * ejectForce);
        casingRightRigidbody.AddTorque(casingRight.transform.position * ejectTorque);

        StartCoroutine(DisableCasing(casingRight));
        


    }

    private IEnumerator DisableCasing(GameObject casing)
    {
        yield return new WaitForSeconds(delay);
        casing.SetActive(false);
    }
}
