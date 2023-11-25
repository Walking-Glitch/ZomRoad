using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorAttackCheck : MonoBehaviour
{
    //public GameObject EnemyTarget { get; set; }
    private Survivor _survivor;

    private void Awake()
    {
       // EnemyTarget = GameObject.FindGameObjectWithTag("Enemy");

        _survivor = GetComponentInParent<Survivor>();
    }

    void Update()
    {
        if (_survivor.currentTarget == null)
        {
            _survivor.SetIsInAttackArea(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _survivor.SetIsInAttackArea(true);
            _survivor.SetCurrentTarget(other.gameObject);
            //Debug.Log("Survivor in ATTACK area coll");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _survivor.SetIsInAttackArea(false);
            Debug.Log("Enemy LEFT ATTACK area coll");
            //_enemy.SetCurrentTarget(null);
        }
    }
}
