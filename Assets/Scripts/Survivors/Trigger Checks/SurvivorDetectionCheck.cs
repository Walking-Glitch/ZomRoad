using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivorDetectionCheck : MonoBehaviour
{
    [SerializeField] private bool enemyInRange;
    private Survivor _survivor;
    public float detectionRange;
    public LayerMask enemyLayer;


    private void Awake()
    {
        _survivor = GetComponentInParent<Survivor>();
    }

    private void Update()
    {
        Debug.Log(_survivor.enemyList.Count);
        DetectEnemies();
        AddEnemies();
        RemoveEnemies();
    }

    public void DetectEnemies()
    {
        enemyInRange = Physics.CheckSphere(gameObject.transform.position, detectionRange, enemyLayer);
        if (enemyInRange)
        {
            _survivor.SetIsInDetectionArea(true);
        }
        else
        {
            _survivor.SetIsInDetectionArea(false);
        }
    }
    public void AddEnemies()
    {
        if (enemyInRange)
        {
            Collider[] numberEnemiesInRange =
                Physics.OverlapSphere(transform.position, detectionRange, enemyLayer);
            foreach (Collider enemy in numberEnemiesInRange)
            {
                Enemy enemy2 = enemy.gameObject.GetComponent<Enemy>();

                if (enemy2 != null && !_survivor.enemyList.Contains(enemy2))
                {
                    _survivor.enemyList.Add(enemy2);
                }
            }
        }

        SetCurrentTarget();
    }

    public void RemoveEnemies()
    {
        if (_survivor.enemyList.Count > 0 && _survivor.enemyList[0] == null)
        {
            _survivor.enemyList.RemoveAt(0);

            SetCurrentTarget();
        }
    }

    public void SetCurrentTarget()
    {
        if (_survivor.enemyList.Count > 0 && _survivor.enemyList[0] != null)
        {
            _survivor.SetCurrentTarget(_survivor.enemyList[0].gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!enemyInRange)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
        else
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }

    }
}
