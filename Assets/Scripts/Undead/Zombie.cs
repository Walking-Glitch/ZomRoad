using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding.Ionic.Zip;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class Zombie : UndeadBase
{

    public int expReward = 10;
    private int giveDamage = 5;

    public AudioClip [] ZombieVoice;
    public AudioClip [] DeathAudioClip;
    protected override void Start()
    {
       base.Start();

        GetRagdollBits();
        RagdollModeOff();
        PlaySfx();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public void PlaySfx()
    {
        if (!isDead)
        {
            audioSource.loop = true;
            audioSource.clip = ZombieVoice[Random.Range(0, ZombieVoice.Length)];
            audioSource.Play();
        }
        else
        {
            audioSource.loop = false;
            audioSource.clip = DeathAudioClip[Random.Range(0, DeathAudioClip.Length)];
            audioSource.Play();
        }
    }
    protected override void RagdollModeOff()
    {
        base.RagdollModeOff();

        PlaySfx();
    }

    public override void TakeDamage(int damage, Vector3 bloodSpeed, bool explosion, float force)
    {
        base.TakeDamage(damage, bloodSpeed, explosion, force);

        if (health <= 0)
        {
            isDead = true;
            PlaySfx();
            gameManager.wheelController.GainExp(expReward);
            RagdollModeOn();
            // ApplyKnockbackForce(-500f, new Vector3(25, 5, -50), false);
            ApplyKnockbackForce(force, bloodSpeed, explosion);
            PlayerDestroyZombie();
            gameManager.enemyManager.ZombKCtr++;
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
