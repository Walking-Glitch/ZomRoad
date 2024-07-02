using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.AI.Navigation;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RoadManager : MonoBehaviour
{
    public GameObject currStreet;
    [SerializeField]private GameObject prevStreet;
    [SerializeField] private Transform streetEndPoint;
    private GameManager gameManager;

    [SerializeField] private GameObject storeRoadTrigger;
    [SerializeField] private GameObject storeRoadDeleter;

    [SerializeField] private List<GameObject> prevTriggersGameObjects;
    [SerializeField] private List<GameObject> currTriggersGameObjects;


    void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("ExitStreetTrigger"))
        {
            if (currTriggersGameObjects.Count > 0)
            {
                foreach (var t in currTriggersGameObjects)
                {
                    prevTriggersGameObjects.Add(t);
                }

                currTriggersGameObjects.Clear();
            }
        }

        if (other.CompareTag("EnterStreetTrigger"))
        {
            if (prevTriggersGameObjects.Count > 0)
            {
                foreach (var t in prevTriggersGameObjects)
                {
                    t.SetActive(true);
                }

                prevTriggersGameObjects.Clear();
            }
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("RoadTrigger"))
        {
            storeRoadTrigger = other.gameObject;
            currTriggersGameObjects.Add(storeRoadTrigger);
            other.gameObject.SetActive(false);
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

                prevStreet = currStreet;
                currStreet = newStreet;
            }
        }


        if (other.gameObject.CompareTag("DeleteTrigger"))
        {
            storeRoadDeleter = other.gameObject;
            currTriggersGameObjects.Add(storeRoadDeleter);
            other.gameObject.SetActive(false);
            //Destroy(prevStreet);
            if (prevStreet != null)
            {
                prevStreet.SetActive(false);
                //if (storeRoadTrigger != null)
                //{
                //    storeRoadTrigger.SetActive(true);
                //}
            }
        }
    }
}
