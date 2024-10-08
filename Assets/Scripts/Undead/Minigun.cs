using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Turret
{
    [SerializeField] protected ParticleSystem muzzleFlashRight;
    [SerializeField] protected ParticleSystem muzzleFlashLeft;
    //[SerializeField] Transform barrelRight;
    //[SerializeField] Transform barrelLeft;

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


            float angleToEnemy = Vector3.Angle(carTransform.forward, direction);//

            
            if (angleToEnemy <= maxAngle && gameManager.wheelController.bulletAmmo != 0)
            {
                //Debug.Log("we are here 1");
                AudioSourceTurret.Play();
                // Interpolate between the current rotation and the target rotation using Quaternion.Lerp
                float t = Time.deltaTime / transitionDuration;
                Quaternion newRotation = Quaternion.Lerp(transformT.rotation, rotation, t);//
                transformT.rotation = newRotation;

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

        if (currentEnemy != null && currentEnemy.gameObject.GetComponent<UndeadBase>().health > 0 && gameManager.wheelController.bulletAmmo > 0)
        {
            //gameManager.tracerManager.SpawnShellTracer(currentEnemy);
            tracerManager.SpawnBulletTracer(currentEnemy);
            audioSource.Play();
            muzzleFlashLeft.Play();
            muzzleFlashRight.Play();
            gameManager.casingManager.SpawnBulletCasing();
            gameManager.wheelController.SpendBulletAmmo(1);
            anim.SetBool("Shoot", false);
            currentEnemy.gameObject.GetComponent<UndeadBase>().TakeDamage(WeaponDamage, new Vector3(25, 5, -50), false, -500f);

        }
    }

    protected override void SetWeaponStats(string wName, int damage, int range, int fireRange)
    {
        base.SetWeaponStats("Gatling Gun", 20, (int)WeaponRange, 70);
    }

}
