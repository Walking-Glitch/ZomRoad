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
    protected AudioSource audioSource;
    protected ParticleSystem muzzleFlash;

    protected GameManager gameManager;


    [SerializeField] protected List<Transform> enemies = new List<Transform>();
    [SerializeField] protected Transform currentEnemy;
    protected virtual void Start()
    {
        gameManager = GameManager.Instance;
        audioSource = GetComponent<AudioSource>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        FindEnemies();
        AimAtTarget();
    }

    protected virtual void FindEnemies()
    {
        enemies.Clear();

        Collider[] colliders = Physics.OverlapSphere(transformT.position, detectionRadius, enemyLayer);

        foreach (Collider col in colliders)
        {
            if(col.gameObject.GetComponent<UndeadBase>().health > 0)
                enemies.Add(col.transform);
        }
    }


    protected virtual void AimAtTarget()
    {
        if (enemies.Count > 0)
        {
            if (currentEnemy == null || !enemies.Contains(currentEnemy))
            {
                currentEnemy = enemies[0];
            }

            Vector3 direction = currentEnemy.position - transformT.position;
            Quaternion rotation = Quaternion.LookRotation(direction);



            // Calculate the angle between the turret's forward direction and the direction to the enemy
            float angleToEnemy = Vector3.Angle(transformT.forward, direction);

            // Define the maximum allowed angle for the front field of view
            float maxAngle = 45f; // Adjust this angle as needed

            // Check if the angle to the enemy is within the front field of view
            if (angleToEnemy <= maxAngle)
            {
                // Interpolate between the current rotation and the target rotation using Quaternion.Lerp
                float t = Time.deltaTime / transitionDuration;
                Quaternion newRotation = Quaternion.Lerp(transformT.rotation, rotation, t);
                transformT.rotation = newRotation;

                // Set the "Shoot" animation trigger
                anim.SetBool("Shoot", true);
            }
            else
            {
                // If the enemy is outside the front field of view, do not rotate the turret
                anim.SetBool("Shoot", false);
                currentEnemy = null;
            }
        }
        else
        {
            anim.SetBool("Shoot", false);
            currentEnemy = null;
            Quaternion targetRotation = Quaternion.LookRotation(carTransform.forward);
            float t = Time.deltaTime / resetDuration;
            Quaternion newRotation = Quaternion.Lerp(transformT.rotation, targetRotation, t);
            transformT.rotation = newRotation;
        }
    }

    public virtual void FireTurret()
    {

        if (currentEnemy != null && currentEnemy.gameObject.GetComponent<UndeadBase>().health > 0)
        {
            audioSource.Play();
            muzzleFlash.Play();
            gameManager.casingManager.SpawnShellCasing();
            anim.SetBool("Shoot", false);
            currentEnemy.gameObject.GetComponent<UndeadBase>().TakeDamage(50);
           
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere in the editor to visualize the detection radius
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transformT.position, detectionRadius);
    }
}
