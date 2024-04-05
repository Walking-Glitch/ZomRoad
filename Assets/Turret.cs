using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float detectionRadius;
    public LayerMask enemyLayer;
    public Animator anim;
    public Transform transformT;

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

        Collider[] colliders = Physics.OverlapSphere(transformT.position, detectionRadius, enemyLayer);

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

            Vector3 direction = currentEnemy.position - transformT.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transformT.rotation = rotation;

            anim.SetTrigger("Shoot");


        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    public void FireTurret()
    {
       
        if (currentEnemy != null)
        {
            currentEnemy.gameObject.GetComponent<Zombie>().RagdollModeOn();
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere in the editor to visualize the detection radius
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transformT.position, detectionRadius);
    }
}
