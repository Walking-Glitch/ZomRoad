using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnergyGun : Turret
{
   public GameObject explosionObject;
   public ParticleSystem ExplosionParticleSystem;

   [SerializeField] protected ParticleSystem muzzleFlashRight;
   [SerializeField] protected ParticleSystem muzzleFlashLeft;

    public float explosionRadius;
   public LayerMask EnemiesLayerMask;

    //[SerializeField] protected ParticleSystem muzzleFlashRight;
    //[SerializeField] protected ParticleSystem muzzleFlashLeft;
    // Start is called before the first frame update

    protected override void AimAtTarget()
    {

        if (enemies.Count > 0)
        {
            if (currentEnemy == null || !enemies.Contains(currentEnemy))
            {
                currentEnemy = enemies[0];
            }


            Vector3 direction = currentEnemy.position - carTransform.position;//
            Quaternion rotation = Quaternion.LookRotation(direction);



            // Calculate the angle between the turret's forward direction and the direction to the enemy
            float angleToEnemy = Vector3.Angle(carTransform.forward, direction);//

            // Define the maximum allowed angle for the front field of view
            //float maxAngle = 45f; // Adjust this angle as needed

            // Check if the angle to the enemy is within the front field of view
            if (angleToEnemy <= maxAngle && gameManager.wheelController.energyAmmo != 0)
            {
                //Debug.Log("we are here 1");
                AudioSourceTurret.Play();
                // Interpolate between the current rotation and the target rotation using Quaternion.Lerp
                float t = Time.deltaTime / transitionDuration;
                Quaternion newRotation = Quaternion.Lerp(transformT.rotation, rotation, t);//
                transformT.rotation = newRotation;

                if( gameManager.wheelController.energyAmmo > 0)
                    anim.SetBool("Shoot", true);

            }
            else
            {
                // If the enemy is outside the front field of view, do not rotate the turret
                anim.SetBool("Shoot", false);

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
    public override void FireTurret()
    {

        if (currentEnemy != null && currentEnemy.gameObject.GetComponent<UndeadBase>().health > 0 && gameManager.wheelController.energyAmmo > 0)
        {
            tracerManager.SpawnEnergyTracer(currentEnemy);
            explosionObject.transform.SetParent(null);
            explosionObject.transform.position = currentEnemy.position + new Vector3(0, 1.2f, 0);
            muzzleFlashLeft.Play();
            muzzleFlashRight.Play();
            gameManager.casingManager.SpawnBulletCasing();
            gameManager.wheelController.SpendEnergyAmmo(1);
            EnemiesInBlastRadius();
            anim.SetBool("Shoot", false);
        }
        else
        {
            Debug.Log("fire turret called without an enemy");
        }
    }

    public void EnemiesInBlastRadius()
    {
        ExplosionParticleSystem.Play();

        Collider[] colliders = Physics.OverlapSphere(explosionObject.transform.position, explosionRadius, EnemiesLayerMask);

        foreach (Collider col in colliders)
        {
            if (col.gameObject.GetComponent<UndeadBase>().health > 0)
                col.gameObject.GetComponent<UndeadBase>().TakeDamage(WeaponDamage, new Vector3(25, 5, -50), true, 500f);
        }
        
    }

    protected override void SetWeaponStats(string wName, int damage, int range, int fireRate)
    {
        base.SetWeaponStats("Energy Gun", 500, (int)detectionRadius, 20);
    }
    public void PlaySfx()
    {
        audioSource.Play();
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire sphere in the editor to visualize the detection radius
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(explosionObject.transform.position, detectionRadius);
    }

}
