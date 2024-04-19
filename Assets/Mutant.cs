using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mutant : UndeadBase
{
    public bool IsInAttackArea { get; set; }
    public AudioClip swingSfx;

    public int expReward = 15;
    public int giveDamage = 25;
    protected override void Start()
    {
        base.Start();
        
    }

    
    protected override void Update()
    {
        //base.Update();
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


        if (!IsAgentOnNavMesh(undead) && !IsAgentOnOffMeshLink(undead))
        {
            CleanerDestroyZombie();
        }

        if (IsInAttackArea && !isDead)
        {
            anim.SetBool("Attack", true);
            undead.isStopped = true;
        }

        else
        {
            anim.SetBool("Attack", false);
           
        }
    }

    private void Attack()
    {
        undead.isStopped = true;

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


    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

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

    protected override void PlayerDestroyZombie()
    {
        bloodVisualEffect.enabled = false;
        gameObject.SetActive(false);
        health = maxHealth;
        gameManager.enemyManager.DecreaseEnemyCtr();
    }


    protected override void CleanerDestroyZombie()
    {
        PlayerDestroyZombie();
        //gameObject.SetActive(false);
        //gameManager.enemyManager.DecreaseEnemyCtr();
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
