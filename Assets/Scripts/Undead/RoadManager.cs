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

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("RoadTrigger"))
        {
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
            //Destroy(prevStreet);
            if (prevStreet != null)
            {
                prevStreet.SetActive(false);
            }
            
        }
    }


}
