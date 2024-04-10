using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RoadManager : MonoBehaviour
{
    public GameObject currStreet;
    private GameObject prevStreet;
    //[SerializeField] private GameObject[] streets;
    [SerializeField] private Transform streetEndPoint;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.CompareTag("RoadTrigger"))
        {
            streetEndPoint = currStreet.transform.Find("CreatePoint");

            if (streetEndPoint != null)
            {
                GameObject tempStreet = gameManager.streetPool.RequestStreet();
                tempStreet.transform.position = streetEndPoint.position + new Vector3(0, 0, 29.5f);
                //tempStreet.GetComponentInChildren<OffMeshLink>().gameObject.SetActive(true);
                OffMeshLink offMeshLink = tempStreet.GetComponentInChildren<OffMeshLink>();
               // this was necessary for off mesh links to display on run time
                    offMeshLink.gameObject.SetActive(false);
                    offMeshLink.gameObject.SetActive(true);


                prevStreet = currStreet;
                currStreet = tempStreet;

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
