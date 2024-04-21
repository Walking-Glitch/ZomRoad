using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class Zombie : UndeadBase
{

    public int expReward = 10;
    public int giveDamage = 10;
    protected override void Start()
    {
       base.Start();


        GetRagdollBits();
        RagdollModeOff();


    }

    // Update is called once per frame
    protected override void Update()
    {
       
        base.Update();
    }

    public override void TakeDamage(int damage, Vector3 bloodSpeed, bool explosion, float force)
    {
        base.TakeDamage(damage, bloodSpeed, explosion, force);

        if (health <= 0)
        {
            isDead = true;
            gameManager.wheelController.GainExp(expReward);
            RagdollModeOn();
            // ApplyKnockbackForce(-500f, new Vector3(25, 5, -50), false);
            ApplyKnockbackForce(force, bloodSpeed, explosion);
            PlayerDestroyZombie();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           TakeDamage(100, new Vector3(25, 5, -50), false, -500f);
           gameManager.wheelController.TakeDamage(giveDamage);
        }
    }

}
