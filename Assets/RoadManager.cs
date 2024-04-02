using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject currStreet;
    private GameObject prevStreet;
    [SerializeField] private GameObject[] streets;
    [SerializeField] private Transform streetEndPoint;

    void Start()
    {
        
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
                GameObject tempStreetPrefab = Instantiate(streets[Random.Range(0,streets.Length)], streetEndPoint.position + new Vector3(0, 0, 29.5f), Quaternion.identity);
                prevStreet = currStreet;
                currStreet = tempStreetPrefab;
                
            }
           

        }

        if (other.gameObject.CompareTag("DeleteTrigger"))
        {
            Destroy(prevStreet);
        }
    }


}
