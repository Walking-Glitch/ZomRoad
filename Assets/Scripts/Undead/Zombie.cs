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

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (health <= 0)
        {
            RagdollModeOn();
            ApplyKnockbackForce(-500f, new Vector3(25, 5, -50));
            PlayerDestroyZombie();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           TakeDamage(100);
        }
    }

}
