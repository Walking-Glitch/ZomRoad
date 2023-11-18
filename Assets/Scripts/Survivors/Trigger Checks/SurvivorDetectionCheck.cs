using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorDetectionCheck : MonoBehaviour
{
    public GameObject EnemyTarget { get; set; }
    private Survivor _survivor;

    private void Awake()
    {
        EnemyTarget = GameObject.FindGameObjectWithTag("Enemy");

        _survivor = GetComponentInParent<Survivor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == EnemyTarget)
        {
            _survivor.SetIsInDetectionArea(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _survivor.SetIsInDetectionArea(false);
    }
}
