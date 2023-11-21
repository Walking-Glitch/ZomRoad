using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionCheck : MonoBehaviour
{
   // public GameObject PlayerTarget { get; set; }
    private Enemy _enemy;

    private void Awake()
    {
       // PlayerTarget = GameObject.FindGameObjectWithTag("Survivor");

        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Survivor"))
        {
            _enemy.SetIsInDetectionArea(true);
            _enemy.SetCurrentTarget(other.gameObject);

            Debug.Log("SURVIVOR DETECTED");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Survivor"))
        {
            _enemy.SetIsInDetectionArea(false);
            _enemy.SetCurrentTarget(null);
        }
    }
}
