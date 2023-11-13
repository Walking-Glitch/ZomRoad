using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectionCheck : MonoBehaviour
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
            _enemy.SetIsInDetectionArea(true);

            Debug.Log("PLAYER DETECTED");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _enemy.SetIsInDetectionArea(false);
    }
}
