using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TracerBehavior : MonoBehaviour
{
 
    
    private GameManager gameManager;
    public GameObject Target;
    [SerializeField] float interpolationRatio;
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    
    void Update()
    {
        if (Target != null)
        {
            Vector3 interpolatedPosition = Vector3.Lerp(transform.position, Target.transform.position, interpolationRatio);

            transform.position = interpolatedPosition;
        }
    }

    public void AssignTarget(Transform x)
    {
        Target.transform.SetParent(null);
        Target.transform.position = x.position + new Vector3(0, 1.2f, 0); ;
    }
    public void EnableLogic(Transform x)
    {
        gameObject.SetActive(true);
        AssignTarget(x);
    }

    public void DisableLogic()
    {
        gameObject.SetActive(false);
        Target.transform.parent = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            DisableLogic();
        }
    }


}
