using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private GameObject currStreet;
    [SerializeField] private GameObject prevStreet;
    [SerializeField] private Transform streetEndPoint;
    private GameManager gameManager;

    private GameObject storeRoadTrigger;
    public Transform reAllocationPoint;

    private bool hasCreatedStreet = false;


    void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void SetRespawnPoint(Collider other)
    {
        Transform childTransform = other.transform.Find("SpawnPoint");
        if (childTransform != null)
        {
            reAllocationPoint = childTransform;
        }
        else
        {
            Debug.LogError("Child object with the specified name not found.");
        }
    }

    private void CallTempStreets(Collider other)
    {
        

        if (!hasCreatedStreet && storeRoadTrigger != other.gameObject.transform.parent.gameObject)
        {
            // get reference to current street 
            storeRoadTrigger = other.gameObject.transform.parent.gameObject;

            prevStreet = currStreet;
            
            currStreet = storeRoadTrigger;

            streetEndPoint = currStreet.transform.Find("CreatePoint");

            if (streetEndPoint != null)
            {
                GameObject newStreet = gameManager.streetPool.RequestStreet();
                newStreet.transform.position = streetEndPoint.position + new Vector3(0, 0, 29.5f);


                //newStreet.GetComponentInChildren<OffMeshLink>().gameObject.SetActive(true);
                OffMeshLink offMeshLink = newStreet.GetComponentInChildren<OffMeshLink>();
                // this was necessary for off mesh links to display on run time
                offMeshLink.gameObject.SetActive(false);
                offMeshLink.gameObject.SetActive(true);

                // prevStreet = currStreet;
                // currStreet = newStreet;
            }

            if (prevStreet != null && prevStreet != currStreet)
            {
                prevStreet.SetActive(false);

            }

            hasCreatedStreet = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("EnterStreetTrigger"))
        {
            SetRespawnPoint(other);
        }

        if (other.gameObject.CompareTag("RoadTrigger"))
        {
            CallTempStreets(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RoadTrigger"))
        {
            hasCreatedStreet = false;
        }
    }

}
