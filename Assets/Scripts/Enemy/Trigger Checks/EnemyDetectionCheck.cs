using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class EnemyDetectionCheck : MonoBehaviour
{
    // public GameObject PlayerTarget { get; set; }

    [SerializeField] private bool survivorInRange;
    private Enemy _enemy;
    public float detectionRange;
    public LayerMask survivorLayer;
    
   
    private void Awake()
    {
       // PlayerTarget = GameObject.FindGameObjectWithTag("Survivor");

        _enemy = GetComponentInParent<Enemy>();
    }

    private void Update()
    {
        //Debug.Log(_enemy.survivorList.Count);
        DetectSurvivors();
        AddSurvivors();
        RemoveSurvivors();
    }

    public void DetectSurvivors()
    {
        survivorInRange = Physics.CheckSphere(gameObject.transform.position, detectionRange, survivorLayer);
        if (survivorInRange)
        {
            _enemy.SetIsInDetectionArea(true);
        }
        else
        {
            _enemy.SetIsInDetectionArea(false);
        }
    }
    public void AddSurvivors()
    {
        if (survivorInRange)
        {
            Collider[] numberSurvivorsInRange =
                Physics.OverlapSphere(transform.position, detectionRange, survivorLayer);
            foreach (Collider survivor in numberSurvivorsInRange)
            {
                Survivor survivor2 = survivor.gameObject.GetComponent<Survivor>();
                
                if (survivor2 != null && !_enemy.survivorList.Contains(survivor2))
                {
                        _enemy.survivorList.Add(survivor2);
                }
            }
        }

        SetCurrentTarget();
    }

    public void RemoveSurvivors()
    {
        if (_enemy.survivorList.Count > 0 && _enemy.survivorList[0] == null)
        {
            _enemy.survivorList.RemoveAt(0);
            
            SetCurrentTarget();
        }
    }

    public void SetCurrentTarget()
    {
        if (_enemy.survivorList.Count > 0 && _enemy.survivorList[0] != null)
        {
            _enemy.SetCurrentTarget(_enemy.survivorList[0].gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!survivorInRange)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Survivor"))
    //    {
             
    //        _enemy.SetIsInDetectionArea(true);
    //        _enemy.SetCurrentTarget(other.gameObject);

    //        Debug.Log("SURVIVOR DETECTED");
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Survivor"))
    //    {
    //        _enemy.SetIsInDetectionArea(false);
    //        _enemy.SetCurrentTarget(null);
    //    }
    //}
}
