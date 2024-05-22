using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Turret
{
    [SerializeField]protected ParticleSystem muzzleFlashRight;
    [SerializeField] protected ParticleSystem muzzleFlashLeft;
    [SerializeField] Transform barrelRight;
    [SerializeField] Transform barrelLeft;
    public override void FireTurret()
    {

        if (currentEnemy != null && currentEnemy.gameObject.GetComponent<UndeadBase>().health > 0 && gameManager.wheelController.bulletAmmo > 0)
        {
            gameManager.tracerManager.SpawnBulletTracer(currentEnemy);
            audioSource.Play();
            muzzleFlashLeft.Play();
            muzzleFlashRight.Play();
            gameManager.casingManager.SpawnBulletCasing();
            gameManager.wheelController.SpendBulletAmmo(1);
            anim.SetBool("Shoot", false);
            currentEnemy.gameObject.GetComponent<UndeadBase>().TakeDamage(20, new Vector3(25, 5, -50), false, -500f);

        }
    }

}
