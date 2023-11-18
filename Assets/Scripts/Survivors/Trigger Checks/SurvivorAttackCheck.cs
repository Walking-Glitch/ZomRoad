using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorAttackCheck : MonoBehaviour
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
            _survivor.SetIsInAttackArea(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _survivor.SetIsInAttackArea(false);
    }
}
