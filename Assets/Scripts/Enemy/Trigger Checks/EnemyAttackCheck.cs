using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCheck : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }
    private Enemy _enemy;

    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");

        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayerTarget)
        {
            _enemy.SetIsInAttackArea(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _enemy.SetIsInAttackArea(false);
    }
}
