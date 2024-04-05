using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float detectionRadius;
    public LayerMask enemyLayer;

    [SerializeField] private List<Transform> enemies = new List<Transform>();
    [SerializeField] private Transform currentEnemy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindEnemies();
        AimAtTarget();
    }

    private void FindEnemies()
    {
        enemies.Clear();

        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        foreach (Collider col in colliders)
        {
            enemies.Add(col.transform);
        }
    }

    private void AimAtTarget()
    {
        if (enemies.Count > 0)
        {
            if (currentEnemy == null || !enemies.Contains(currentEnemy))
            {
                currentEnemy = enemies[0];
            }

            Vector3 direction = currentEnemy.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere in the editor to visualize the detection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
