using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Mutant : UndeadBase
{
    public bool IsInAttackArea { get; set; }
    public AudioClip swingSfx;

    public int expReward = 15;
    public int giveDamage = 25;
    public int giveMinDamage = 2;
    protected override void Start()
    {
        base.Start();
    }
    
    protected override void Update()
    {
        if (health > 0)
        {
            isDead = false;
        }
        MutantMovement();
    }

    private void MutantMovement()
    {

        if (Time.frameCount % 10 == 0) // Update every 10 frames for example
        {
            isAgentOnNavMesh = IsAgentOnNavMesh(undead);
        }

        if (player != null && isAgentOnNavMesh && !isDead &&!IsInAttackArea) // Use cached result
        {
            undead.SetDestination(player.position);
        }


        if ((gameManager.wheelController.IsInvincible) ||!IsAgentOnNavMesh(undead) && !IsAgentOnOffMeshLink(undead))
        {
            CleanerDestroyZombie();
        }

        if (IsInAttackArea && !isDead)
        {
            anim.SetBool("Attack", true);
            undead.isStopped = true;

            Vector3 direction = player.position - transform.position;

            // Ensure the direction is not zero (to prevent NaN errors)
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);

                transform.rotation = lookRotation;
            }
        }

        else
        {
            anim.SetBool("Attack", false);
        }
    }

    private void Attack()
    {
        if (IsInAttackArea)
        {
            gameManager.wheelController.PlaySfx();
            player.gameObject.GetComponent<Rigidbody>().AddForce((undead.transform.forward.normalized + undead.transform.up.normalized) * 5000f, ForceMode.Impulse);
            gameManager.wheelController.TakeDamage(giveDamage);
        }

        else
        {
            Debug.Log("missed");
        }
      
    }

    private void PlaySfx()
    {
        audioSource.clip = swingSfx;
        audioSource.Play();
    }


    public override void TakeDamage(int damage, Vector3 bloodSpeed, bool explosion, float force)
    {
        base.TakeDamage(damage, bloodSpeed, explosion, force);

        if (health <= 0)
        {
            isDead = true;
            gameManager.wheelController.GainExp(expReward);
            audioSource.Stop();
            bloodVisualEffect.Play();
            undead.isStopped = true;
            anim.SetBool("Dead", true);
            return;
        }

        bloodVisualEffect.enabled = true;
        bloodVisualEffect.Play();
        anim.SetTrigger("Hit");

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.wheelController.TakeDamage(giveMinDamage);
        }
    }

    protected override void PlayerDestroyZombie()
    {
        bloodVisualEffect.enabled = false;
        gameObject.SetActive(false);
        health = maxHealth;
        gameManager.enemyManager.DecreaseEnemyCtr();
        gameManager.enemyManager.BruteKCtr++;
    }


    protected override void CleanerDestroyZombie()
    {
        PlayerDestroyZombie();
    }

    public void SetIsInAttackArea(bool isInAttackArea)
    {
        IsInAttackArea = isInAttackArea;
    }

    protected void ResumeMovement()
    {
        undead.isStopped = false;
    }
}
