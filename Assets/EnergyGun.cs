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
    public override void FireTurret()
    {

        if (currentEnemy != null && currentEnemy.gameObject.GetComponent<UndeadBase>().health > 0)
        {
            explosionObject.transform.SetParent(null);
            explosionObject.transform.position = currentEnemy.position + new Vector3(0, 1.2f, 0);
            muzzleFlashLeft.Play();
            muzzleFlashRight.Play();
            gameManager.casingManager.SpawnBulletCasing();
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
                col.gameObject.GetComponent<UndeadBase>().TakeDamage(500);
        }
        
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
