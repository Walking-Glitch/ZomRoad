using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float detectionRadius;
    public LayerMask enemyLayer;
    public Animator anim;
    public Transform transformT;
    public Transform carTransform;
    public float transitionDuration;
    public float resetDuration;
    private AudioSource audioSource;

    [SerializeField] private List<Transform> enemies = new List<Transform>();
    [SerializeField] private Transform currentEnemy;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            //transformT.rotation = rotation;

            // Calculate the interpolation factor based on the transition progress
            float t = Time.deltaTime / transitionDuration;

            // Interpolate between the current rotation and the target rotation using Quaternion.Lerp
            Quaternion newRotation = Quaternion.Lerp(transformT.rotation, rotation, t);

            // Apply the new rotation to the turret's transform
            transformT.rotation = newRotation;

            if (Quaternion.Angle(transformT.rotation, rotation) < 1)
            {
                anim.SetBool("Shoot", true);
            }


        }
        else
        {
            currentEnemy = null;
            Quaternion targetRotation = Quaternion.LookRotation(carTransform.forward);
            float t = Time.deltaTime / resetDuration;
            Quaternion newRotation = Quaternion.Lerp(transformT.rotation, targetRotation, t);
            transformT.rotation = newRotation;
        }
    }

    public void FireTurret()
    {

        if (currentEnemy != null)
        {
            audioSource.Play();
            anim.SetBool("Shoot", false);
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
