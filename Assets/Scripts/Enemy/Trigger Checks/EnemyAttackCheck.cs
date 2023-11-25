using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCheck : MonoBehaviour
{
    //public GameObject PlayerTarget { get; set; }
    private Enemy _enemy;

    
    private void Awake()
    {
       // PlayerTarget = GameObject.FindGameObjectWithTag("Survivor");

        _enemy = GetComponentInParent<Enemy>();
    }

    void Update()
    {
        if (_enemy.currentTarget == null)
        {
            _enemy.SetIsInAttackArea(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Survivor"))
        {
            _enemy.SetIsInAttackArea(true);
            _enemy.SetCurrentTarget(other.gameObject);
            //Debug.Log("Survivor in ATTACK area coll");
        }
         
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Survivor"))
        {
            _enemy.SetIsInAttackArea(false);
            Debug.Log("Survivor LEFT ATTACK area coll");
            //_enemy.SetCurrentTarget(null);
        }
    }


}
