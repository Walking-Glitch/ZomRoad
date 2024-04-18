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

    public void SpawnCasing(bool twinTurret)
    {
        //if (twinTurret)
        //{
        //    GameObject casing = gameManager.casingPool.RequestCasing();
        //    casing.transform.position = spawnTransformRight.position;
        //    casing.transform.rotation = spawnTransformRight.rotation;

        //    casing.GetComponent<Rigidbody>().isKinematic = false;
        //    casing.GetComponent<Rigidbody>().AddForce(((spawnTransformRight.right * 5f) + (spawnTransformRight.forward * 10f) + (spawnTransformRight.up * 10f)) * ejectForce);
        //    casing.GetComponent<Rigidbody>().AddTorque(casing.transform.position * ejectTorque);

        //    GameObject casingLeft = gameManager.casingPool.RequestCasing();
        //    casingLeft.transform.position = spawnTransformLeft.position;
        //    casingLeft.transform.rotation = spawnTransformLeft.rotation;

        //    casingLeft.GetComponent<Rigidbody>().isKinematic = false;
        //    casingLeft.GetComponent<Rigidbody>().AddForce(((spawnTransformLeft.right * 5f * -1) + (spawnTransformLeft.forward * 10f) + (spawnTransformLeft.up * 10f)) * ejectForce);
        //    casingLeft.GetComponent<Rigidbody>().AddTorque(casingLeft.transform.position * ejectTorque);

        //    StartCoroutine(DisableCasing(casing));
        //    StartCoroutine(DisableCasing(casingLeft));
        //}

        //else
        //{
        //    GameObject casing = gameManager.casingPool.RequestCasing();
        //    casing.transform.position = spawnTransformRight.position;
        //    casing.transform.rotation = spawnTransformRight.rotation;

        //    casing.GetComponent<Rigidbody>().isKinematic = false;
        //    casing.GetComponent<Rigidbody>().AddForce(((spawnTransformRight.right * 5f) + (spawnTransformRight.forward * 10f) + (spawnTransformRight.up * 10f)) * ejectForce);
        //    casing.GetComponent<Rigidbody>().AddTorque(casing.transform.position * ejectTorque);


        //    StartCoroutine(DisableCasing(casing));
        //}


        // Spawn casing for the right turret
        GameObject casingRight = gameManager.casingPool.RequestCasing();
        casingRight.transform.position = spawnTransformRight.position;
        casingRight.transform.rotation = spawnTransformRight.rotation;
        Rigidbody casingRightRigidbody = casingRight.GetComponent<Rigidbody>();

        casingRightRigidbody.isKinematic = false;
        Vector3 rightForce = spawnTransformRight.right * 5f + spawnTransformRight.forward * 10f + spawnTransformRight.up * 10f;
        casingRightRigidbody.AddForce(rightForce * ejectForce);
        casingRightRigidbody.AddTorque(casingRight.transform.position * ejectTorque);

        StartCoroutine(DisableCasing(casingRight));

        if (twinTurret)
        {
            // Spawn casing for the left turret
            GameObject casingLeft = gameManager.casingPool.RequestCasing();
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


    }

    private IEnumerator DisableCasing(GameObject casing)
    {
        yield return new WaitForSeconds(delay);
        casing.SetActive(false);
    }
}
