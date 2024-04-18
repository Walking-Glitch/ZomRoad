using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Turret
{
    [SerializeField]protected ParticleSystem muzzleFlashRight;
    [SerializeField] protected ParticleSystem muzzleFlashLeft;
    public override void FireTurret()
    {

        if (currentEnemy != null && currentEnemy.gameObject.GetComponent<UndeadBase>().health > 0)
        {
            audioSource.Play();
            muzzleFlashLeft.Play();
            muzzleFlashRight.Play();
            gameManager.casingManager.SpawnBulletCasing();
            anim.SetBool("Shoot", false);
            currentEnemy.gameObject.GetComponent<UndeadBase>().TakeDamage(10);

        }
    }

}
